using Magic_Villa_Api.Models;
using System.Linq.Expressions;

namespace Magic_Villa_Api.Repository.IRepository
{
    public interface IVillaRepository
    {
        Task<List<Villa>> GetAll(Expression<Func<Villa, bool>> filter = null);
        Task<Villa> Get(Expression<Func<Villa, bool>> filter = null, bool tracked = true);
        Task Create(Villa entity);
        Task Remove(Villa entity);
        Task Save();
    }
}
