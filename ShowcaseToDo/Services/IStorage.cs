using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowCaseToDo.Services
{
    internal interface IStorage<T>: ISortable
    {
        Task<T> CreateAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        T Get(int id);
        Task<IEnumerable<T>> GetAllAsync();
    }
    internal interface ISortable
    {
        Task MoveToNewPosition(int oldIndex, int newIndex);
    }
}
