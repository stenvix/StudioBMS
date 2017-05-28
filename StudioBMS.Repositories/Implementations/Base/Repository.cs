using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StudioBMS.Business.Interfaces.Repositories.Base;
using StudioBMS.Core.Entities.Interfaces;
using StudioBMS.Database.Context;

namespace StudioBMS.Repositories.Implementations.Base
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
    {
        public Repository(StudioContext context)
        {
            Context = context;
            Set = context.Set<TEntity>();
        }

        protected StudioContext Context { get; }
        protected DbSet<TEntity> Set { get; }


        public virtual Task<IQueryable<TEntity>> GetAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            return Task.Run(() => Set.AsQueryable(), cancellationToken);
        }

        public virtual Task<TEntity> GetAsync(Guid id, CancellationToken cancellationToken = new CancellationToken())
        {
            return Set.FindAsync(new object[] {id}, cancellationToken);
        }

        public virtual async Task<TEntity> CreateAsync(TEntity entity,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var entry = Context.Entry(entity);
            if (entry.State != EntityState.Detached)
            {
                entry.State = EntityState.Added;
                return entity;
            }
            return (await Set.AddAsync(entity, cancellationToken))?.Entity;
        }

        public virtual Task<TEntity> Update(TEntity entity, CancellationToken cancellationToken = default(CancellationToken))
        {
            return Task.Run(() =>
            {
                var local = Set.Local.FirstOrDefault(i => i.Id == entity.Id);
                if (local != null)
                    Context.Entry(local).State = EntityState.Detached;

                var entry = Context.Entry(entity);
                if (cancellationToken.IsCancellationRequested)
                    return null;
                if (entry.State == EntityState.Detached)
                    Set.Attach(entity);
                entry.State = EntityState.Modified;
                return entity;
            }, cancellationToken);
        }

        public virtual async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default(CancellationToken))
        {
            var entity = await Set.FindAsync(new object[] {id}, cancellationToken);
            if (entity != null)
                await DeleteAsync(entity, cancellationToken);
        }

        public virtual Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default(CancellationToken))
        {
            return Task.Run(() =>
            {
                if (entity == null) return;

                var local = Set.Local.FirstOrDefault(i => i.Id == entity.Id);
                if (local != null)
                    Context.Entry(local).State = EntityState.Detached;
                if (cancellationToken.IsCancellationRequested)
                    return;
                var entry = Context.Entry(entity);
                if (entry.State != EntityState.Deleted)
                    Set.Remove(entity);
            }, cancellationToken);
        }
    }
}