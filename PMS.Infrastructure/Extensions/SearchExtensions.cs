using System.Linq;

namespace PMS.Infrastructure.Extensions
{
    public static class SearchExtensions
    {
        /// <summary>
        /// Search on IQueryable by search term, many search fields
        /// for eg .Search("searchTerm","Name","Description");
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="query"></param>
        /// <param name="searchTerm"></param>
        /// <param name="propertyNames"></param>
        /// <returns></returns>
        public static IQueryable<TEntity> Search<TEntity>(this IQueryable<TEntity> query, string searchTerm, params string[] propertyNames)
        {
            var type = typeof(TEntity);
            var properties = type.GetProperties();
            var searchQuery = query;
            foreach (var propertyName in propertyNames)
            {
                searchQuery = searchQuery.AsEnumerable().Where(e => e.GetType().GetProperty(propertyName).GetValue(e).ToString().ToLower().Contains(searchTerm)).AsQueryable();
            }
            return searchQuery;
        }


    }

}
