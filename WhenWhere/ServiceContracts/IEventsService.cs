using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhenWhere.Models;

namespace WhenWhere.ServiceContracts
{
    public interface IEventsService
    {
        Task<List<EventModel>?> GetAllEvents();
        Task<List<EventModel>?> GetCreatedEvents();
        Task<List<EventModel>?> GetRegisteredEvents();
        Task  RegisterEvent(Guid? eventId);
        Task CreateEvent(CreateEventModel eventModel);
    }
}
