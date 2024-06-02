
namespace WhenWhere.Models
{
    public class EventModel
    {
        public Guid? Id { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        public DateTime? ExpiredAt { get; set; }
        
        public int? Capacity {  get; set; }

        public string? Location {  get; set; }

        public string? EventCreator {  get; set; }

        public override bool Equals(object? obj)
        {
            if (obj == null && obj?.GetType() == typeof(EventModel))
            {
                return false;
            }
            var eventModel = (EventModel?)obj;
            return Id == eventModel?.Id;
        }
    }
}
