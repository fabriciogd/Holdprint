using Holdprint.Core.Base;
using System.Collections.Generic;
using System.Linq;

namespace Holdprint.Core.Contract
{
    public interface IRepositoryQuery<TEntity, TDTO> 
        where TEntity : Poco
        where TDTO: Dto
    {
        IEnumerable<TDTO> GetAll(string sort = null, string order = "ASC");

        TDTO Get(int id);
    }
}
