using Magic_Villa_WebApp.Models.Dto;

namespace Magic_Villa_WebApp.Services.IServices
{
    public interface IVillaService
    {
        Task<T> GetAllAsync<T>();
        Task<T> GetAsync<T>(int id);
        Task<T> CreateAsync<T>(CreateVillaDto dto);
        Task<T> UpdateAsync<T>(UpdateVillaDto dto);
        Task<T> DeleteAsync<T>(int id);
    }
}
