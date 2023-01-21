using System.Linq;

namespace PMS.Infrastructure.Extensions
{
    public static class SortExtensions
    {
        // var products = context.Products.Sort("asc","Name","Price").Search("searchTerm","Name","Description");

        public static IQueryable<TEntity> Sort<TEntity>(this IQueryable<TEntity> query, string orderBy, params string[] propertyNames) where TEntity : class
        {
            if (propertyNames.Length == 0) return query;

            var sortedQuery = query;
            foreach (var propertyName in propertyNames)
            {
                if (string.IsNullOrEmpty(orderBy))
                    sortedQuery = sortedQuery.OrderBy(p => p.GetType().GetProperty(propertyName).GetValue(p, null));
                else
                    sortedQuery = orderBy switch
                    {
                        "asc" => sortedQuery.OrderBy(p => p.GetType().GetProperty(propertyName).GetValue(p, null)),
                        "desc" => sortedQuery.OrderByDescending(p => p.GetType().GetProperty(propertyName).GetValue(p, null)),
                        _ => sortedQuery.OrderBy(p => p.GetType().GetProperty(propertyName).GetValue(p, null))
                    };
            }
            return sortedQuery;
        }
    }
}
