using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.Linq;

namespace RapidDevStarter.Api.StartupConfigs
{
    public static class SwaggerStartup
    {
        public static void AddSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "RapidDevStarter API v1", Version = "v1" });
            });
            services.AddSwaggerGenNewtonsoftSupport();
        }

        public static void UseSwagger(IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "RapidDevStarter API v1");
            });

            // Redirect for Swagger OData queries
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
                    return;
                }

                await next();
            });
        }
    }
}