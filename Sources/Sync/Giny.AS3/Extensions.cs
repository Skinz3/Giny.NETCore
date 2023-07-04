using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.AS3
{
    public static class Extensions
    {
        public static T ParseEnum<T>(this string value) where T : struct
        {
            T result = default(T);

            if (Enum.TryParse<T>(value, out result))
            {
                return result;
            }
            else
            {
                return default(T);
            }
        }
        public static string PrepareForExpression(this string value)
        {
            if (value.EndsWith(";"))
            {
                return value.Remove(value.Length - 1).Trim();
            }
            return value.Trim();
        }
        public static string RemoveChars(this string input, params char[] chars)
        {
            string result = input;
            foreach (var @char in chars)
            {
                result = result.Replace(@char.ToString(), string.Empty);
            }
            return result;
        }

    }
}
