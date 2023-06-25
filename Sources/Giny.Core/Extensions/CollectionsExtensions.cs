
using Giny.Core.Time;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.Core.Extensions
{
    public static class CollectionsExtensions
    {
        public static TSource ArgBy<TSource, TKey>(
        this IEnumerable<TSource> source,
        Func<TSource, TKey> keySelector,
        Func<(TKey Current, TKey Previous), bool> predicate)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (keySelector == null) throw new ArgumentNullException(nameof(keySelector));
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));

            var value = default(TSource);
            var key = default(TKey);

            if (value == null)
            {
                foreach (var other in source)
                {
                    if (other == null) continue;
                    var otherKey = keySelector(other);
                    if (otherKey == null) continue;
                    if (value == null || predicate((otherKey, key)))
                    {
                        value = other;
                        key = otherKey;
                    }

                }
                return value;
            }
            else
            {
                bool hasValue = false;
                foreach (var other in source)
                {
                    var otherKey = keySelector(other);
                    if (otherKey == null) continue;

                    if (hasValue)
                    {
                        if (predicate((otherKey, key)))
                        {
                            value = other;
                            key = otherKey;
                        }
                    }
                    else
                    {
                        value = other;
                        key = otherKey;
                        hasValue = true;
                    }
                }
                if (hasValue) return value;
                throw new InvalidOperationException("Sequence contains no elements");
            }
        }
        public static TSource MinBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer = null)
        {
            if (comparer == null) comparer = Comparer<TKey>.Default;
            return source.ArgBy(keySelector, lag => comparer.Compare(lag.Current, lag.Previous) < 0);
        }

        public static TSource MaxBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer = null)
        {
            if (comparer == null) comparer = Comparer<TKey>.Default;
            return source.ArgBy(keySelector, lag => comparer.Compare(lag.Current, lag.Previous) > 0);
        }
        public static T2 TryGetValue<T1, T2>(this IDictionary<T1, T2> dictionary, T1 key) where T2 : class
        {
            T2 result;

            if (!dictionary.TryGetValue(key, out result))
            {
                return null;
            }

            return result;
        }
        public static bool TryRemove<T1, T2>(this ConcurrentDictionary<T1, T2> dictionnary, T1 key)
        {
            T2 result;
            return dictionnary.TryRemove(key, out result);
        }
      
        public static Color[] RandomColors(int count)
        {
            AsyncRandom rd = new AsyncRandom();

            Color[] colors = new Color[count];

            for (int i = 0; i < count; i++)
            {
                colors[i] = Color.FromArgb((byte)rd.Next(256), (byte)rd.Next(256), (byte)rd.Next(256));
            }
            return colors;
        }
        public static void Swap<T>(this IList<T> list, int indexA, int indexB)
        {
            T tmp = list[indexA];
            list[indexA] = list[indexB];
            list[indexB] = tmp;
        }
        public static T[] ParseCollection<T>(this string str, Func<string, T> converter)
        {
            T[] result;
            if (string.IsNullOrEmpty(str))
            {
                result = new T[0];
            }
            else
            {
                int num = 0;
                int num2 = str.IndexOf(',', 0);
                if (num2 == -1)
                {
                    result = new T[]
                    {
                        converter(str)
                    };
                }
                else
                {
                    T[] array = new T[str.CountOccurences(',', num, str.Length - num) + 1];
                    int num3 = 0;
                    while (num2 != -1)
                    {
                        array[num3] = converter(str.Substring(num, num2 - num));
                        num = num2 + 1;
                        num2 = str.IndexOf(',', num);
                        num3++;
                    }
                    array[num3] = converter(str.Substring(num, str.Length - num));
                    result = array;
                }
            }
            return result;
        }
        public static T Random<T>(this T[] array)
        {
            if (array.Length > 0)
            {
                return array[new AsyncRandom().Next(0, array.Length)];
            }
            else
                return default(T);
        }
        public static T Random<T>(this IEnumerable<T> collection)
        {
            int count = collection.Count();

            if (count > 0)
            {
                return collection.ElementAt(new AsyncRandom().Next(0, count));
            }
            else
                return default(T);
        }
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> enumerable)
        {
            var rand = new Random();

            T[] elements = enumerable.ToArray();
            // Note i > 0 to avoid final pointless iteration
            for (int i = elements.Length - 1; i > 0; i--)
            {
                // Swap element "i" with a random earlier element it (or itself)
                int swapIndex = rand.Next(i + 1);
                T tmp = elements[i];
                elements[i] = elements[swapIndex];
                elements[swapIndex] = tmp;
            }
            // Lazily yield (avoiding aliasing issues etc)
            foreach (T element in elements)
            {
                yield return element;
            }
        }
        public static IEnumerable<T> ShuffleWithProbabilities<T>(this IEnumerable<T> enumerable, IEnumerable<int> probabilities)
        {
            var rand = new Random();
            var elements = enumerable.ToList();
            var result = new T[elements.Count];
            var indices = probabilities.ToList();

            if (elements.Count != indices.Count)
                throw new Exception("Probabilities must have the same length that the enumerable");

            int sum = indices.Sum();

            if (sum == 0)
                return Shuffle(elements);

            for (int i = 0; i < result.Length; i++)
            {
                int randInt = rand.Next(sum + 1);
                int currentSum = 0;
                for (int j = 0; j < indices.Count; j++)
                {
                    currentSum += indices[j];

                    if (currentSum >= randInt)
                    {
                        result[i] = elements[j];

                        sum -= indices[j];
                        indices.RemoveAt(j);
                        elements.RemoveAt(j);
                        break;
                    }
                }
            }

            return result;
        }
    }
}
