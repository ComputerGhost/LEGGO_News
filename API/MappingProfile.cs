using API.DTOs;
using AutoMapper;
using Business.DTOs;
using Data.Models;

namespace API
{
    class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Character, CharacterSummary>();
            CreateMap<Character, CharacterDetails>();
            CreateMap<CharacterSaveData, Character>();
        }
    }
}
