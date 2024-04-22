using Magic_Villa_Api.Models;
using System.Linq.Expressions;

namespace Magic_Villa_Api.Repository.IRepository
{
    public interface IVillaRepository : IRepository<Villa>
    {
        Task<Villa> UpdateAsync(Villa entity);
    }
}
