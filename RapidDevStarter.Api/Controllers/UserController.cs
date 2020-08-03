using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RapidDevStarter.Api.DTOs;
using RapidDevStarter.Core.Models;
using RapidDevStarter.Core.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace RapidDevStarter.Api.Controllers
{
    [ODataRoutePrefix("Users")]
    [ApiExplorerSettings(IgnoreApi = false)] // Needed for OData in Swagger
    public class UserController : ODataController
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [ODataRoute]
        [EnableQuery]
        [HttpGet("[controller]s")] // Needed for OData in Swagger
        // [FromQuery] and [FromBody] params needed for Swagger
        public async Task<IEnumerable<UserDto>> Get([FromQuery(Name = "$count")] bool count, [FromQuery(Name = "$skip")] int skip, [FromQuery(Name = "$top")] int top, [FromQuery(Name = "$filter")] string filter, [FromQuery(Name = "$expand")] string expand, [FromQuery(Name = "$select")] string select, [FromQuery(Name = "$orderby")] string orderBy, [FromQuery(Name = "$apply")] string apply, [FromQuery(Name = "$format")] string format, [FromQuery(Name = "$skiptoken")] string skipToken, [FromQuery(Name = "$deltatoken")] string deltaToken)
        {
            // Use ToListAsync to allow orderby with navigation properties
            return await _userService.Get().ProjectTo<UserDto>(_mapper.ConfigurationProvider).ToListAsync();
        }

        [ODataRoute("({key})")]
        [EnableQuery(AllowedQueryOptions = AllowedQueryOptions.Expand | AllowedQueryOptions.Select)]
        [HttpGet("[controller]s({key})")]
        public SingleResult<UserDto> GetByKey([Required][FromODataUri] int key, [FromQuery(Name = "$expand")] string expand, [FromQuery(Name = "$select")] string select)
        {
            var userModel = _userService.Get(key);
            var result = userModel.ProjectTo<UserDto>(_mapper.ConfigurationProvider);
            return SingleResult.Create(result);
        }

        [ODataRoute]
        [HttpPost("[controller]s")]
        public async Task<IActionResult> Post([Required][FromBody] UserDto userDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userModel = _mapper.Map<UserModel>(userDto);
            var resultModel = await _userService.CreateAsync(userModel);
            var resultDto = _mapper.Map<UserDto>(resultModel);
            return Created(resultDto);
        }

        [ODataRoute("({key})")]
        [HttpPut("[controller]s({key})")]
        public async Task<IActionResult> Put([Required][FromODataUri] int key, [Required][FromBody] UserDto updatedUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedUserModel = _mapper.Map<UserModel>(updatedUser);
            var resultModel = await _userService.UpdateAsync(key, updatedUserModel);
            var resultDto = _mapper.Map<UserDto>(resultModel);
            return Updated(resultDto);
        }

        [ODataRoute("({key})")]
        [HttpDelete("[controller]s({key})")]
        public async Task<IActionResult> Delete([Required][FromODataUri] int key)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _userService.DeleteAsync(key);
            return NoContent();
        }
    }
}