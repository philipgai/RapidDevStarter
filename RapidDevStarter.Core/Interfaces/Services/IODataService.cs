using System.Linq;
using System.Threading.Tasks;

namespace RapidDevStarter.Core.Interfaces.Services
{
    public interface IODataService<TModel>
    {
        Task<TModel> CreateAsync(TModel model);

        Task DeleteAsync(int key);

        IQueryable<TModel> Get();

        IQueryable<TModel> Get(int key);

        Task<TModel> UpdateAsync(int key, TModel updatedModel);
    }
}