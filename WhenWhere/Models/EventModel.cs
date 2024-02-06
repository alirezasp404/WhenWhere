
namespace WhenWhere.Models
{
    public class EventModel
    {
        public int Id { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        public DateTime DateTime { get; set; }

        public int Capacity {  get; set; }

        public string? Location {  get; set; }

        public string? Creator {  get; set; }
    }
}
