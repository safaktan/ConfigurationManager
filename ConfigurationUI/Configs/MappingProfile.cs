using AutoMapper;
using ConfigurationUI.Models;
using ConfigurationUI.ViewModels;

namespace ConfigurationUI.Configs
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ConfigurationParameter, ConfigurationViewModel>().ReverseMap();
        }
    }
}