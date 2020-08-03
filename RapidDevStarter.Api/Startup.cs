using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Net.Http.Headers;
using RapidDevStarter.Api.StartupConfigs;
using RapidDevStarter.Infrastructure.Config;
using System.Linq;

namespace RapidDevStarter.Api
{
    public class Startup
    {
        private readonly string CorsPolicyName = "RapidDevStarterApiCorsPolicy";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCoreServices();
            services.AddInfraServices(Configuration);

            services.AddOData();

            services.AddControllers(options =>
            {
                // outputFormatter and inputFormatter added so Swagger still works with ODataControllers
                foreach (var outputFormatter in options.OutputFormatters.OfType<OutputFormatter>().Where(x => x.SupportedMediaTypes.Count == 0))
                {
                    outputFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/prs.odatatestxx-odata"));
                }

                foreach (var inputFormatter in options.InputFormatters.OfType<InputFormatter>().Where(x => x.SupportedMediaTypes.Count == 0))
                {
                    inputFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/prs.odatatestxx-odata"));
                }
            }).AddNewtonsoftJson();

            AutoMapperStartup.AddAutoMapper(services);

            SwaggerStartup.AddSwagger(services);

            services.AddCors(corsOptions =>
            {
                corsOptions.AddPolicy(CorsPolicyName, options =>
                {
                    options
                    .WithOrigins(Configuration.GetSection("Clients")["RapidDevStarterWebUrl"])
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            SwaggerStartup.UseSwagger(app);

            app.UseRouting();

            app.UseCors(CorsPolicyName);

            app.UseAuthorization();

            app.UseEndpoints(endpointRouteBuilder =>
            {
                endpointRouteBuilder.MapControllers();
                ODataStartup.UseOData(endpointRouteBuilder);
            });
        }
    }
}