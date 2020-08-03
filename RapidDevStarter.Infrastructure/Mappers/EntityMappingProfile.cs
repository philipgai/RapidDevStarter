using AutoMapper;
using RapidDevStarter.Core.Models;
using RapidDevStarter.Entities.RapidDevStarterEntities;

namespace RapidDevStarter.Infrastructure.Mappers
{
    public class EntityMappingProfile : Profile
    {
        public EntityMappingProfile()
        {
            CreateMap<User, UserModel>().ReverseMap();
            CreateMap<ContactInfo, ContactInfoModel>().ReverseMap();
        }
    }
}