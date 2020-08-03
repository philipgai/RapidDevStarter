using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RapidDevStarter.Core.Interfaces.Repos;
using RapidDevStarter.Core.Services;
using RapidDevStarter.Entities.DbContexts;
using RapidDevStarter.Infrastructure.Mappers;
using RapidDevStarter.Infrastructure.Repos;

namespace RapidDevStarter.Infrastructure.Config
{
    public static class RapidDevStarterServices
    {
        public static void AddCoreServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
        }

        public static void AddInfraServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUserRepo, UserRepo>();

            services.AddDbContext<RapidDevStarterDbContextWrapper>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("RapidDevStarterDB"), options =>
                {
                    options.EnableRetryOnFailure();
                });
            });
        }

        public static void AddInfraProfiles(IMapperConfigurationExpression mc)
        {
            mc.AddProfile<EntityMappingProfile>();
        }
    }
}