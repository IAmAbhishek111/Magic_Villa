using Magic_Villa_WebApp.Models;

namespace Magic_Villa_WebApp.Services.IServices
{
    public interface IBaseService
    {
        APIResponse responseModel { get; set; }

        Task <T> SendAsync<T>(APIRequest apiRequest);

    }
}
