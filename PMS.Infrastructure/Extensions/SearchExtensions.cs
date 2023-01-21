using System.Linq;

namespace PMS.Infrastructure.Extensions
{
    public static class SearchExtensions
    {
        // var products = context.Products.Sort("asc","Name","Price").Search("searchTerm","Name","Description");
        public static IQueryable<TEntity> Search<TEntity>(this IQueryable<TEntity> query, string searchTerm, params string[] propertyNames)
        {
            var type = typeof(TEntity);
            var properties = type.GetProperties();
            var searchQuery = query;
            foreach (var propertyName in propertyNames)
            {
                //query = query.Where(e => e.GetType().GetProperty(propertyName).GetValue(e).ToString().Contains(searchTerm));
                System.Console.WriteLine(query.FirstOrDefault().GetType().GetProperty(propertyName).GetValue(query.FirstOrDefault()));
                searchQuery = searchQuery.AsEnumerable().Where(e => e.GetType().GetProperty(propertyName).GetValue(e).ToString().ToLower().Contains(searchTerm)).AsQueryable();

                /*foreach (var propertyName in propertyNames)
                {
                    *//*if(property.ToString() == propertyName)
                    {
                        if()
                    }*//*
                    query = query.Where(property.ToString() == propertyName)
                }
                var propertyValue = property.GetValue(query.FirstOrDefault());
                Console.WriteLine($"{property.Name}: {propertyValue}");*/
            }
            return searchQuery;
        }


    }

}
