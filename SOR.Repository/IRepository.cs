using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SOR.Repository
{
    public interface IRepository<T> where T : class
    {
        Task<bool> CreateAsync(T t);
        Task<bool> DeleteAsync(Guid id);
        Task<ICollection<T>> GetAllAsync();
        Task<ICollection<T>> GetAllCustomsAsync(Expression<Func<T, bool>> expression);
        Task<T> GetByCustomAsync(Expression<Func<T, bool>> expression);
        Task<T> GetByIdAsync(Guid id);
        Task<bool> UpdateAsync(T t);
    }
}