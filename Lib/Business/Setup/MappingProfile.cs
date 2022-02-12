using AutoMapper;
using Business.DTOs;
using Data.Models;

namespace Business.Setup
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Article, ArticleSummary>();
            CreateMap<Article, ArticleDetails>();
            CreateMap<ArticleSaveData, Article>();
        }
    }
}
