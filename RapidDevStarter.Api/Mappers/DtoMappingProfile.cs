using AutoMapper;
using RapidDevStarter.Api.DTOs;
using RapidDevStarter.Core.Models;
using RapidDevStarter.Entities.RapidDevStarterEntities;

namespace RapidDevStarter.Api.Mappers
{
    public class DtoMappingProfile : Profile
    {
        public DtoMappingProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<ContactInfo, ContactInfoDto>().ReverseMap();

            CreateMap<UserDto, UserModel>().ReverseMap();
            CreateMap<ContactInfoDto, ContactInfoModel>().ReverseMap();
        }
    }
}