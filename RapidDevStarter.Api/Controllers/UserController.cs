using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Mvc;
using RapidDevStarter.Api.DTOs;
using RapidDevStarter.Entities.RapidDevStarterEntities;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RapidDevStarter.Api.Controllers
{
    [ApiExplorerSettings(IgnoreApi = false)]
    [ODataRoutePrefix("Users")]
    public class UserController : ODataController
    {
        private readonly RapidDevStarterDbContext _rapidDevStarterDbContext;
        private readonly IMapper _mapper;

        public UserController(RapidDevStarterDbContext rapidDevStarterDbContext, IMapper mapper)
        {
            _rapidDevStarterDbContext = rapidDevStarterDbContext ?? throw new ArgumentNullException(nameof(rapidDevStarterDbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet("Users")]
        [ODataRoute]
        [EnableQuery]
        public IQueryable<UserDto> GetUsers([FromQuery(Name = "$skip")] int skip, [FromQuery(Name = "$count")] bool count, [FromQuery(Name = "$top")] int top, [FromQuery(Name = "$select")] string select, [FromQuery(Name = "$expand")] string expand, [FromQuery(Name = "$orderby")] string orderBy, [FromQuery(Name = "$filter")] string filter, [FromQuery(Name = "$apply")] string apply, [FromQuery(Name = "$format")] string format, [FromQuery(Name = "$skiptoken")] string skipToken, [FromQuery(Name = "$deltatoken")] string deltaToken)
        {
            return _rapidDevStarterDbContext.User.ProjectTo<UserDto>(_mapper.ConfigurationProvider);
        }

        [HttpGet("Users({key})")]
        [ODataRoute("({key})")]
        [EnableQuery(AllowedQueryOptions = AllowedQueryOptions.Select | AllowedQueryOptions.Expand)]
        public SingleResult<UserDto> Get([Required][FromODataUri] int key, [FromQuery(Name = "$select")] string select, [FromQuery(Name = "$expand")] string expand)
        {
            var result = _rapidDevStarterDbContext.User.Where(user => user.UserKey == key).ProjectTo<UserDto>(_mapper.ConfigurationProvider);
            return SingleResult.Create(result);
        }

        [HttpPost("Users")]
        [ODataRoute]
        public async Task<IActionResult> Post([FromBody] UserDto userDto)
        {
            var userEntity = _mapper.Map<User>(userDto);
            _rapidDevStarterDbContext.User.Add(userEntity);
            await _rapidDevStarterDbContext.SaveChangesAsync();
            var result = _mapper.Map<UserDto>(userEntity);
            return Created(result);
        }

        [HttpPatch("Users({key})")]
        [ODataRoute("({key})")]
        public async Task<IActionResult> Patch([Required][FromODataUri] int key, [FromBody] Delta<UserDto> userDeltaDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var originalEntity = await _rapidDevStarterDbContext.User.FindAsync(key);

            if (originalEntity == null)
            {
                return NotFound();
            }

            var originalDto = _mapper.Map<UserDto>(originalEntity);
            userDeltaDto.Patch(originalDto);

            originalEntity.Email = originalDto.Email;
            originalEntity.FirstName = originalDto.FirstName;
            originalEntity.LastName = originalDto.LastName;
            originalEntity.MiddleName = originalDto.MiddleName;
            originalEntity.UserName = originalDto.UserName;

            await _rapidDevStarterDbContext.SaveChangesAsync();
            var result = _mapper.Map<UserDto>(originalEntity);
            return Updated(result);
        }

        [HttpDelete("Users({key})")]
        [ODataRoute("({key})")]
        public async Task<IActionResult> Delete([Required][FromODataUri] int key)
        {
            var product = await _rapidDevStarterDbContext.User.FindAsync(key);
            if (product == null)
            {
                return NotFound();
            }
            _rapidDevStarterDbContext.User.Remove(product);
            await _rapidDevStarterDbContext.SaveChangesAsync();
            return NoContent();
        }
    }
}