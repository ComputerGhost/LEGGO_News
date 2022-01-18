using API.DTOs;
using AutoMapper;
using Data.Models;

namespace API
{
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
        }
    }
}
