using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Holdprint.EF.Contract
{
    public interface IDatabaseContext
    {
        Task<int> SaveChangesAsync(CancellationToken ct = default(CancellationToken));

        IQueryable<TEntity> AsNoTracking<TEntity>() where TEntity : class;

        DbSet<TEntity> Set<TEntity>() where TEntity : class;

        EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
    }
}
