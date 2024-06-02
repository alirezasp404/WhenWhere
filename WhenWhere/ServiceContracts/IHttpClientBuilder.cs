
using WhenWhere.Models;

namespace WhenWhere.ServiceContracts
{
    public interface IHttpClientBuilder
    {

        Task<HttpClient> GetHttpClientAsync();
        Task SetTokensInStorage(LoginRes loginRes);
        void ClearHttpClientData();
    }
}
