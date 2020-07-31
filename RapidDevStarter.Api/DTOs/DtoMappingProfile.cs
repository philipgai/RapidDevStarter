using AutoMapper;
using RapidDevStarter.Entities.RapidDevStarterEntities;

namespace RapidDevStarter.Api.DTOs
{
    public class DtoMappingProfile : Profile
    {
        public DtoMappingProfile()
        {
            CreateMap<User, UserDto>();
        }
    }
}