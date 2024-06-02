
using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using WhenWhere.Models;
using WhenWhere.ServiceContracts;

namespace WhenWhere.Services
{
    public class EventsService:IEventsService
    {
        private readonly IHttpClientBuilder _httpClientBuilder;
         public EventsService(IHttpClientBuilder httpClientBuilder)
        {
            _httpClientBuilder=httpClientBuilder;
        }
        //public static async Task CreateEvent(CreateEventModel createEvent, string? userId)
        //{

        //    string json = JsonSerializer.Serialize(createEvent, _serializerOptions);
        //    StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

        //    HttpResponseMessage response = await _client.PostAsync($"create_event/{userId}/", content);
        //}
        public  async Task<List<EventModel>?> GetAllEvents()
        {
            var eventModels = new List<EventModel>();
            var client = await _httpClientBuilder.GetHttpClientAsync();
            HttpResponseMessage response = await client.GetAsync("api/events");
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                eventModels = JsonConvert.DeserializeObject<List<EventModel>>(content);
            }
            return eventModels;
        }

        //public static async Task<List<EventModel>> GetAllRegisterdEvents(string url)
        //{
        //    List<EventModel>? RegisteredEvents = new List<EventModel>();

        //    HttpResponseMessage response = await _client.GetAsync(url);
        //    if (response.IsSuccessStatusCode)
        //    {
        //        string content = await response.Content.ReadAsStringAsync();
        //        var eventModels = JsonSerializer.Deserialize<List<RegisteredEventModel>>(content, _serializerOptions);

        //        if (eventModels.Count <= 0)
        //            return RegisteredEvents;

        //        var allEvents = await GetAllEvents("event_list/");
        //        if (allEvents.Count > 0 && eventModels.Count > 0)
        //        {
        //            foreach (int eventId in eventModels[0].events)
        //            {
        //                foreach (var item in allEvents)
        //                {
        //                    if (item.id == eventId)
        //                    {
        //                        RegisteredEvents.Add(item);
        //                    }
        //                }
        //            }
        //        }

        //    }

        //    return RegisteredEvents;
        //}
        //public static async Task<bool> RegisterEvent(Guid? eventId, string? userId)
        //{
        //    var registerEvent = new RegisterEventDetailsModel();
        //    registerEvent.events.Add(eventId);
        //    string json = JsonSerializer.Serialize(registerEvent, _serializerOptions);
        //    StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

        //    HttpResponseMessage response = await _client.PostAsync($"select_event/{userId}/", content);
            
        //    return response.IsSuccessStatusCode;

        //}
        //public static async Task<ProfileModel?> GetProfileModel(string? UserId)
        //{
        //    var profileModel = new ProfileModel();
        //    HttpResponseMessage response = await _client.GetAsync($"login/{UserId}");
        //    if (response.IsSuccessStatusCode)
        //    {
        //        string content = await response.Content.ReadAsStringAsync();
        //        profileModel = JsonSerializer.Deserialize<ProfileModel>(content, _serializerOptions);

        //    }
        //    return profileModel;

        //}
    }
}
