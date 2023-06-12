using System.Security.Claims;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SignUpService.DTO;
using SignUpService.Mappers;
using SignUpService.Repositories;
using SignUpService.Validation;

namespace SignUpServiceHost.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationsController : ControllerBase
    {
        private readonly ILocationRepository locationRepository;

        public LocationsController(ILocationRepository locationRepository)
        {
            this.locationRepository = locationRepository;
        }

        [HttpPost]
        [Route("set")]
        [EnableCors]
        [Authorize]
        public async Task<ActionResult> SetLocation([FromBody] Location location)
        {
            try
            {
                string userId = GetCurrentUserId();
                await locationRepository.CreateOrUpdateAccountLocationAsync(
                    userId, 
                    location.CountryId, 
                    location.RegionId);
                return Ok();
            }
            catch (ValidationException exception)
            {
                return BadRequest(exception.ResultMessage());
            }
        }

        [HttpGet]
        [Route("get")]
        [EnableCors]
        [Authorize]
        public async Task<ActionResult<Location>> GetLocation()
        {
            try
            {
                string userId = GetCurrentUserId();
                var location = await locationRepository.GetAccountLocationAsync(userId);
                var result = LocationMapper.ToDto(location);
                return Ok(result);
            }
            catch (ValidationException exception)
            {
                return BadRequest(exception.ResultMessage());
            }
        }

        private string GetCurrentUserId()
        {
            string? result = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return result ?? throw new Exception("Can't determine user id.");
        }
    }
}
