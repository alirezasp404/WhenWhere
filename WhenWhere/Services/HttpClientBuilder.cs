using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Security.Authentication;
using System.Text;
using WhenWhere.Models;
using WhenWhere.ServiceContracts;

namespace WhenWhere.Services
{
    public class HttpClientBuilder : IHttpClientBuilder
    {
        private readonly HttpClient _httpClient;
        private bool _TokensInitialized { get; set; } = false;
        private LoginRes _tokens { get; set; } = new LoginRes();
        public HttpClientBuilder()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("http://192.168.100.10:5138");
        }

        private async Task InitializeTokensFromStorage()
        {
            _tokens.AccessToken = await SecureStorage.Default.GetAsync("AccessToken");
            _tokens.RefreshToken = await SecureStorage.Default.GetAsync("RefreshToken");
            _tokens.TokenType = await SecureStorage.Default.GetAsync("TokenType");
            _tokens.ExpireTime = Convert.ToDateTime(await SecureStorage.GetAsync("ExpireTime"));
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(_tokens?.TokenType, _tokens?.AccessToken);
            _TokensInitialized = true;
        }

        private async Task RefreshToken()
        {
            var refreshModel = new RefreshModel() { RefreshToken = _tokens.RefreshToken };
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

        public async Task SetTokensInStorage(LoginRes loginRes)
        {
            loginRes.ExpireTime = DateTime.Now.AddSeconds(loginRes.ExpiresIn);
            await SecureStorage.SetAsync("AccessToken", loginRes.AccessToken);
            await SecureStorage.SetAsync("TokenType", loginRes.TokenType);
            await SecureStorage.SetAsync("RefreshToken", loginRes.RefreshToken);
            await SecureStorage.SetAsync("ExpireTime", loginRes.ExpireTime.ToString());
            await InitializeTokensFromStorage();
        }
        public async Task<HttpClient> GetHttpClientAsync()
        {
            if (await SecureStorage.Default.GetAsync("TokenType") is not null)
            {
                if (!_TokensInitialized)
                {
                    await InitializeTokensFromStorage();
                }

                if (_tokens?.ExpireTime <= DateTime.Now)
                {
                    await RefreshToken();
                }
            }

            return _httpClient;
        }
        public void ClearHttpClientData()
        {
            _httpClient.DefaultRequestHeaders.Authorization = null;
            _tokens = new LoginRes();
        }
    }
}
