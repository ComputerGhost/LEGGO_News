using API.DTOs;
using AutoMapper;
using Data.Models;

namespace API
{
    using BCrypt = BCrypt.Net.BCrypt;

    class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Article, ArticleSummary>();
            CreateMap<Article, ArticleDetails>();
            CreateMap<ArticleSaveData, Article>();

            CreateMap<Character, CharacterSummary>();
            CreateMap<Character, CharacterDetails>();
            CreateMap<CharacterSaveData, Character>();

            CreateMap<Comment, CommentSummary>();
            CreateMap<Comment, CommentDetails>();
            CreateMap<CommentSaveData, Comment>();

            CreateMap<Tag, TagSummary>();
            CreateMap<Tag, TagDetails>();
            CreateMap<TagSaveData, Tag>();

            CreateMap<Template, TemplateSummary>();
            CreateMap<Template, TemplateDetails>();
            CreateMap<TemplateSaveData, Template>();

            CreateMap<User, UserSummary>();
            CreateMap<User, UserDetails>();
            CreateMap<UserSaveData, User>()
                .ForMember(d => d.HashedPassword, o =>
                {
                    o.MapFrom(s => BCrypt.HashPassword(s.Password));
                    o.Condition(s => !string.IsNullOrEmpty(s.Password));
                });
        }
    }
}
