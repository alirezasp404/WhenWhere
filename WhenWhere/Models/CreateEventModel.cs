using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhenWhere.Models
{
    public class CreateEventModel
    {
        public string? title { get; set; }

        public string? description { get; set; }

        public DateTime? expired_at { get; set; }

        public int? capacity { get; set; }

        public string? location { get; set; }

    }
}
