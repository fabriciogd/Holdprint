using Holdprint.Core.Base;
using System.Threading;
using System.Threading.Tasks;

namespace Holdprint.Core.Contract
{
    public interface IService<TEntity, TDTO> 
        where TEntity: Poco
        where TDTO: Dto
    {
        Task<TDTO> Add(TDTO dto, CancellationToken ct = default(CancellationToken));

        Task<TDTO> Update(TDTO dto, CancellationToken ct = default(CancellationToken));

        Task Delete(int id, CancellationToken ct = default(CancellationToken));
    }
}
