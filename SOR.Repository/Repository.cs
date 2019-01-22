using SOR.DAL;
using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace SOR.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly SORDBContext _context;

        public Repository(SORDBContext context)
        {
            _context = context;
        }
        public async Task<ICollection<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<T> GetByCustomAsync(Expression<Func<T, bool>> expression)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(expression);
        }

        public async Task<ICollection<T>> GetAllCustomsAsync(Expression<Func<T, bool>> expression)
        {
            return await _context.Set<T>().Where(expression).ToListAsync();
        }

        public async Task<bool> CreateAsync(T t)
        {
            _context.Set<T>().Add(t);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(T t)
        {
            _context.Entry(t).State = EntityState.Modified;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var t = await GetByIdAsync(id);
            _context.Set<T>().Remove(t);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
