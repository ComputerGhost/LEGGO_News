using AutoMapper;
using Database.DTOs;
using Database.Internal.Models;

namespace Database.Internal.Setup
{
    internal class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Article, ArticleSummary>();
            CreateMap<Article, ArticleDetails>();
            CreateMap<ArticleSaveData, Article>();

            CreateMap<Calendar, CalendarSummary>();
            CreateMap<Calendar, CalendarDetails>();
            CreateMap<CalendarSaveData, Calendar>();

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
