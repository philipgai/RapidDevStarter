using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using RapidDevStarter.Api.DTOs;
using RapidDevStarter.Entities.RapidDevStarterEntities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RapidDevStarter.Api.Controllers
{
    public class UsersController : ODataController
    {
        private readonly RapidDevStarterDbContext _rapidDevStarterDbContext;
        private readonly IMapper _mapper;

        public UsersController(RapidDevStarterDbContext rapidDevStarterDbContext, IMapper mapper)
        {
            _rapidDevStarterDbContext = rapidDevStarterDbContext ?? throw new ArgumentNullException(nameof(rapidDevStarterDbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [EnableQuery]
        public IQueryable<UserDto> Get()
        {
            return _rapidDevStarterDbContext.User.ProjectTo<UserDto>(_mapper.ConfigurationProvider);
        }

        [EnableQuery]
        public SingleResult<UserDto> Get([FromODataUri] int key)
        {
            var result = _rapidDevStarterDbContext.User.Where(user => user.UserKey == key).ProjectTo<UserDto>(_mapper.ConfigurationProvider);
            return SingleResult.Create(result);
        }

        public async Task<IActionResult> Post([FromBody] UserDto userDto)
        {
            var userEntity = _mapper.Map<User>(userDto);
            _rapidDevStarterDbContext.User.Add(userEntity);
            await _rapidDevStarterDbContext.SaveChangesAsync();
            var result = _mapper.Map<UserDto>(userEntity);
            return Created(result);
        }

        public async Task<IActionResult> Patch([FromODataUri] int key, Delta<UserDto> userDeltaDto)
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

        public async Task<IActionResult> Delete([FromODataUri] int key)
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