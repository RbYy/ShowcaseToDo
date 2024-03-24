using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowCaseToDo.Services
{
    internal interface IDataAccessService<T>: ISortable
    {
        Task<T> CreateAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        T Get(string id);
        Task<List<T>> GetAllAsync();
    }
    internal interface ISortable
    {
        Task MoveToNewPosition(int oldIndex, int newIndex);
    }
}
