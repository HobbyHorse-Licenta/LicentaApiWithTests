using HobbyHorseApi.Entities;
using HobbyHorseApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Text;

namespace HobbyHorseApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("sse")]
    public class SSEController : ControllerBase
    {

        [HttpGet("events")]
        public async Task<IActionResult> GetHeaderForSSE()
        {
            HttpContext.Response.ContentType = "text/event-stream";
            HttpContext.Response.Headers.Add("Cache-Control", "no-cache");
            HttpContext.Response.Headers.Add("Connection", "keep-alive");
            HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");
            await HttpContext.Response.Body.FlushAsync();

            return new EmptyResult();
        }

    }
}