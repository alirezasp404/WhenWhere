
using System.Text;
using System.Text.Json;
using WhenWhere.Models;

namespace WhenWhere.Services
{
    public static class EventsService
    {
        private static readonly HttpClient _client = new HttpClient();
        private static JsonSerializerOptions _serializerOptions;
        static EventsService()
        {
            _client = new HttpClient()
            {
                BaseAddress = new Uri("http://127.0.0.1:8000/")
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
               var eventModels =  JsonSerializer.Deserialize<List<RegisteredEventModel>>(content, _serializerOptions);

                var allEvents= await GetAllEvents("event_list/");
               
                foreach (int eventId in eventModels[0].events)
                {
                    foreach (var item in allEvents)
                    {
                        if(item.id == eventId)
                        {
                            RegisteredEvents.Add(item);
                        }
                    }
                }
                
            }
            return RegisteredEvents;
        }
    }
}
