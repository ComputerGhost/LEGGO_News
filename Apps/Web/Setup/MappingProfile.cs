using AutoMapper;
using Database.DTOs;
using Web.ViewModels;

namespace Web.Setup
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
            CreateMap<Database.DTOs.CalendarSummary, Calendar.Models.CalendarInfo>();
        }
    }
}
