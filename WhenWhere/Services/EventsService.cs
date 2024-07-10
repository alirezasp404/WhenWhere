
using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using WhenWhere.Models;
using WhenWhere.ServiceContracts;

namespace WhenWhere.Services
{
    public class EventsService : IEventsService
    {
        private readonly IHttpClientBuilder _httpClientBuilder;
        public EventsService(IHttpClientBuilder httpClientBuilder)
        {
            _httpClientBuilder = httpClientBuilder;
        }
        public async Task CreateEvent(CreateEventModel createEvent)
        {
            string json = JsonConvert.SerializeObject(createEvent);
            var client = await _httpClientBuilder.GetHttpClientAsync();
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync($"api/events", content);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("unable to create new event");
            }
        }
        public async Task DeleteEvent(Guid? eventId)
        {
            
            var client = await _httpClientBuilder.GetHttpClientAsync();
            HttpResponseMessage response = await client.DeleteAsync($"api/events/{eventId}");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("unable to delete selected event");
            }
        }
        private async Task<List<EventModel>?> GetEvents(string url)
        {

            var eventModels = new List<EventModel>();
            var client = await _httpClientBuilder.GetHttpClientAsync();
            HttpResponseMessage response = await client.GetAsync($"api/{url}");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Get events failed.");
            }
            string content = await response.Content.ReadAsStringAsync();
            eventModels = JsonConvert.DeserializeObject<List<EventModel>>(content);
            return eventModels;
        }
        public async Task<List<EventModel>?> GetAllEvents()
        {

            return await GetEvents("events");
        }
        public async Task<List<EventModel>?> GetCreatedEvents()
        {

            return await GetEvents("events/createdbyuser");
        }
        public async Task<List<EventModel>?> GetRegisteredEvents()
        {

            return await GetEvents("registerevents");
        }

        public  async Task RegisterEvent(Guid? eventId)
        {
            var json=JsonConvert.SerializeObject(eventId);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            var client = await _httpClientBuilder.GetHttpClientAsync();
            HttpResponseMessage response = await client.PostAsync("api/registerevents", content);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("unable to register the event");
            }
        }

    }
}
