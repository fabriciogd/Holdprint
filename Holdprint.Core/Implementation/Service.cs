using AutoMapper;
using Holdprint.Core.Base;
using Holdprint.Core.Contract;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Holdprint.Core.Implementation
{
    public class Service<TEntity, TDTO> : IService<TEntity, TDTO>
        where TEntity : Poco
        where TDTO : Dto
    {
        private readonly IRepository<TEntity> _repository;
        private readonly IUnitOfWork _unitOfWork;

        public Service(IRepository<TEntity> repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<TDTO> Add(TDTO dto, CancellationToken ct = default(CancellationToken))
        {
            TEntity entity = Mapper.Map<TEntity>(dto);

            entity = await _repository.Add(entity, ct);

            await _unitOfWork.SaveChangesAsync(ct);

            dto = Mapper.Map<TEntity, TDTO>(entity, dto);

            return dto;
        }

        public async Task<TDTO> Update(TDTO dto, CancellationToken ct = default(CancellationToken))
        {
            TEntity entity = await _repository.Get(dto.Id, ct);

            entity = Mapper.Map<TDTO, TEntity>(dto, entity);

            entity = await _repository.Update(entity, ct);

            await _unitOfWork.SaveChangesAsync(ct);

            dto = Mapper.Map<TEntity, TDTO>(entity, dto);

            return dto;
        }

        public async Task Delete(int id, CancellationToken ct = default(CancellationToken))
        {
            TEntity entity = await _repository.Get(id, ct);

            _repository.Delete(entity);

            await _unitOfWork.SaveChangesAsync(ct);
        }
    }
}
