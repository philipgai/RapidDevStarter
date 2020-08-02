using AutoMapper;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Net.Http.Headers;
using Microsoft.OData.Edm;
using Microsoft.OpenApi.Models;
using RapidDevStarter.Api.DTOs;
using RapidDevStarter.Entities.RapidDevStarterEntities;
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

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOData();

            services.AddControllers(options =>
            {
                foreach (var outputFormatter in options.OutputFormatters.OfType<OutputFormatter>().Where(x => x.SupportedMediaTypes.Count == 0))
                {
                    outputFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/prs.odatatestxx-odata"));
                }

                foreach (var inputFormatter in options.InputFormatters.OfType<InputFormatter>().Where(x => x.SupportedMediaTypes.Count == 0))
                {
                    inputFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/prs.odatatestxx-odata"));
                }
            }).AddNewtonsoftJson();
            services.AddAutoMapper(typeof(Startup));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "RapidDevStarter API v1", Version = "v1" });
            });
            services.AddSwaggerGenNewtonsoftSupport();

            services.AddDbContext<RapidDevStarterDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("RapidDevStarterDB"), options =>
                {
                    options.EnableRetryOnFailure();
                });
            });

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

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            // Redirect for swagger OData queries
            app.Use(async (context, next) =>
            {
                var url = context.Request.Path.Value;
                var queryParams = context.Request.Query;

                if (url.Contains("{key}") && queryParams.ContainsKey("key"))
                {
                    var key = queryParams["key"].ToString();
                    var newPath = url.Replace("{key}", key);
                    context.Request.Query = new QueryCollection(queryParams.Where(p => p.Key != "key").ToDictionary(p => p.Key, p => p.Value));
                    context.Response.Redirect($"{newPath}{context.Request.QueryString}");
                    return;   // short circuit
                }

                await next();
            });

            app.UseCors(CorsPolicyName);

            app.UseAuthorization();

            app.UseEndpoints(endpointRouteBuilder =>
            {
                endpointRouteBuilder.MapControllers();
                endpointRouteBuilder.Select().Expand().Filter().OrderBy().MaxTop(100).Count();
                endpointRouteBuilder.EnableDependencyInjection();
                endpointRouteBuilder.MapODataRoute("ODataRoute", null, GetEdmModel());
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "RapidDevStarter API v1");
            });
        }

        private static IEdmModel GetEdmModel()
        {
            var builder = new ODataConventionModelBuilder();
            builder.EntitySet<UserDto>("Users").EntityType.HasKey(user => user.UserKey);//.Name = "User";
            return builder.GetEdmModel();
        }
    }
}