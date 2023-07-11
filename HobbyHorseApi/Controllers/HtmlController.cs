using HobbyHorseApi.Entities;
using HobbyHorseApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Text;

namespace HobbyHorseApi.Controllers
{
    [ApiController]
    [Route("")]
    public class HtmlController : ControllerBase
    {

        [HttpGet]
        public async Task<IActionResult> GetAllSkills()
        {
            string html = "<!DOCTYPE html>\r\n<html>\r\n<head>\r\n\t<title>Welcome</title>\r\n</head>\r\n<body>\r\n\t<h1>Welcome</h1>\r\n\t<p>Thank you for visiting this API. This was made to serve the HobbyHorse application available on Android and iOS</p>\r\n</body>\r\n</html>";
            return Content(html.ToString(), "text/html");
        }

        [HttpGet("favicon.ico")]
        public IActionResult GetFavicon()
        {
            return File("~/favicon.ico", "image/x-icon");
        }
    }
}