using HobbyHorseApi.Entities;
using HobbyHorseApi.RabbitMQ;
using HobbyHorseApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OneSignalApi.Api;
using OneSignalApi.Model;
using OneSignalApi.Client;

using Expo.Server.Client;
using Expo.Server.Models;
//using expo_server_sdk_dotnet.Client;
//using expo_server_sdk_dotnet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using HobbyHorseApi.Utils;
using HobbyHorseApi.JsonConverters;

namespace HobbyHorseApi.Controllers
{
//    [Authorize]
    [ApiController]
    [Route("notification")]
    public class NotificationController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly PushApiClient _pushApiClient;
        private readonly SenderAndReceiver eventsSender = new SenderAndReceiver();

        public NotificationController(IUserService userService)
        {
            _userService = userService;
            _pushApiClient = new PushApiClient();
        }


        [HttpPost("post/notificationSkateProfiles")]
        public void PostNotifToSkateProfiles()
        {
            try
            {
                NotificationUtil.SendNotificationToUsersWithSkateProfiles(null, "Random notification", "This is a custom message");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        [HttpPost("post/{title}/{body}")]
        public void PostNotifToUsers(string title, string body)
        {
            try
            {
                NotificationUtil.SendNotificationToUsersWithUserIds(_userService, null, title, body);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}