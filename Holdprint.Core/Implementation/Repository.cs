using Holdprint.Core.Base;
using Holdprint.Core.Contract;
using Holdprint.EF.Contract;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Holdprint.Core.Implementation
{
    public class Repository<TEntity> : IRepository<TEntity> 
        where TEntity: Poco
    {
        private readonly IDatabaseContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public Repository(IDatabaseContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public async Task<TEntity> Get(int id, CancellationToken ct = default(CancellationToken))
        {
            return await _dbSet.FindAsync(new object[] { id }, ct);
        }

        public async Task<TEntity> Add(TEntity entity, CancellationToken ct = default(CancellationToken))
        {
            await _dbSet.AddAsync(entity, ct);

            return entity;
        }

        public async Task<TEntity> Update(TEntity entity, CancellationToken ct = default(CancellationToken))
        {
            if (entity == null)
                return null;

            TEntity exist = await this.Get(entity.Id, ct);

            if (exist != null)
            {
                _context.Entry(exist).CurrentValues.SetValues(entity);
            }

            return exist;
        }

        public void Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
        }
    }
}
