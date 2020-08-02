using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RapidDevStarter.Api.DTOs;
using RapidDevStarter.Entities.DbContexts;
using RapidDevStarter.Entities.RapidDevStarterEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RapidDevStarter.Api.Controllers
{
    // TODO - Move controller methods to Core, remove reference to *.Entities
    [ODataRoutePrefix("Users")]
    // Get OData endpoints to show up in Swagger
    // HttpMethod("Route") is added to endpoints for Swagger
    // [FromQuery] and [FromBody] added for Swagger
    [ApiExplorerSettings(IgnoreApi = false)]
    public class UserController : ODataController
    {
        private readonly RapidDevStarterDbContextWrapper _rapidDevStarterDbContext;
        private readonly IMapper _mapper;

        public UserController(RapidDevStarterDbContextWrapper rapidDevStarterDbContext, IMapper mapper)
        {
            _rapidDevStarterDbContext = rapidDevStarterDbContext ?? throw new ArgumentNullException(nameof(rapidDevStarterDbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [ODataRoute]
        [EnableQuery]
        [HttpGet("Users")]
        public async Task<IEnumerable<UserDto>> GetUsers([FromQuery(Name = "$count")] bool count, [FromQuery(Name = "$skip")] int skip, [FromQuery(Name = "$top")] int top, [FromQuery(Name = "$filter")] string filter, [FromQuery(Name = "$expand")] string expand, [FromQuery(Name = "$select")] string select, [FromQuery(Name = "$orderby")] string orderBy, [FromQuery(Name = "$apply")] string apply, [FromQuery(Name = "$format")] string format, [FromQuery(Name = "$skiptoken")] string skipToken, [FromQuery(Name = "$deltatoken")] string deltaToken)
        {
            // Use ToListAsync to allow orderby with navigation properties
            return await _rapidDevStarterDbContext.User.ProjectTo<UserDto>(_mapper.ConfigurationProvider).ToListAsync();
        }

        [ODataRoute("({key})")]
        [EnableQuery(AllowedQueryOptions = AllowedQueryOptions.Expand | AllowedQueryOptions.Select)]
        [HttpGet("Users({key})")]
        public SingleResult<UserDto> Get([Required][FromODataUri] int key, [FromQuery(Name = "$expand")] string expand, [FromQuery(Name = "$select")] string select)
        {
            var result = _rapidDevStarterDbContext.User.Where(user => user.UserKey == key).ProjectTo<UserDto>(_mapper.ConfigurationProvider);
            return SingleResult.Create(result);
        }

        [ODataRoute]
        [HttpPost("Users")]
        public async Task<IActionResult> Post([FromBody] UserDto userDto)
        {
            var userEntity = _mapper.Map<User>(userDto);
            _rapidDevStarterDbContext.User.Add(userEntity);
            await _rapidDevStarterDbContext.SaveChangesAsync();
            var result = _mapper.Map<UserDto>(userEntity);
            return Created(result);
        }

        [ODataRoute("({key})")]
        [HttpPut("Users({key})")]
        public async Task<IActionResult> Put([Required][FromODataUri] int key, [FromBody] UserDto updatedUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userEntity = await _rapidDevStarterDbContext.User
                .Include(user => user.ContactInfo)
                .SingleOrDefaultAsync(user => user.UserKey == key);

            if (userEntity == null)
            {
                return NotFound();
            }

            _rapidDevStarterDbContext.Entry(userEntity).CurrentValues.SetValues(updatedUser);

            if (userEntity.ContactInfo == null && updatedUser.ContactInfo != null) // Create new ContactInfo
            {
                userEntity.ContactInfo = _mapper.Map<ContactInfo>(updatedUser.ContactInfo);
            }
            else if (userEntity.ContactInfo != null && updatedUser.ContactInfo != null) // Update current ContactInfo
            {
                _rapidDevStarterDbContext.Entry(userEntity.ContactInfo).CurrentValues.SetValues(updatedUser.ContactInfo);
            }
            else if (userEntity.ContactInfo != null && updatedUser.ContactInfo == null) // Delete ContactInfo
            {
                userEntity.ContactInfo = null;
            }

            await _rapidDevStarterDbContext.SaveChangesAsync();
            var result = _mapper.Map<UserDto>(userEntity);
            return Updated(result);
        }

        [ODataRoute("({key})")]
        [HttpDelete("Users({key})")]
        public async Task<IActionResult> Delete([Required][FromODataUri] int key)
        {
            var userEntity = await _rapidDevStarterDbContext.User
                .Include(user => user.ContactInfo)
                .SingleOrDefaultAsync(user => user.UserKey == key);
            if (userEntity == null)
            {
                return NotFound();
            }
            _rapidDevStarterDbContext.User.Remove(userEntity);
            await _rapidDevStarterDbContext.SaveChangesAsync();
            return NoContent();
        }
    }
}