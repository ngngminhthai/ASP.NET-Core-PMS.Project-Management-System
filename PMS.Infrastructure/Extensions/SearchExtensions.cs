using System.Linq;

namespace PMS.Infrastructure.Extensions
{
    public static class SearchExtensions
    {
        // var products = context.Products.Sort("asc","Name","Price").Search("searchTerm","Name","Description");
        public static IQueryable<TEntity> Search<TEntity>(this IQueryable<TEntity> query, string searchTerm, params string[] propertyNames) where TEntity : class
        {
            if (propertyNames.Length == 0 || string.IsNullOrEmpty(searchTerm)) return query;

            var lowerCaseSearchTerm = searchTerm.Trim().ToLower();
            var searchedQuery = query;
            foreach (var propertyName in propertyNames)
            {
                searchedQuery = searchedQuery.Where(p => p.GetType().GetProperty(propertyName).GetValue(p, null).ToString().ToLower().Contains(lowerCaseSearchTerm));
            }
            return searchedQuery;
        }
    }

}
