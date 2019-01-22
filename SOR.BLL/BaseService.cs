using SOR.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SOR.BLL
{
    public class BaseService<T> : IBaseService<T> where T : class
    {
        private readonly IRepository<T> _repository;

        public BaseService(IRepository<T> repository)
        {
            _repository = repository;
        }

        public virtual async Task<ICollection<T>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public virtual async Task<ICollection<T>> GetAllCustomAsync(Expression<Func<T,bool>> expression)
        {
            return await _repository.GetAllCustomsAsync(expression);
        }

        public virtual async Task<T> GetByCustomAsync(Expression<Func<T,bool>> expression)
        {
            return await _repository.GetByCustomAsync(expression);
        }

        public virtual async Task<T> GetByIdAsync(Guid id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public virtual async Task<bool> CreateAsync(T t)
        {
            return await _repository.CreateAsync(t);
        }

        public virtual async Task<bool> UpdateAsync(T t)
        {
            return await _repository.UpdateAsync(t);
        }

        public virtual async Task<bool> DeleteAsync(Guid id)
        {
            return await _repository.DeleteAsync(id);
        }

    }
}
