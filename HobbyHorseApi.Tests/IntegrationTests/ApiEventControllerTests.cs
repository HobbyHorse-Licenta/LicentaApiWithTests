using FluentAssertions;
using HobbyHorseApi.Entities;
using HobbyHorseApi.JsonConverters;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;

namespace HobbyHorseApi.Tests.Controllers
{
    public class ApiEventControllerTests
    {
        private readonly string _apiUrl;
        public ApiEventControllerTests()
        {
            string pipelineStage = Environment.GetEnvironmentVariable("HEROKU_PIPELINE_STAGE");
            if (pipelineStage == "staging")
            {
                _apiUrl = "https://hobby-horse-api-staging-c015da26324c.herokuapp.com";
            }
            else if(pipelineStage == "production")
            {
                _apiUrl = "https://hobby-horse-api-afcf33169b5e.herokuapp.com";
            }
            else
            {
                //_apiUrl = "https://localhost:7085"; //if API runs locally
                _apiUrl = "https://hobby-horse-api-staging-c015da26324c.herokuapp.com"; //if API runs in staging
            }
        }

        
        [Fact]
        public async void EventController_GetAllEvents_ReturnsArray()
        {
            //arange
            string getAllEventsUrl = _apiUrl + $"/event/getAllEvents";

            //act
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "JBXWEYTZJBXXE43FI5SW4ZLSMF2G64Q=");
                HttpResponseMessage response = await httpClient.GetAsync(getAllEventsUrl);

                string responseContent;
                if (response.IsSuccessStatusCode)
                {
                    responseContent = await response.Content.ReadAsStringAsync();
                }
                else
                {
                    responseContent = null;
                }

                //assert
                response.StatusCode.Should().Be(HttpStatusCode.OK);
                //responseContent.Should().NotBe(null);

                //act
                JsonSerializerOptions options = new JsonSerializerOptions();
                var events = JsonSerializer.Deserialize<List<Event>>(responseContent, options);

                //assert
                events.Should().BeOfType<List<Event>>();
            }
        }
    }
}