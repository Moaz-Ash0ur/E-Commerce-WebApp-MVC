namespace ECommerceStore.BLL
{
    public static class Pagination
    {
        public static IQueryable<TSource> Paginate<TSource>(this IQueryable<TSource> source,
            int page = 1, int pageSize = 5)
        {
            if (source == null)
                return Enumerable.Empty<TSource>().AsQueryable();


            return source.Skip((page - 1) * pageSize).Take(pageSize);
        }

    }


}
