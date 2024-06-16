using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhenWhere.Models;
using WhenWhere.ServiceContracts;

namespace WhenWhere.Services
{
    public class UserInfoService : IUserInfoService
    {
        private readonly IHttpClientBuilder _httpClientBuilder;

        public UserInfoService(IHttpClientBuilder httpClientBuilder)
        {
            this._httpClientBuilder = httpClientBuilder;
        }
        public async Task<ProfileModel> GetProfile()
        {
            var profileModel = new ProfileModel();
            var client = await _httpClientBuilder.GetHttpClientAsync();
            HttpResponseMessage response = await client.GetAsync("api/profile");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("get profile failed");
            }
            string content = await response.Content.ReadAsStringAsync();
            profileModel = JsonConvert.DeserializeObject<ProfileModel>(content);
            return profileModel;
        }
    }
}
