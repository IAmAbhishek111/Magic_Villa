using Magic_Villa_Api.Models;
using System.Linq.Expressions;

namespace Magic_Villa_Api.Repository.IRepository
{
    public interface IVillaNumberRepository : IRepository<VillaNumber>
    {

        Task<VillaNumber> UpdateAsync(VillaNumber entity);

    }
}
