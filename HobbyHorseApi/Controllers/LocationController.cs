using HobbyHorseApi.Entities;
using HobbyHorseApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Google.Protobuf.Reflection.SourceCodeInfo.Types;
using Location = HobbyHorseApi.Entities.Location;

namespace HobbyHorseApi.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("location")]
    public class LocationController : ControllerBase
    {
        private readonly ILocationService _service;

        public LocationController(ILocationService service)
        {
           _service = service;  
        }

        [HttpGet("{name}")]
        public async Task<ActionResult<Location>> GetLocationByName(string name)
        {
            try
            {
                var location = await _service.GetLocationByName(name);
                return Ok(location);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }
    }
}