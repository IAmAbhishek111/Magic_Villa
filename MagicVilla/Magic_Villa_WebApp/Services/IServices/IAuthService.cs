using Magic_Villa_WebApp.Models.Dto;

namespace Magic_Villa_WebApp.Services.IServices
{
    public interface IAuthService
    {
        Task<T> LoginAsync<T>(LoginRequestDto objToCreate);
        Task<T> RegisterAsync<T>(RegistrationRequestDto objToCreate);

    }
}
