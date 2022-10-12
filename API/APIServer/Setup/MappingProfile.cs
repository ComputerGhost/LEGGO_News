using APIClient.DTOs;
using APIServer.Database.Models;
using AutoMapper;

namespace APIServer.Setup
{
    internal class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CalendarSummary, Calendar.Models.CalendarInfo>();

            CreateMap<Article, ArticleSummary>();
            CreateMap<Article, ArticleDetails>();
            CreateMap<ArticleSaveData, Article>();

            CreateMap<Database.Models.Calendar, CalendarSummary>();
            CreateMap<Database.Models.Calendar, CalendarDetails>();
            CreateMap<CalendarSaveData, Database.Models.Calendar>();

            CreateMap<Character, CharacterSummary>();
            CreateMap<Character, CharacterDetails>();
            CreateMap<CharacterSaveData, Character>();

            CreateMap<Database.Models.Media, MediaSummary>();
            CreateMap<Database.Models.Media, MediaDetails>();
            CreateMap<MediaSaveExistingData, Database.Models.Media>();
            CreateMap<MediaSaveNewData, Database.Models.Media>();

            CreateMap<Tag, TagSummary>();
            CreateMap<Tag, TagDetails>();
            CreateMap<TagSaveData, Tag>();
        }
    }
}
