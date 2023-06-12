using FluentValidation;
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
    public class CountriesController : ControllerBase
    {
        private readonly ICountryRepository countryRepository;

        public CountriesController(ICountryRepository countryRepository)
        {
            this.countryRepository = countryRepository;
        }

        [HttpGet]
        [Route("listcountries")]
        [EnableCors]
        public async Task<ActionResult<Country[]>> ListCountries()
        {
            try
            {
                var countries = await countryRepository.GetCountriesAsync();
                var result = countries
                    .Select(c => CountryMapper.ToDto(c, includeRegions: false))
                    .ToArray();
                return Ok(result);
            }
            catch (ValidationException exception)
            {
                return BadRequest(exception.ResultMessage());
            }
        }

        [HttpGet]
        [Route("getcountry/{countryId}")]
        [EnableCors]
        public async Task<ActionResult<Country>> GetCountryById(
            Guid countryId,
            [FromQuery(Name = "regions")] bool? regions = null)
        {
            try
            {
                bool includeRegions = regions.GetValueOrDefault(true);
                var country = await countryRepository.GetCountryAsync(countryId, includeRegions);
                var result = CountryMapper.ToDto(country, includeRegions);
                return Ok(result);
            }
            catch (ValidationException exception)
            {
                return BadRequest(exception.ResultMessage());
            }
        }

        [HttpGet]
        [Route("getregion/{regionId}")]
        [EnableCors]
        public async Task<ActionResult<Region>> GetRegionById(Guid regionId)
        {
            try
            {
                var region = await countryRepository.GetRegionAsync(regionId);
                var result = CountryMapper.ToDto(region);
                return Ok(result);
            }
            catch (ValidationException exception)
            {
                return BadRequest(exception.ResultMessage());
            }
        }
    }
}
