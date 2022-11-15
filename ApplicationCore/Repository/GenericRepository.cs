using ApplicationCore.Models2;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        DbContext _context;
        public GenericRepository(DbContext context)
        {
            _context = context;
        }
        public async Task<TEntity> Create(TEntity entity)
        {
            try
            {
                if (entity == null) throw new ArgumentNullException("entity");
                _context.Add(entity);
                await _context.SaveChangesAsync();
                return entity; 
            }
            catch (Exception)
            {
                throw new Exception("GenericRepository Add Failed");
            }
        }

        public Task<bool> Delete(TEntity entity)
        {
            try
            {
                if (entity == null) throw new ArgumentNullException("entity");
                _context.Remove(entity);
                return Task<bool>.FromResult(true);
            }
            catch (Exception)
            {
                throw new Exception("GenericRepository Delete Failed");
            }
        }

        public Task<IEnumerable<TEntity>> Get(Expression<Func<TEntity, bool>>? filter = null, string? includeProperties = null)
        {
            var _dbSet = _context.Set<TEntity>();
            var query = _dbSet.AsQueryable();
            if (includeProperties != null)
            {
                foreach (string property in includeProperties.Split(","))
                {
                    query = query.Include(property);
                }
            }
            if (filter != null)
            {
                query = query.Where(filter);
            }
            return Task.FromResult(query.AsEnumerable());
        }


        public async Task<TEntity> Update(Expression<Func<TEntity, bool>> filter, TEntity entity)
        {
            try
            {
                if (entity == null) throw new ArgumentNullException("entity");
#pragma warning disable CS8073 // The result of the expression is always the same since a value of this type is never equal to 'null'
                var find = await Get(filter);
                var found = find.First();
                if(found != null)
                {
                    _context.Entry<TEntity>(found).CurrentValues.SetValues(entity);
                    await _context.SaveChangesAsync();
                    return found;
                }
                return null;
            }
            catch (Exception)
            {
                throw new Exception("GenericRepository Update Failed");
            }
        }
    }
}
