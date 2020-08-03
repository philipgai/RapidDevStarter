using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using RapidDevStarter.Api.Mappers;
using RapidDevStarter.Infrastructure.Config;

namespace RapidDevStarter.Api.StartupConfigs
{
    public static class AutoMapperStartup
    {
        public static void AddAutoMapper(IServiceCollection services)
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile<DtoMappingProfile>();
                RapidDevStarterServices.AddInfraProfiles(mc);
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}