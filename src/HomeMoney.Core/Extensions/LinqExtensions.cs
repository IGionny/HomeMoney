using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace HomeMoney.Core.Extensions
{
  public static class LinqExtensions
  {
    public static IOrderedQueryable<TSource> OrderBy<TSource>(this IQueryable<TSource> source, string field,
      bool ascending = true)
    {
      if (string.IsNullOrWhiteSpace(field))
      {
        return source.OrderBy(x => 0);
      }

      if (field.Contains(" "))
      {
        var chunks = field.Split(' ');
        if (chunks.Length == 2)
        {
          field = chunks[0];
          ascending = string.Equals(chunks[1], "asc", StringComparison.InvariantCultureIgnoreCase);
        }
      }

      var lambda = source.GetLambda(field);
      if (!ascending)
      {
        return source.OrderByDescending(lambda);
      }

      return source.OrderBy(lambda);
    }

    public static Expression<Func<TSource, string>> GetLambda<TSource>(this IQueryable<TSource> source, string field)
    {
      var parameter = Expression.Parameter(typeof(TSource), "r");
      var expression = Expression.Property(parameter, field);
      return Expression.Lambda<Func<TSource, string>>(expression, parameter);
    }

    public static IOrderedQueryable<TSource> ThenBy<TSource>(this IOrderedQueryable<TSource> source, string field,
      bool ascending = true)
    {
      if (string.IsNullOrWhiteSpace(field)) return source;

      if (field.Contains(" "))
      {
        var chunks = field.Split(' ');
        if (chunks.Length == 2)
        {
          field = chunks[0];
          ascending = string.Equals(chunks[1], "asc", StringComparison.InvariantCultureIgnoreCase);
        }
      }

      var lambda = source.GetLambda(field);
      if (!ascending)
      {
        return source.ThenByDescending(lambda);
      }

      return source.ThenBy(lambda);
    }

    public static IEnumerable<T> OrderBy<T>(this IEnumerable<T> list, string field, bool ascending = true)
    {
      if (string.IsNullOrWhiteSpace(field))
      {
        return list;
      }

      field = field.Trim();
      if (field.Contains(" "))
      {
        var chunks = field.Split(' ').Where(x => !string.IsNullOrWhiteSpace(x)).ToList();
        if (chunks.Count() == 2)
        {
          field = chunks[0];
          ascending = string.Equals(chunks[1], "asc", StringComparison.InvariantCultureIgnoreCase);
        }
      }

      var prop = GetPropertyByName(typeof(T), field);

      if (prop == null)
      {
        throw new Exception("No property '" + field + "' in object of type " + typeof(T).Name + "'");
      }

      if (ascending)
        return list.OrderBy(x => prop.GetValue(x, null));

      else
        return list.OrderByDescending(x => prop.GetValue(x, null));
    }

    public static PropertyInfo GetPropertyByName(Type t, string field)
    {
      var props = t.GetProperties();
      var result = props.SingleOrDefault(x =>
        string.Equals(x.Name, field, StringComparison.InvariantCultureIgnoreCase));
      return result;
    }
  }
}