using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhenWhere.Models
{
    public class LoginRes
    {
        public string? TokenType { get; set; }
        public string? AccessToken { get; set; }
        public double ExpiresIn { get; set; }
        public string? RefreshToken { get; set; }
        [JsonIgnore]
        public DateTime? ExpireTime { get; set; }



    }
}
