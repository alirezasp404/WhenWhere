
using System;
using System.ComponentModel;
using System.Text;
using System.Text.Json;
using WhenWhere.Models;

namespace WhenWhere.Services
{
    public static class EventsService
    {
        private static readonly HttpClient _client = new HttpClient();
        //public static string BaseAddress = DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:8000/" : "http://localhost:8000/";
        private static JsonSerializerOptions _serializerOptions;
        static EventsService()
        {
        _client = new HttpClient()
            {
                BaseAddress = new Uri("http://localhost:8000/")
            };
            _serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
                WriteIndented = true
            };
        }
        public static async Task CreateEvent(CreateEventModel createEvent, string? userId)
        {

            string json = JsonSerializer.Serialize(createEvent, _serializerOptions);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _client.PostAsync($"create_event/{userId}/", content);
        }
        public static async Task<List<EventModel>?> GetAllEvents(string url)
        {
            var eventModels = new List<EventModel>();
            HttpResponseMessage response = await _client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                eventModels = JsonSerializer.Deserialize<List<EventModel>>(content, _serializerOptions);

            }
            return eventModels;
        }

        public static async Task<List<EventModel>> GetAllRegisterdEvents(string url)
        {
            List<EventModel>? RegisteredEvents = new List<EventModel>();

            HttpResponseMessage response = await _client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                var eventModels = JsonSerializer.Deserialize<List<RegisteredEventModel>>(content, _serializerOptions);

                if (eventModels?.Count <= 0)
                    return RegisteredEvents;

                var allEvents = await GetAllEvents("event_list/");
                if (allEvents?.Count > 0 && eventModels?.Count > 0)
                {
                    foreach (int eventId in eventModels[0]?.events)
                    {
                        foreach (var item in allEvents)
                        {
                            if (item.id == eventId)
                            {
                                RegisteredEvents.Add(item);
                            }
                        }
                    }
                }

            }

            return RegisteredEvents;
        }
        public static async Task<bool> RegisterEvent(int? eventId, string? userId)
        {
            var registerEvent = new RegisterEventDetailsModel();
            registerEvent.events.Add(eventId);
            string json = JsonSerializer.Serialize(registerEvent, _serializerOptions);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _client.PostAsync($"select_event/{userId}/", content);
            
            return response.IsSuccessStatusCode;

        }
        public static async Task<ProfileModel?> GetProfileModel(string? UserId)
        {
            var profileModel = new ProfileModel();
            HttpResponseMessage response = await _client.GetAsync($"login/{UserId}");
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                profileModel = JsonSerializer.Deserialize<ProfileModel>(content, _serializerOptions);

            }
            return profileModel;

        }
    }
}
