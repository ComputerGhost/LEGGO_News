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

            CreateMap<Character, CharacterSummary>();
            CreateMap<Character, CharacterDetails>();
            CreateMap<CharacterSaveData, Character>();

            CreateMap<Lead, LeadSummary>();
            CreateMap<Lead, LeadDetails>();
            CreateMap<LeadSaveData, Lead>();

            CreateMap<Media, MediaSummary>();
            CreateMap<Media, MediaDetails>();
            CreateMap<MediaSaveExistingData, Media>();
            CreateMap<MediaSaveNewData, Media>();

            CreateMap<Tag, TagSummary>();
            CreateMap<Tag, TagDetails>();
            CreateMap<TagSaveData, Tag>();
        }
    }
}
