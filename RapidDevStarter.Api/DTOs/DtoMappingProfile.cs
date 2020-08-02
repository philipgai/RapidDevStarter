using AutoMapper;
using Microsoft.AspNet.OData;
using RapidDevStarter.Entities.RapidDevStarterEntities;

namespace RapidDevStarter.Api.DTOs
{
    public class DtoMappingProfile : Profile
    {
        public DtoMappingProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<ContactInfo, ContactInfoDto>().ReverseMap();
        }
    }
}