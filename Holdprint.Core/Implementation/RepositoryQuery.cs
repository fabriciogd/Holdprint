using AutoMapper;
using Holdprint.Commom;
using Holdprint.Core.Base;
using Holdprint.Core.Contract;
using Holdprint.EF.Contract;
using System.Collections.Generic;
using System.Linq;
using Holdprint.Commom.Extensions;

namespace Holdprint.Core.Implementation
{
    public class RepositoryQuery<TEntity, TDTO> : IRepositoryQuery<TEntity, TDTO> 
        where TEntity: Poco
        where TDTO : Dto
    {
        private readonly IDatabaseContext _context;

        public RepositoryQuery(IDatabaseContext context)
        {
            _context = context;
        }

        public virtual IQueryable<TEntity> Queryable
        {
            get
            {
                return this._context.AsNoTracking<TEntity>();
            }
        }

        public TDTO Get(int id)
        {
            var row = this.Queryable.FirstOrDefault(a => a.Id == id);

            return Mapper.Map<TDTO>(row);
        }

        public IEnumerable<TDTO> GetAll(string sort = null, string order = "asc")
        {
            var query = this.Queryable;

            if(sort != null)
            {
                query = query.OrderBy(sort, order == "asc" ? SortDirection.Ascending : SortDirection.Descending);
            }

            var rows = query.ToList();

            return Mapper.Map<IList<TDTO>>(rows);
        }
    }
}
