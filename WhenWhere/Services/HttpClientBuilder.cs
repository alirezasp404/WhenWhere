using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;
using WhenWhere.Models;
using WhenWhere.ServiceContracts;

namespace WhenWhere.Services
{
    public class HttpClientBuilder : IHttpClientBuilder
    {
        private readonly HttpClient _httpClient;

        private bool _UserIsLoggedIn = SecureStorage.Default.GetAsync("TokenType") is not null;
        private LoginRes _tokens { get; set; } = new LoginRes();
        public HttpClientBuilder()
        {
            _UserIsLoggedIn = SecureStorage.Default?.GetAsync("TokenType") is not null;
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7152");

        }

        private async Task InitializeTokensFromStorage()
        {
            _tokens.AccessToken = await SecureStorage.Default.GetAsync("AccessToken");
            _tokens.RefreshToken = await SecureStorage.Default.GetAsync("RefreshToken");
            _tokens.TokenType = await SecureStorage.Default.GetAsync("TokenType");
            _tokens.ExpireTime = Convert.ToDateTime(await SecureStorage.GetAsync("ExpireTime"));
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(_tokens?.TokenType, _tokens?.AccessToken);

        }

        public async Task<bool> LoginAsync(LoginModel login)
        {
            string json = JsonConvert.SerializeObject(login);
            StringContent loginContent = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage LoginResponse = await _httpClient.PostAsync("/login", loginContent);
            if (!LoginResponse.IsSuccessStatusCode)
            {
                return false;
            }
            string content = await LoginResponse.Content.ReadAsStringAsync();
            var loginRes = JsonConvert.DeserializeObject<LoginRes>(content);
            await SetTokensInStorage(loginRes);
            return true;
        }

        private async Task RefreshToken()
        {
            var refreshModel=new RefreshModel() { RefreshToken= _tokens.RefreshToken };
            var json = JsonConvert.SerializeObject(refreshModel);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage refreshResponse = await _httpClient.PostAsync("/refresh", content);
            if (refreshResponse.IsSuccessStatusCode)
            {
                string refreshContent = await refreshResponse.Content.ReadAsStringAsync();
                var loginRes = JsonConvert.DeserializeObject<LoginRes>(refreshContent);
                await SetTokensInStorage(loginRes);
            }
            else
            {
                throw new AuthenticationException("refreshing access token failed");
            }
        }

        private async Task SetTokensInStorage(LoginRes loginRes)
        {
            loginRes.ExpireTime = DateTime.Now.AddSeconds(loginRes.ExpiresIn);
            await SecureStorage.SetAsync("AccessToken", loginRes.AccessToken);
            await SecureStorage.SetAsync("TokenType", loginRes.TokenType);
            await SecureStorage.SetAsync("RefreshToken", loginRes.RefreshToken);
            await SecureStorage.SetAsync("ExpireTime", loginRes.ExpireTime.ToString());
            await InitializeTokensFromStorage();
            _UserIsLoggedIn = true;
        }
        public async Task<HttpClient> GetHttpClientAsync()
        {

            if (!_UserIsLoggedIn)
            {
                throw new AuthenticationException("user is not logged in");
            }
            else
            {
                await InitializeTokensFromStorage();
            }

            if (_tokens?.ExpireTime <= DateTime.Now)
            {
                await RefreshToken();
            }
            return _httpClient;
        }

    }
}
