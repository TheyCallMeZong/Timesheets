using System;
using System.Threading.Tasks;

namespace Timesheets.Data
{
    public interface IRepoBase<T> 
        where T : class
    {
        Task Create(T item);
        Task Delete(Guid id);
        Task Update(T item);
    }
}