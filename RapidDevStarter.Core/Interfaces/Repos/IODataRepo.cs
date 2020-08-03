using System.Linq;
using System.Threading.Tasks;

namespace RapidDevStarter.Core.Interfaces.Repos
{
    public interface IODataRepo<TModel>
    {
        Task<TModel> CreateAsync(TModel userModel);

        Task DeleteAsync(int key);

        IQueryable<TModel> Get();

        IQueryable<TModel> Get(int key);

        Task<TModel> UpdateAsync(int key, TModel updatedUserModel);
    }
}