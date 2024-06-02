
using WhenWhere.Models;

namespace WhenWhere.ServiceContracts
{
    public interface IHttpClientBuilder
    {
        Task<bool> LoginAsync(LoginModel login);

        Task<HttpClient> GetHttpClientAsync();
    }
}
