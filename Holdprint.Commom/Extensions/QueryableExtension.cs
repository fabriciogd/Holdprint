using System;
using System.Linq;
using System.Linq.Expressions;

namespace Holdprint.Commom.Extensions
{
    public static class QueryableExtension
    {
        public static IQueryable<T> OrderBy<T>(this IQueryable<T> query, string attribute, SortDirection direction)
        {
            return ApplyOrdering(query, attribute, direction, "OrderBy");
        }

        private static IQueryable<T> ApplyOrdering<T>(IQueryable<T> query, string attribute, SortDirection direction, string orderMethodName)
        {
            try
            {
                if (direction == SortDirection.Descending)
                    orderMethodName += "Descending";

                Type t = typeof(T);

                var param = Expression.Parameter(t);
                var property = t.GetProperties().FirstOrDefault(a => a.Name.Equals(attribute, StringComparison.OrdinalIgnoreCase));

                return query.Provider.CreateQuery<T>(
                    Expression.Call(
                        typeof(Queryable),
                        orderMethodName,
                        new Type[] { t, property.PropertyType },
                        query.Expression,
                        Expression.Quote(
                            Expression.Lambda(
                                Expression.Property(param, property),
                                param))
                    ));
            }
            catch (Exception)
            {
                return query;
            }
        }
    }
}
