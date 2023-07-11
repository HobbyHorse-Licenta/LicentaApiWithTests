using HobbyHorseApi.Entities;
using HobbyHorseApi.Repositories.Interfaces;
using HobbyHorseApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HobbyHorseApi.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("skateProfile")]
    public class SkateProfileController : ControllerBase
    {
        private readonly ISkateProfileService _service;

        public SkateProfileController(ISkateProfileService service)
        {
           _service = service;  
        }


        [HttpPost("post")]
        public async Task<ActionResult<SkateProfile>> PostSkateProfile(SkateProfile skateProfile)
        {
            try
            {
                var returnedSkateProfile = await _service.PostSkateProfile(skateProfile);
                return Ok(returnedSkateProfile);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("getAll")]
        public async Task<ActionResult<List<SkateProfile>>> getAllSkateProfiles()
        {
            try
            {
                var returnedSkateProfiles = await _service.getAllSkateProfiles();
                return Ok(returnedSkateProfiles);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("get/{skateProfileId}")]
        public async Task<ActionResult<SkateProfile>> getSkateProfile(string skateProfileId)
        {
            try
            {
                var returnedSkateProfile = await _service.getSkateProfile(skateProfileId);
                return Ok(returnedSkateProfile);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }
    }
}