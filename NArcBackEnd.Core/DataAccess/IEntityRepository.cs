using NArcBackEnd.Core.Entities;
using System.Linq.Expressions;

namespace NArcBackEnd.Core.DataAccess
{
    public interface IEntityRepository<TEntity> where TEntity : class, new()
    {
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        List<TEntity> GetAll(Expression<Func<TEntity, bool>> expression = null);
        TEntity Get(Expression<Func<TEntity, bool>> expression);
    }
}
