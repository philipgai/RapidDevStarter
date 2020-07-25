using RapidDevStarter.Api.App_Start;
using System.Web.Http;

namespace RapidDevStarter.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            AutofacConfig.Register();
            RestierConfig.Register(config);
        }
    }
}