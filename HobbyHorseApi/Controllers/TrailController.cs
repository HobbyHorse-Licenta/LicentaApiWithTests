using HobbyHorseApi.Entities;
using HobbyHorseApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Web;

namespace HobbyHorseApi.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("trail")]
    public class TrailController : ControllerBase
    {
        private readonly ITrailService _service;

        public TrailController(ITrailService service)
        {
           _service = service;  
        }

        [HttpGet("allParkTrails")]
        public async Task<ActionResult<IEnumerable<ParkTrail>>> GetAllParkTrails()
        {
            try
            {
                var parkTrails = await _service.GetAllParkTrails();
                return Ok(parkTrails);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500);
            }
        }

        //[HttpPost("post/schedule")]
        //public async Task<ActionResult<Schedule>> PostSchedule(Schedule schedule)
        //{
        //    try
        //    {
        //        var postedSchedule = await _service.PostSchedule(schedule);
        //        return Ok(postedSchedule);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, ex.InnerException.Message);
        //    }
        //}

        //[HttpDelete("delete/schedule/{scheduleId}")]
        //public async Task<ActionResult> DeleteSchedule(string scheduleId)
        //{
        //    try
        //    {
        //        await _service.DeleteSchedule(scheduleId);
        //        return Ok();
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, ex.Message);
        //    }
        //}

    }
}