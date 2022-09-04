using AutoMapper;

namespace API.Setup
{
    internal class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Our libraries don't depend on each other,
            // so do the inter-library mappings here.
            CreateMap<Database.DTOs.CalendarSummary, Calendar.Models.CalendarInfo>();
        }
    }
}
