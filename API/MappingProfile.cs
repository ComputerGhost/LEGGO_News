using API.DTOs;
using AutoMapper;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API
{
    class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Character, CharacterSummary>();
            CreateMap<Character, CharacterDetails>()
                .ReverseMap();
        }
    }
}
