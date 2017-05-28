using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using StudioBMS.Core.Entities.Interfaces;

namespace StudioBMS.Business.Interfaces.Repositories.Base
{
    public interface IRepository<TEntity> where TEntity : class, IEntity
    {
        Task<IQueryable<TEntity>> GetAsync(CancellationToken cancellationToken = default(CancellationToken));
        Task<TEntity> GetAsync(Guid id, CancellationToken cancellationToken = default(CancellationToken));
        Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken = default(CancellationToken));
        Task<TEntity> Update(TEntity entity, CancellationToken cancellationToken = default(CancellationToken));
        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default(CancellationToken));
        Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default(CancellationToken));
    }
}