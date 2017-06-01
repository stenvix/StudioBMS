using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using StudioBMS.Business.DTO.Models.Interfaces;

namespace StudioBMS.Business.Managers.Models.Base.Interfaces
{
    public interface IManager<TModel>: IDisposable
        where TModel : class, IModel

    {
        Task<IList<TModel>> GetAsync(CancellationToken cancellationToken = default(CancellationToken));
        Task<TModel> GetAsync(Guid id, CancellationToken cancellationToken = default(CancellationToken));
        Task<TModel> CreateAsync(TModel entity, CancellationToken cancellationToken = default(CancellationToken));
        Task<TModel> UpdateAsync(TModel entity, CancellationToken cancellationToken = default(CancellationToken));
        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default(CancellationToken));
        Task DeleteAsync(TModel entity, CancellationToken cancellationToken = default(CancellationToken));
    }
}