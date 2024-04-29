using Magic_Villa_WebApp.Models.Dto;

namespace Magic_Villa_WebApp.Services.IServices
{
    public interface IVillaNumberService
    {
        Task<T> GetAllAsync<T>(string token);
        Task<T> GetAsync<T>(int id, string token);
        Task<T> CreateAsync<T>(VillaNumberCreateDto dto, string token);
        Task<T> UpdateAsync<T>(VillaNumberUpdateDto  dto, string token);
        Task<T> DeleteAsync<T>(int id, string token);
    }
}
