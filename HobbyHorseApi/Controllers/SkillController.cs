using HobbyHorseApi.Entities;
using HobbyHorseApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Web;

namespace HobbyHorseApi.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("skill")]
    public class SkillController : ControllerBase
    {
        private readonly ISkillService _service;

        public SkillController(ISkillService service)
        {
           _service = service;  
        }

        [HttpGet("allSkills")]
        public async Task<ActionResult<IEnumerable<Skill>>> GetAllSkills()
        {
            try
            {
                var skills = await _service.GetAllSkills();
                return Ok(skills);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500);
            }
        }

        [HttpGet("skillRecomdations/{encodedPracticeStyle}/{encodedExperience}")]
        public async Task<ActionResult<IEnumerable<SkillRecommendation>>> GetSkillRecomandations(string encodedPracticeStyle, string encodedExperience)
        {
            string practiceStyle = HttpUtility.UrlDecode(encodedPracticeStyle);
            string experience = HttpUtility.UrlDecode(encodedExperience);

            try
            {
                var skillRecomandations = await _service.GetSkillRecomandations(practiceStyle, experience);
                return Ok(skillRecomandations);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("post/assignedSkill")]
        public async Task<ActionResult<AssignedSkill>> PostAssignedSkill(AssignedSkill assignedSkill)
        {
            try
            {
                var postedAssignedSkill = await _service.PostAssignedSkill(assignedSkill);
                return Ok(postedAssignedSkill);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("put/assignedSkill")]
        public async Task<ActionResult<AssignedSkill>> PutAssignedSkill(AssignedSkill assignedSkill)
        {
            try
            {
                var putAssignedSkill = await _service.PutAssignedSkill(assignedSkill);
                return Ok(putAssignedSkill);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("delete/assignedSkill/{assignedSkillId}/fromSkateProfile/{skateProfileId}")]
        public async Task<ActionResult> DeleteAssignedSkill(string assignedSkillId, string skateProfileId)
        {
            try
            {
                await _service.DeleteAssignedSkill(assignedSkillId, skateProfileId);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}