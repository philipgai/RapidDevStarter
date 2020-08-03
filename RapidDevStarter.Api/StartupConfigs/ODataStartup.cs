using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Routing;
using Microsoft.OData.Edm;
using RapidDevStarter.Api.DTOs;
using System.Linq;

namespace RapidDevStarter.Api.StartupConfigs
{
    public static class ODataStartup
    {
        public static void UseOData(IEndpointRouteBuilder endpointRouteBuilder)
        {
            endpointRouteBuilder.Select().Expand().Filter().OrderBy().MaxTop(100).Count();
            endpointRouteBuilder.EnableDependencyInjection();
            endpointRouteBuilder.MapODataRoute("ODataRoute", null, GetEdmModel());
        }

        private static IEdmModel GetEdmModel()
        {
            var builder = new ODataConventionModelBuilder();
            builder.EntitySet<UserDto>("Users").EntityType.HasKey(dto => dto.UserKey);
            builder.EntitySet<ContactInfoDto>("ContactInfos").EntityType.HasKey(dto => dto.ContactInfoUserKey);
            return builder.GetEdmModel();
        }
    }
}