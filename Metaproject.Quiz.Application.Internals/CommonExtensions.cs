using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Metaproject.Quiz.Application.Internals
{
    public static class CommonExtensions
    {
        public static bool IsTrue(this bool? item)
        {
            return item.HasValue && item.Value;
        }

        public static List<T> AsList<T>(this T item)
        {
            return new List<T> { item };
        }

        public static T[] AsArray<T>(this T item)
        {
            var array = new T[1];
            array[0] = item;
            return array;
        }

        public static bool IsNullOrEmpty(this string item)
        {
            return string.IsNullOrEmpty(item);
        }

        public static bool IsNotNullNorEmpty(this string item)
        {
            return !IsNullOrEmpty(item);
        }


        public static bool IsCollectionNullOrEmpty<T>(this IList<T> collection)
        {
            return collection == null || collection.Count == 0;
        }

        public static bool IsCollectionNotNullAndHasAnyItem<T>(this IList<T> collection)
        {
            return !IsCollectionNullOrEmpty<T>(collection);
        }
        
        public static string ToJson(this object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        public static string ConcatenateStrings(this IEnumerable<string> input)
        {
            return string.Join("", input);
        }

        public static bool IsEven(this int value)
        {
            return value % 2 == 0;
        }

        public static bool IsOdd(this int value)
        {
            return !IsEven(value);
        }

        
        public static IList<T> Shuffle<T>(this IList<T> input)
        {
            var list = input.ToList();

            var provider = new RNGCryptoServiceProvider();
            int n = list.Count;
            while (n > 1)
            {
                byte[] box = new byte[1];
                do provider.GetBytes(box);
                while (!(box[0] < n * (Byte.MaxValue / n)));
                int k = (box[0] % n);
                n--;
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }

            return list;
        }



}
}
