using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StationeersLogicTool
{
    public static class IEnumerableExtension
    {
        public static IEnumerable<TResult> Where<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, int, bool> predicate) where TResult : TSource
        {
            return source.Where((o, i) => o is TResult && predicate((TResult)o, i)).Cast<TResult>();
        }
        public static IEnumerable<TResult> Where<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, bool> predicate) where TResult : TSource
        {
            return source.Where(o => o is TResult && predicate((TResult)o)).Cast<TResult>();
        }
        public static IEnumerable<TResult> Where<TSource, TResult>(this IEnumerable<TSource> source) where TResult : TSource
        {
            return source.Where(o => o is TResult).Cast<TResult>();
        }
    }
}
