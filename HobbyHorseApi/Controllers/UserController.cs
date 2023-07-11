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
    [Route("user")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
           _service = service;  
        }

        [HttpDelete("delete/{userId}")]
        public async Task<ActionResult> DeleteUser(string userId)
        {
            try
            {
                await _service.DeleteUser(userId);
                return Ok();
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message); 
            }
        }

        [HttpPut("put/{userId}")]
        public async Task<ActionResult<User>> PutUser(string userId, User user)
        {
            try
            {
                var result = await _service.PutUser(userId, user);
                return Ok(result);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.InnerException.Message);
            }
        }

        [HttpPost("post")]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            try
            {
                var returnedUser = await _service.PostUser(user);
                return Ok(returnedUser);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("getAll")]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
        {
            try
            {
                var users = await _service.GetAllUsers();
                return Ok(users);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("get/{userId}")]
        public async Task<ActionResult<User>> GetUser(string userId)
        {
            try
            {
                var user = await _service.GetUser(userId);
                return Ok(user);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("getBasicInfo/{userId}")]
        public async Task<ActionResult<User>> GetUserWithBasicInfo(string userId)
        {
            try
            {
                var user = await _service.GetUserWithBasicInfo(userId);
                return Ok(user);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }
    }
}