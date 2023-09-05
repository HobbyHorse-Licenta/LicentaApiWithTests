using HobbyHorseApi.Entities;
using HobbyHorseApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HobbyHorseApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("test")]
    public class TestController : ControllerBase
    {

        [HttpGet]
        public ActionResult<string> Test()
        {
            try
            {
                return Ok("API is working gooood");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500);
            }
        }
    }
}