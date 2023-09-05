using HobbyHorseApi.Entities;
using HobbyHorseApi.RabbitMQ;
using HobbyHorseApi.Services.Interfaces;
using HobbyHorseApi.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Web;

namespace HobbyHorseApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("schedule")]
    public class ScheduleController : ControllerBase
    {
        private readonly IScheduleService _service;
        private readonly SenderAndReceiver _rabbitMqSender;

        public ScheduleController(IScheduleService service, SenderAndReceiver rabbitMqSender)
        {
            _service = service;
            _rabbitMqSender = rabbitMqSender;
        }

        [HttpGet("allSchedules")]
        public async Task<ActionResult<IEnumerable<Schedule>>> GetAllSchedules()
        {
            try
            {
                var schedules = await _service.GetAllSchedules();
                return Ok(schedules);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500);
            }
        }

        [HttpGet("get/schedule/{scheduleId}")]
        public async Task<ActionResult<Schedule>> GetSchedule(string scheduleId)
        {
            try
            {
                var schedule = await _service.GetSchedule(scheduleId);
                return Ok(schedule);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500);
            }
        }

        [HttpGet("get/scheduleForSkateProfile/{skateProfileId}")]
        public async Task<ActionResult<IEnumerable<Schedule>>> GetScheduleForSkateProfile(string skateProfileId)
        {
            try
            {
                var schedule = await _service.GetScheduleForSkateProfile(skateProfileId);
                return Ok(schedule);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500);
            }
        }

        [HttpPost("post/schedule")]
        public async Task<ActionResult<Schedule>> PostSchedule(Schedule schedule)
        {
            try
            {
                var postedSchedule = await _service.PostSchedule(schedule);

                if(postedSchedule.SkateProfile.SkatePracticeStyle == "Aggresive Skating")
                {
                    Console.WriteLine("This is a schedule for aggresive skating profile\n" +
                        "Schedule was posted. Send schedule to EventGenerator to be added to exisitng aggresive skating events");
                    try
                    {
                        _rabbitMqSender.SendScheduleToBeAddedToExistingAggresiveEvents(postedSchedule);
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Coudn't send message");
                    }
                }
                return Ok(postedSchedule);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.InnerException.Message);
            }
        }

        [HttpPut("put/schedule/{scheduleId}")]
        public async Task<ActionResult<Schedule>> PutSchedule(Schedule schedule, string scheduleId)
        {
            try
            {
                var updatedSchedule = await _service.PutSchedule(schedule, scheduleId);
                return Ok(updatedSchedule);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.InnerException.Message);
            }
        }

        [HttpDelete("delete/schedule/{scheduleId}")]
        public async Task<ActionResult> DeleteSchedule(string scheduleId)
        {
            try
            {
                Schedule scheduleToDelete = await _service.GetSchedule(scheduleId);
                List<SkateProfile> skateProfilesToNotify = new List<SkateProfile>() { scheduleToDelete.SkateProfile };
                //notify them of event deletion
                Console.WriteLine("Notify user of schedule deletion");
                NotificationUtil.SendNotificationToUsersWithSkateProfiles(skateProfilesToNotify, "Schedule delete", "Your schedule got deleted");
                await _service.DeleteSchedule(scheduleId);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        //[HttpGet("skillRecomdations/{encodedPracticeStyle}/{encodedExperience}")]
        //public async Task<ActionResult<IEnumerable<SkillRecommendation>>> GetSkillRecomandations(string encodedPracticeStyle, string encodedExperience)
        //{
        //    string practiceStyle = HttpUtility.UrlDecode(encodedPracticeStyle);
        //    string experience = HttpUtility.UrlDecode(encodedExperience);

        //    try
        //    {
        //        var skillRecomandations = await _service.GetSkillRecomandations(practiceStyle, experience);
        //        return Ok(skillRecomandations);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, ex.Message);
        //    }
        //}


    }
}