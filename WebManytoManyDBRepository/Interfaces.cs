using Microsoft.EntityFrameworkCore;

namespace WebManytoManyDBRepository
{
    public interface IMergeable<TEntity>
    {
        void Merge(TEntity entity);
    }

    public interface IEntity<TEntityIdType>
    {
        TEntityIdType Id { set;  get; }

        bool HasIdenticalId(TEntityIdType id);
    }

    public interface IDataRepository<TEntity, TEntityIdType> where TEntity : class, IEntity<TEntityIdType>
    {
        Task<TEntityIdType> InsertAsync(DbContext context, TEntity entity);

        IEnumerable<TEntity> GetAll(DbContext context, string includeProperties = "");

        Task<int> DeleteAllAsync(DbContext context);

        Task<TEntityIdType> Update(DbContext context,TEntity entity, string includeProperties="");

        Task<TEntity> GetByIdAsync(DbContext context, TEntityIdType id, string includeProperties="");
        
        Task<TEntityIdType> DeleteByIdAsync(DbContext context, TEntityIdType id);

    }
}
