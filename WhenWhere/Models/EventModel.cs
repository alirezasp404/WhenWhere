
namespace WhenWhere.Models
{
    public class EventModel
    {
        public int? id { get; set; }

        public string? title { get; set; }

        public string? description { get; set; }

        public DateTime? expired_at { get; set; }

        public int? capacity {  get; set; }

        public string? location {  get; set; }

        public string? event_maker {  get; set; }

        public override bool Equals(object? obj)
        {
            if (obj == null && obj.GetType() == typeof(EventModel))
            {
                return false;
            }
            var eventModel = (EventModel)obj;
            return id == eventModel.id;
        }
    }
}
