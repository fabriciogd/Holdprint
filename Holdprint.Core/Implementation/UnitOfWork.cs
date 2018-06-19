using Holdprint.Core.Contract;
using Holdprint.EF.Contract;
using System.Threading;
using System.Threading.Tasks;

namespace Holdprint.Core.Implementation
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDatabaseContext _context;

        public UnitOfWork(IDatabaseContext context)
        {
            _context = context;
        }

        public async Task<int> SaveChangesAsync(CancellationToken ct = default(CancellationToken))
        {
            return await _context.SaveChangesAsync();
        }
    }
}
