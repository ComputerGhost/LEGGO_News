using AutoMapper;
using CMS.ViewModels;
using Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace CMS
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            ConfigureCharacter();
            ConfigureTag();
        }

        // Methods below are grouped by destination type.

        private void ConfigureCharacter()
        {
            CreateMap<Character, CharacterEditViewModel>();
            CreateMap<Character, CharacterIndexItemViewModel>();
            CreateMap<CharacterEditViewModel, Character>();
        }

        private void ConfigureTag()
        {
            CreateMap<Tag, TagIndexItemViewModel>();
            CreateMap<Tag, TagEditViewModel>();
            CreateMap<TagEditViewModel, Tag>();
        }
    }
}
