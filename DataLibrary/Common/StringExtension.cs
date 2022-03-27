using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;

namespace System
{
    public static class StringExtension
    {
        public static string GetString(this string input)
        {
            if (input != null)
            {
                return input;
            }
            else
            {
                return string.Empty;
            }
        }

        public static string ToStringDefault(this string input)
        {
            if (input != null)
            {
                if (input == string.Empty)
                {
                    return null;
                }
                else
                {
                    return input;
                }
            }
            else
            {
                return null;
            }
        }

        public static bool IsJson(this string input)
        {
            input = input.GetString().Trim();
            return input.StartsWith("{") && input.EndsWith("}")
                   || input.StartsWith("[") && input.EndsWith("]");
        }

        public static bool IsXml(this string input)
        {
            input = input.GetString().Trim();
            return input.StartsWith("<") && input.EndsWith(">");
        }

        public static bool IsHtml(this string input)
        {
            input = input.GetString().Trim();
            return input.StartsWith("<") && input.EndsWith(">") && input.Contains("html");
        }
    }

    public static class IEnumrableExtention
    {
        public static IEnumerable<T> DistinctBy<T, TKey>(this IEnumerable<T> items, Func<T, TKey> property)
        {
            return items.GroupBy(property).Select(x => x.First());
        }

        public static IEnumerable<TResult> FullOuterJoin<T1, T2, TResult>(this IEnumerable<T1> one, IEnumerable<T2> two, Func<T1, T2, bool> match, Func<T1, T2, TResult> result)
        {
            var left = from a in one
                       from b in two.Where((b) => match(a, b)).DefaultIfEmpty()
                       select new Tuple<T1, T2>(a, b);

            var right = from b in two
                        from a in one.Where((a) => match(a, b)).DefaultIfEmpty()
                        select new Tuple<T1, T2>(a, b);

            return left.Concat(right).Distinct().Select(x => result(x.Item1, x.Item2));
        }
    }

    public static class DirectoryExtension
    {
        public static System.IO.FileInfo[] GetFilesIncludeFilter(this System.IO.DirectoryInfo dInfo, string filters)
        {
            return filters.Split(';').SelectMany(filter => dInfo.GetFiles(filter)).ToArray();
        }

        public static System.IO.FileInfo[] GetFilesExcludeFilter(this System.IO.DirectoryInfo dInfo, string filters)
        {
            string[] extensions = filters.Replace("*", "").Split(';');
            System.IO.FileInfo[] files = dInfo.GetFiles();
            System.IO.FileInfo[] result = (from f in files
                                           where extensions.Contains(f.Extension) == false
                                           select f).ToArray();
            return result;
        }
    }
}
