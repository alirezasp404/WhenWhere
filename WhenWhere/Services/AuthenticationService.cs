using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;
using WhenWhere.Exceptions;
using WhenWhere.Models;
using WhenWhere.ServiceContracts;

namespace WhenWhere.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IHttpClientBuilder _httpClientBuilder;

        public AuthenticationService(IHttpClientBuilder httpClientBuilder)
        {
            this._httpClientBuilder = httpClientBuilder;
        }
        public async Task LoginAsync(LoginModel login)
        {
            var client = await _httpClientBuilder.GetHttpClientAsync();
            string json = JsonConvert.SerializeObject(login);
            StringContent loginContent = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync("/login", loginContent);
            if (!response.IsSuccessStatusCode)
            {
                throw new AuthenticationValidationException("SingIn Failed");
            }
            string content = await response.Content.ReadAsStringAsync();
            var loginRes = JsonConvert.DeserializeObject<LoginRes>(content);
            await _httpClientBuilder.SetTokensInStorage(loginRes);
        }
        public async Task RegisterAsync(RegisterModel register)
        {

            var client = await _httpClientBuilder.GetHttpClientAsync();

            string json = JsonConvert.SerializeObject(register);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync("register", content);
            if (!response.IsSuccessStatusCode) {
                throw new AuthenticationValidationException("SignUp Failed");
            }

        }
        public async Task LogoutAsync()
        {
            
            SecureStorage.Default.RemoveAll();
           _httpClientBuilder.ClearHttpClientData();
        }
    }
}
