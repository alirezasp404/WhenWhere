using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhenWhere.Models;

namespace WhenWhere.ServiceContracts
{
    public interface IAuthenticationService
    {
        Task LoginAsync(LoginModel login);
        Task RegisterAsync(RegisterModel login);

        Task LogoutAsync();


    }
}
