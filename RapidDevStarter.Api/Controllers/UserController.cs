using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Mvc;
using RapidDevStarter.Api.DTOs;
using RapidDevStarter.Entities.RapidDevStarterEntities;
using System;
using System.Linq;

namespace RapidDevStarter.Api.Controllers
{
    [ODataRoutePrefix("User")]
    public class UserController : ControllerBase
    {
        private readonly RapidDevStarterDbContext _rapidDevStarterDbContext;
        private readonly IMapper _mapper;

        public UserController(RapidDevStarterDbContext rapidDevStarterDbContext, IMapper mapper)
        {
            _rapidDevStarterDbContext = rapidDevStarterDbContext ?? throw new ArgumentNullException(nameof(rapidDevStarterDbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [ODataRoute]
        [EnableQuery]
        public IQueryable<UserDto> Get()
        {
            return _rapidDevStarterDbContext.User.ProjectTo<UserDto>(_mapper.ConfigurationProvider);
        }
    }
}