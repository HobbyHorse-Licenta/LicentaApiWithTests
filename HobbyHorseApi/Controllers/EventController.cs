using HobbyHorseApi.Entities;
using HobbyHorseApi.RabbitMQ;
using HobbyHorseApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Expo.Server.Client;
using Expo.Server.Models;
//using expo_server_sdk_dotnet.Client;
//using expo_server_sdk_dotnet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using HobbyHorseApi.Utils;
using HobbyHorseApi.JsonConverters;
using Newtonsoft.Json;

namespace HobbyHorseApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("event")]
    public class EventController : ControllerBase
    {
        private readonly IEventService _service;
        private readonly PushApiClient _pushApiClient;
        private readonly SenderAndReceiver _eventsSender;
        private readonly INotificationService _notifService;


        public EventController(IEventService service, SenderAndReceiver eventsSender, INotificationService notifService)
        {
           _service = service;
            _eventsSender = eventsSender;
            _pushApiClient = new PushApiClient();
            _notifService = notifService;
        }

        [HttpPost("postWithModifications")]
        public async Task<ActionResult<AggresiveEvent>> PostEventWithModifications(AggresiveEvent evnt)
        {
            try
            {
                Console.WriteLine("Entered post Aggresive Event");
                _eventsSender.SendPartialAggresiveEvent(evnt);
                return Ok(evnt);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500);
            }
        }

        [HttpPost("post")]
        public async Task<ActionResult<Event>> PostEvent(Event evnt)
        {
            try
            {

                Console.WriteLine("I'm in controller, about to post the event");
                var returnedEvent = await _service.PostEvent(evnt);

                //notifiy all users of the new event
                Console.WriteLine("Sending notifications");
                if (evnt.RecommendedSkateProfiles.Count() > 0)
                {
                    if(evnt.RecommendedSkateProfiles[0].User != null)
                    {
                        Console.WriteLine("HERE1");
                        NotificationUtil.SendNotificationToUsersWithSkateProfiles(evnt.RecommendedSkateProfiles, "New Event", "You have new event suggestions");

                    }
                    else
                    {
                        List<string> notifTokens = new List<string>();

                        foreach (SkateProfile skateProfile in evnt.RecommendedSkateProfiles)
                        {
                            string token = await _notifService.GetNotificationTokenForSkateProfile(skateProfile.Id);
                            notifTokens.Add(token);
                        }

                        if (notifTokens.Count > 0)
                        {
                            Console.WriteLine("HERE2");

                            NotificationUtil.SendNotificationToUsersWithNotifToken(notifTokens, "New Event", "You have new event suggestions");
                        }
                    }
                }

                if(evnt.SkateProfiles.Count() > 0)
                {
                    if(evnt.SkateProfiles[0].User != null)
                    {
                        Console.WriteLine("HERE3");

                        NotificationUtil.SendNotificationToUsersWithSkateProfiles(evnt.SkateProfiles, "Event set up", "Your event has been created. Suitable skaters have been invited");

                    }
                    else
                    {
                        List<string> notifTokens = new List<string>();

                        foreach (SkateProfile skateProfile in evnt.SkateProfiles)
                        {
                            string token = await _notifService.GetNotificationTokenForSkateProfile(skateProfile.Id);
                            notifTokens.Add(token);
                        }

                        if (notifTokens.Count > 0)
                        {
                            Console.WriteLine("HERE4");

                            NotificationUtil.SendNotificationToUsersWithNotifToken(notifTokens, "Event set up", "Your event has been created. Suitable skaters have been invited");
                        }
                    }
                }

                return Ok(returnedEvent);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException.Message);
                return StatusCode(500);
            }
        }

        [HttpPut("put")]
        public async Task<ActionResult<Event>> PutEvent(Event evnt)
        {
            try
            {
                var returnedEvent = await _service.PutEvent(evnt);

                List<SkateProfile> allSkateProfiles = evnt.RecommendedSkateProfiles.Concat(evnt.SkateProfiles).ToList();

                bool needToRetrieveNotifToken = true;
                if(allSkateProfiles.Count() > 0 && allSkateProfiles[0].User != null)
                {
                    NotificationUtil.SendNotificationToUsersWithSkateProfiles(allSkateProfiles, "Event changes", "Existing event changed");
                }
                else
                {
                    List<string> notifTokens = new List<string>();

                    foreach(SkateProfile skateProfile in allSkateProfiles)
                    {
                       string token = await _notifService.GetNotificationTokenForSkateProfile(skateProfile.Id);
                        notifTokens.Add(token);
                    }

                    if(notifTokens.Count > 0)
                    {
                        NotificationUtil.SendNotificationToUsersWithNotifToken(notifTokens, "Event changes", "Existing event changed");
                    }
                }


                return Ok(returnedEvent);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500);
            }
        }

        [HttpGet("get/joinSkateProfile/{eventId}/{skateProfileId}")]
        public async Task<ActionResult<Event>> JoinSkateProfile(string eventId, string skateProfileId)
        {
            try
            {
                var returnedEvent = await _service.JoinSkateProfile(eventId, skateProfileId);
                return Ok(returnedEvent);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500);
            }
        }

        [HttpGet("get/leaveSkateProfile/{eventId}/{skateProfileId}")]
        public async Task<ActionResult<Event>> LeaveSkateProfile(string eventId, string skateProfileId)
        {
            try
            {
                var returnedEvent = await _service.LeaveSkateProfile(eventId, skateProfileId);
                return Ok(returnedEvent);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500);
            }
        }

        [HttpDelete("delete/{eventId}")]
        public async Task<ActionResult> DeleteEvent(string eventId)
        {
            try
            {
                var participants = await _service.getParticipantsForEvent(eventId);
                var suggestedParticipants = await _service.getSuggestedParticipantsForEvent(eventId);

                //notify them of event deletion
                NotificationUtil.SendNotificationToUsersWithSkateProfiles(participants.Concat(suggestedParticipants).ToList(), "Event delete", "An event got deleted");

                await _service.DeleteEvent(eventId);
                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500);
            }
        }

        [HttpGet("getAllEvents")]
        public async Task<ActionResult<IEnumerable<Event>>> GetAllEvents()
        {
            try
            {
                var allEvents = await _service.GetAllEvents();
                return Ok(allEvents);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500);
            }
        }



        [HttpGet("getRecommendedEvents/skateProfile/{skateProfileId}")]
        public async Task<ActionResult<IEnumerable<Event>>> GetRecommendedEventsForSkateProfile(string skateProfileId)
        {
            try
            {
                var recommendedEvents = await _service.GetRecommendedEventsForSkateProfile(skateProfileId);
                return Ok(recommendedEvents);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500);
            }
        }

        [HttpGet("getEvents/skateProfile/{skateProfileId}")]
        public async Task<ActionResult<IEnumerable<Event>>> GetEventsForSkateProfile(string skateProfileId)
        {
            try
            {
                var events = await _service.GetEventsForSkateProfile(skateProfileId);
                return Ok(events);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500);
            }
        }

        [HttpGet("getEvents/user/{userId}")]
        public async Task<ActionResult<IEnumerable<Event>>> GetEventsForUser(string userId)
        {
            try
            {
                var events = await _service.GetEventsForUser(userId);
                return Ok(events);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500);
            }
        }

        [HttpGet("getParticipants/{eventId}")]
        public async Task<ActionResult<IEnumerable<SkateProfile>>> getParticipantsForEvent(string eventId)
        {
            try
            {
                var participants = await _service.getParticipantsForEvent(eventId);
                return Ok(participants);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500);
            }
        }

        [HttpGet("getSuggestedParticipants/{eventId}")]
        public async Task<ActionResult<IEnumerable<SkateProfile>>> getSuggestedParticipantsForEvent(string eventId)
        {
            try
            {
                var participants = await _service.getSuggestedParticipantsForEvent(eventId);
                return Ok(participants);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500);
            }
        }



    }
}