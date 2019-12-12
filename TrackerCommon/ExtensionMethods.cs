using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Linq.Expressions;

namespace TrackerCommon
{
    public static partial class ExtensionMethods
    {
        public static string GetDescriptionFromEnumValue<T>(this T value) where T : Enum
        {
            return !(typeof(T)
                .GetField(value.ToString())
                .GetCustomAttributes(typeof(DescriptionAttribute), false)
                .SingleOrDefault() is DescriptionAttribute attr) ? value.ToString() : attr.Description;
        }

        public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, string order, bool descending)
        {
            //
            // this code from StackOverflow.com answer to this question: 
            //   https://stackoverflow.com/questions/7265186/how-do-i-specify-the-linq-orderby-argument-dynamically
            //

            string command = descending ? "OrderByDescending" : "OrderBy";
            var type = typeof(T);
            var property = type.GetProperty(order);
            var parameter = Expression.Parameter(type, "p");
            var propertyAccess = Expression.MakeMemberAccess(parameter, property);
            var orderByExpression = Expression.Lambda(propertyAccess, parameter);
            var resultExpression = Expression.Call(typeof(Queryable), command, new Type[] { type, property.PropertyType },
                source.Expression, Expression.Quote(orderByExpression));
            return source.Provider.CreateQuery<T>(resultExpression);
        }

        public static bool ArrayEquals<T>(this T[] array1, T[] array2)
        {
            if (ReferenceEquals(array1, array2))
            {
                return true;
            }
            if (array1 is null)
            {
                if (array2 is null)
                {
                    return true;
                }
                return false;
            }
            if (array2 is null)
            {
                return false;
            }
            if (array1.Length != array2.Length)
            {
                return false;
            }
            IEqualityComparer<T> comparer = EqualityComparer<T>.Default;
            for (int i = 0; i < array1.Length; i++)
            {
                if (!comparer.Equals(array1[i], array2[i]))
                {
                    return false;
                }
            }
            return true;
        }

        public static T[] ArrayCopy<T>(this T[] source)
        {
            T[] ret = new T[source.Length];
            for (int i = 0; i < source.Length; i++)
            {
                ret[i] = source[i];
            }
            return ret;
        }

        public static bool IsBetween(this DateTime date, DateTime start, DateTime end) => date >= start && date <= end;

        public static string Innermost(this Exception ex)
        {
            if (ex.InnerException is null)
            {
                return ex.Message;
            }
            return ex.InnerException.Innermost();
        }

        public static bool Contains(this string source, string pattern, StringComparison comp) => source.IndexOf(pattern, comp) >= 0;

        public static bool Matches(this string s1, string s2, StringComparison comp = StringComparison.OrdinalIgnoreCase) =>
            s1.Equals(s2, comp);

        public static List<string> ToList(this StringCollection coll)
        {
            List<string> ret = new List<string>(coll.Count);
            foreach (var c in coll)
            {
                ret.Add(c);
            }
            return ret;
        }

        public static void Resort<T, U>(this List<T> coll, Func<T, U> pred) where U : IComparable<U>
        {
            coll.Sort((x, y) => pred.Invoke(x).CompareTo(pred.Invoke(y)));
        }
    }
}
