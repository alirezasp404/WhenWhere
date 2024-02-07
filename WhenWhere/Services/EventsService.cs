
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
        public static async Task<List<EventModel>?> GetAllEvents()
        {
            var eventModels = new List<EventModel>();
            HttpResponseMessage response = await _client.GetAsync("event_list/");
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                eventModels = JsonSerializer.Deserialize<List<EventModel>>(content, _serializerOptions);
                
            }
            return eventModels;
        }
    }
}
