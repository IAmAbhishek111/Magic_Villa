using Magic_Villa_WebApp.Models.Dto;

namespace Magic_Villa_WebApp.Services.IServices
{
    public interface IVillaService
    {
        Task<T> GetAllAsync<T>(string token);
        Task<T> GetAsync<T>(int id , string token);
        Task<T> CreateAsync<T>(CreateVillaDto dto , string token);
        Task<T> UpdateAsync<T>(UpdateVillaDto dto , string token);
        Task<T> DeleteAsync<T>(int id , string token);
    }
}
