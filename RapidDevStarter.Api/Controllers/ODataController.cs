using Microsoft.Restier.EntityFramework;
using RapidDevStarter.Entities;
using System;

namespace RapidDevStarter.Api.Controllers
{
    public class ODataController : EntityFrameworkApi<RapidDevStarterEntities>
    {
        public ODataController(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
}