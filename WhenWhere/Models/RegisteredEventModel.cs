using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhenWhere.Models
{
    public class RegisteredEventModel
    {
        public int id { get; set; }
        public string? event_picker { get; set; }
        public List<int>? events { get; set; }
         
    }
}
