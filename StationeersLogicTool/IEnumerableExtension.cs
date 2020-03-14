using StationeersLogicTool.GameTypes.LogicChips;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StationeersLogicTool
{
    public static class IEnumerableExtension
    {
        public static IEnumerable<TResult> Filter<TResult>(this IEnumerable<object> source)
        {
            return source.Where(o => o is TResult).Cast<TResult>();
        }

        public static TResult First<TResult>(this IEnumerable<Chip> source, Func<TResult, bool> predicate) where TResult : Chip
        {
            return source.First(o => o is TResult && predicate(o as TResult)) as TResult;
        }
        public static TResult First<TResult>(this IEnumerable<Chip> source) where TResult : Chip
        {
            return source.First(o => o is TResult) as TResult;
        }

        public static TResult Last<TResult>(this IEnumerable<Chip> source, Func<TResult, bool> predicate) where TResult : Chip
        {
            return source.Last(o => o is TResult && predicate(o as TResult)) as TResult;
        }
        public static TResult Last<TResult>(this IEnumerable<Chip> source) where TResult : Chip
        {
            return source.Last(o => o is TResult) as TResult;
        }

        public static IEnumerable<TResult> Where<TResult>(this IEnumerable<Chip> source, Func<TResult, int, bool> predicate) where TResult : Chip
        {
            return source.Where((o, i) => o is TResult && predicate(o as TResult, i)).Cast<TResult>();
        }
        public static IEnumerable<TResult> Where<TResult>(this IEnumerable<Chip> source, Func<TResult, bool> predicate) where TResult : Chip
        {
            return source.Where(o => o is TResult && predicate(o as TResult)).Cast<TResult>();
        }
        public static IEnumerable<TResult> Where<TResult>(this IEnumerable<Chip> source) where TResult : Chip
        {
            return source.Where(o => o is TResult).Cast<TResult>();
        }
    }
}
