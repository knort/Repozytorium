using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SOR.BLL
{
    public interface IBaseService<T> where T : class
    {
        Task<bool> CreateAsync(T t);
        Task<bool> DeleteAsync(Guid id);
        Task<ICollection<T>> GetAllAsync();
        Task<ICollection<T>> GetAllCustomAsync(Expression<Func<T, bool>> expression);
        Task<T> GetByCustomAsync(Expression<Func<T, bool>> expression);
        Task<T> GetByIdAsync(Guid id);
        Task<bool> UpdateAsync(T t);
    }
}