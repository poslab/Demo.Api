using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demo.Repository.Interfaces
{
    public interface IBaseRepository<T, in TSelectModel>
    {
        Task<int> InsertAsync(T item);
        Task<int> UpdateAsync(T item);
        Task<int> DeleteAsync(object whereClause);
        Task<List<T>> GetAsync(TSelectModel whereClause);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetSingleRecordAsync(TSelectModel whereClause);
    }
}
