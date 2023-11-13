using ContentCore.Articles.DTOs;
using ContentCore.ArticlesCore.Events;
using ContentCore.ArticlesCore.tbd;
using ContentCore.Events;
using ContentCore.Modules.Tags.Repositories;
using System;

namespace ContentCore.Modules.Tags
{
    internal class TagEventHandlers
    {
        private readonly ArticleTagRepository _articleTagsRepository;
        private readonly TagRepository _tagRepository;

        public TagEventHandlers(
            ArticleTagRepository articleTagsRepository,
            ArticleService articleService,
            TagRepository tagRepository)
        {
            _articleTagsRepository = articleTagsRepository;
            _tagRepository = tagRepository;

            articleService.OnArticleCreating += HandleArticleCreated;
            articleService.OnArticleFetching += HandleArticleFetching;
            articleService.OnArticleReplaced += HandleArticleReplaced;
        }

        private async Task HandleArticleCreated(object? sender, ArticleCreatingEventArgs eventArgs)
        {
            if (eventArgs.ArticleDto.Tags != null)
            {
                await AddTagsToArticle(eventArgs.ArticleId, eventArgs.ArticleDto.Tags).ConfigureAwait(false);
            }
        }

        private async Task HandleArticleFetching(object? sender, ArticleFetchingEventArgs eventArgs)
        {
            var articleId = eventArgs.ArticleDto.Id!.Value;
            var tags = await _articleTagsRepository.FetchArticleTagsAsync(articleId).ConfigureAwait(false);
            eventArgs.ArticleDto.Tags = tags;
        }

        private async Task HandleArticleReplaced(object? sender, ArticleReplacingEventArgs eventArgs)
        {
            await _articleTagsRepository.DetachArticleTagsAsync(eventArgs.ArticleDto.Id!.Value);
            if (eventArgs.ArticleDto.Tags != null)
            {
                await AddTagsToArticle(eventArgs.ArticleDto.Id!.Value, eventArgs.ArticleDto.Tags).ConfigureAwait(false);
            }
        }

        private async Task AddTagsToArticle(int articleId, IEnumerable<string> tags)
        {
            foreach (var tag in tags)
            {
                await _tagRepository.InsertTagIfNotExistsAsync(tag).ConfigureAwait(false);
                await _articleTagsRepository.AttachArticleTagAsync(articleId, tag).ConfigureAwait(false);
            }
        }
    }
}
