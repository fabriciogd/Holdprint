using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Holdprint.Core.Contract
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync(CancellationToken ct = default(CancellationToken));
    }
}
