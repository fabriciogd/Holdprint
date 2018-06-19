using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Holdprint.EF.Base;
using Holdprint.EF.Contract;
using Microsoft.EntityFrameworkCore;

namespace Holdprint.EF.Implementation
{
    public class DatabaseContext : ContextBase, IDatabaseContext, IDisposable
    {
        public DatabaseContext(DbContextOptions options)
            : base(options) { }

        public override async Task<int> SaveChangesAsync(CancellationToken ct = default(CancellationToken))
        {
            try
            {
                return await base.SaveChangesAsync(ct);
            }
            catch (DbUpdateException)
            {
                throw;
            }
        }

        public IQueryable<TEntity> AsNoTracking<TEntity>() where TEntity : class
        {
            return this.Set<TEntity>().AsNoTracking();
        }

        public override void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
