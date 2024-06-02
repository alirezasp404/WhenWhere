using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhenWhere.Exceptions
{
    public class AuthenticationValidationException : Exception
    {
        public AuthenticationValidationException(string? message) : base(message) { }
    }
}
