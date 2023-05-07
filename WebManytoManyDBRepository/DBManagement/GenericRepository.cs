using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System.Linq.Expressions;

namespace WebManytoManyDBRepository.DBManagement
{
    public class GenericRepository<TEntity, TEntityIdType> : IDataRepository<TEntity, TEntityIdType> where TEntity : class, IEntity<TEntityIdType>
    {
        public GenericRepository()
        {
        }

        public async Task<int> DeleteAllAsync(DbContext context)
        {
            var deletedEntities = await context.Set<TEntity>().ExecuteDeleteAsync();
            
            return deletedEntities;
        }

        public async Task<TEntityIdType> DeleteByIdAsync(DbContext context, TEntityIdType id)
        {
            var deletedEntities = await context.Set<TEntity>().Where(_ => _.HasIdenticalId(id)).ExecuteDeleteAsync();

            //var deletedEntities = await context.Set<TEntity>().Where(_ => _.Id.ToString() == id.ToString()).ExecuteDeleteAsync();
            return id;
        }

        public IEnumerable<TEntity> GetAll(DbContext context, string includeProperties ="")
        {
            var query = context.Set<TEntity>().AsNoTracking();

            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            var result = query.ToList();
            return result;
        }

        public async Task<TEntity> GetByIdAsync(DbContext context, TEntityIdType id, string includeProperties = "")
        {
            TEntity result = null;

            if (string.IsNullOrEmpty(includeProperties))
            {
                await context.Set<TEntity>().FindAsync(id);
            }
            else 
            {
                var query = context.Set<TEntity>().AsNoTracking().Where(_ => _.Id.ToString() == id.ToString());
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }

                result = query.FirstOrDefault();
            }

            return result;
        }

        public async Task<TEntityIdType> InsertAsync(DbContext context, TEntity entity)
        {
            await context.Set<TEntity>().AddAsync(entity);
            await context.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<TEntityIdType> Update(DbContext context, TEntity entity, string includeProperties="")
        {
            if (entity is IMergeable<TEntity>)
            {
                var query = context.Set<TEntity>().Where(_ => _.Id.ToString() == entity.Id.ToString());
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }

                var dbEntity = query.FirstOrDefault();
                if (dbEntity != null)
                {
                    ((IMergeable<TEntity>)dbEntity).Merge(entity);
                }
            }
            else
            {
                context.Set<TEntity>().Update(entity);
            }

            await context.SaveChangesAsync();

            return entity.Id;
        }

        //public virtual IEnumerable<TEntity> Get(
        //    Expression<Func<TEntity, bool>> filter = null,
        //    Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        //    string includeProperties = "")
        //{
        //    IQueryable<TEntity> query = dbSet;

        //    if (filter != null)
        //    {
        //        query = query.Where(filter);
        //    }

        //    foreach (var includeProperty in includeProperties.Split
        //        (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
        //    {
        //        query = query.Include(includeProperty);
        //    }

        //    if (orderBy != null)
        //    {
        //        return orderBy(query).ToList();
        //    }
        //    else
        //    {
        //        return query.ToList();
        //    }
        //}

        //public virtual TEntity GetByID(object id)
        //{
        //    return dbSet.Find(id);
        //}

        //public virtual void Insert(TEntity entity)
        //{
        //    dbSet.Add(entity);
        //}

        //public virtual void Delete(object id)
        //{
        //    TEntity entityToDelete = dbSet.Find(id);
        //    Delete(entityToDelete);
        //}

        //public virtual void Delete(TEntity entityToDelete)
        //{
        //    if (context.Entry(entityToDelete).State == EntityState.Detached)
        //    {
        //        dbSet.Attach(entityToDelete);
        //    }
        //    dbSet.Remove(entityToDelete);
        //}

        //public virtual void Update(TEntity entityToUpdate)
        //{
        //    dbSet.Attach(entityToUpdate);
        //    context.Entry(entityToUpdate).State = EntityState.Modified;
        //}
    }
}
