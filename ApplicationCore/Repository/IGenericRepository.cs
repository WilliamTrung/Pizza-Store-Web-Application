using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Repository
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        //C
        public Task<TEntity> Create(TEntity entity);
        //R
        public Task<IEnumerable<TEntity>> Get(Expression<Func<TEntity,bool>>? filter = null, string? includeProperties = null);
        //U
        public Task<TEntity> Update(TEntity entity);
        //D
        public Task<bool> Delete(TEntity entity);
    }
}
