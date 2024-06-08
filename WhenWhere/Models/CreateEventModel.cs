using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhenWhere.Models
{
    public class CreateEventModel
    {
        public string? Title { get; set; }

        public string? Description { get; set; }

        public DateTime? ExpiredAt { get; set; }

        public int? Capacity { get; set; }

        public string? Location { get; set; }

    }
}
