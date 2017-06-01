using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using StudioBMS.Business.DTO.Extensions;
using StudioBMS.Business.DTO.Models.Interfaces;
using StudioBMS.Business.Interfaces.Repositories.Base;
using StudioBMS.Business.Managers.Models.Base.Interfaces;
using StudioBMS.Business.Managers.Repositories.Interfaces;
using StudioBMS.Core.Entities.Interfaces;

namespace StudioBMS.Business.Managers.Models.Base.Impl
{
    public class CrudManager<TModel, TEntity>: IManager<TModel> where TModel: class, IModel where TEntity: class, IEntity
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IRepository<TEntity> _repository;

        public CrudManager(IUnitOfWork unitOfWork, IRepository<TEntity> repository)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
        }
        public virtual async Task<IList<TModel>> GetAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            return Mapper.Map<IEnumerable<TEntity>, IList<TModel>>(await _repository.GetAsync(cancellationToken));
        }

        public virtual async Task<TModel> GetAsync(Guid id, CancellationToken cancellationToken = new CancellationToken())
        {
            return (await _repository.GetAsync(id, cancellationToken)).To<TModel>();
        }

        public virtual async Task<TModel> CreateAsync(TModel entity, CancellationToken cancellationToken = new CancellationToken())
        {
            var result = await _repository.CreateAsync(entity.To<TEntity>(), cancellationToken);
            await _unitOfWork.SaveChanges();
            return result.To<TModel>();
        }

        public virtual async Task<TModel> UpdateAsync(TModel entity, CancellationToken cancellationToken = new CancellationToken())
        {
            var result = await _repository.Update(entity.To<TEntity>(), cancellationToken);
            await _unitOfWork.SaveChanges();
            return result.To<TModel>();
        }

        public virtual async Task DeleteAsync(Guid id, CancellationToken cancellationToken = new CancellationToken())
        {
            await _repository.DeleteAsync(id, cancellationToken);
            await _unitOfWork.SaveChanges();
        }

        public virtual async Task DeleteAsync(TModel entity, CancellationToken cancellationToken = new CancellationToken())
        {
            await _repository.DeleteAsync(entity.To<TEntity>(), cancellationToken);
            await _unitOfWork.SaveChanges();
        }

        public void Dispose()
        {
            _unitOfWork?.Dispose();
        }
    }
}
