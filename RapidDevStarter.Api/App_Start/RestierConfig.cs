using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNet.OData.Query;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Restier.EntityFramework;
using RapidDevStarter.Api.Controllers;
using RapidDevStarter.Entities;
using System;
using System.Linq;
using System.Web.Http;

namespace RapidDevStarter.Api.App_Start
{
    public static class RestierConfig
    {
        public static void Register(HttpConfiguration config)
        {
#if !PROD
            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;
#endif

            config.Filter().Expand().Select().OrderBy().MaxTop(100).Count().SetTimeZoneInfo(TimeZoneInfo.Utc);

            config.UseRestier<ODataController>((services) =>
            {
                services.AddEF6ProviderServices<RapidDevStarterEntities>();

                services.AddSingleton(new ODataValidationSettings
                {
                    MaxAnyAllExpressionDepth = 3,
                    MaxExpansionDepth = 3
                });
            });

            config.MapRestier<ODataController>(
                routeName: "RapidDevStarterODataApiv1",
                routePrefix: "v1",
                allowBatching: true
            );
        }
    }
}