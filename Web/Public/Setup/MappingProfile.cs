using APIClient.DTOs;
using AutoMapper;
using Public.ViewModels;

namespace Public.Setup
{
    internal class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<SearchResults<ArticleSummary>, ArticleIndexViewModel>();
            CreateMap<ArticleSummary, ArticleIndexViewModel.ArticleIndexItem>();
            CreateMap<ArticleDetails, ArticleViewModel>();

            // Our libraries don't depend on each other,
            // so do the inter-library mappings here.
            CreateMap<APIClient.DTOs.CalendarSummary, Calendar.Models.CalendarInfo>();
        }
    }
}
