using AutoMapper;
using Database.DTOs;
using Database.Internal;
using Database.Internal.Models;
using Database.Repositories.Interfaces;
using System.Linq;

namespace Database.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly DatabaseContext _databaseContext;
        private readonly IMapper _mapper;

        public TagRepository(DatabaseContext databaseContext, IMapper mapper)
        {
            _databaseContext = databaseContext;
            _mapper = mapper;
        }

        public TagSummary Create(TagSaveData saveData)
        {
            var newTag = _mapper.Map<Tag>(saveData);
            _databaseContext.Tags.Add(newTag);
            _databaseContext.SaveChanges();

            return _mapper.Map<TagSummary>(newTag);
        }

        public void Delete(long id)
        {
            var tag = _databaseContext.Tags.Find(id);
            _databaseContext.Tags.Remove(tag);
            _databaseContext.SaveChanges();
        }

        public TagDetails Fetch(long id)
        {
            var tag = _databaseContext.Tags.Find(id);
            if (tag == null)
            {
                return null;
            }
            return _mapper.Map<TagDetails>(tag);
        }

        public SearchResults<TagSummary> Search(SearchParameters parameters)
        {
            var foundTags = _databaseContext.Tags.AsQueryable();
            if (!string.IsNullOrEmpty(parameters.Query))
            {
                foundTags = foundTags.Where(tag => tag.Name.Contains(parameters.Query));
            }

            var tagsPage = foundTags
                .Skip(parameters.Offset)
                .Take(parameters.Count);

            return new SearchResults<TagSummary>
            {
                Key = parameters.Key,
                Offset = parameters.Offset,
                Count = tagsPage.Count(),
                TotalCount = foundTags.Count(),
                Data = tagsPage.Select(tag => _mapper.Map<TagSummary>(tag))
            };
        }

        public void Update(long id, TagSaveData saveData)
        {
            var tag = _databaseContext.Tags.Find(id);
            _mapper.Map(saveData, tag);
            _databaseContext.SaveChanges();
        }
    }
}
