namespace ETLPipelineTool.Shared.Helpers
{
  public static class SortingHelper
  {
    public static IQueryable<TEntity> ApplySorting<TEntity, TQuery>(
      this IQueryable<TEntity> query,
      TQuery request,
      Func<TQuery, IEnumerable<SortField>> getSorts
    )
      where TQuery : ISortableQuery
    {
      var sorts = getSorts(request)?.ToList();
      if (sorts == null || sorts.Count == 0)
      {
        return query;
      }

      bool first = true;

      foreach (var sort in sorts)
      {
        query = query.OrderByDynamic(sort.Field, sort.Mode == SortMode.Desc, first);
        first = false;
      }

      return query;
    }

    public static IQueryable<T> OrderByDynamic<T>(
      this IQueryable<T> source,
      string property,
      bool descending,
      bool first
    )
    {
      if (string.IsNullOrWhiteSpace(property))
      {
        throw new ArgumentException("Property name cannot be null or empty", nameof(property));
      }

      var parameter = Expression.Parameter(typeof(T), "x");
      var member = Expression.PropertyOrField(parameter, property);
      var lambda = Expression.Lambda(member, parameter);

      string methodName = (first, descending) switch
      {
        (true, true) => nameof(Queryable.OrderByDescending),
        (true, false) => nameof(Queryable.OrderBy),
        (false, true) => nameof(Queryable.ThenByDescending),
        (false, false) => nameof(Queryable.ThenBy),
      };

      var method = typeof(Queryable)
        .GetMethods()
        .First(m => m.Name == methodName && m.GetParameters().Length == 2)
        .MakeGenericMethod(typeof(T), member.Type);

      return (IQueryable<T>)method.Invoke(null, [source, lambda])!;
    }
  }
}
