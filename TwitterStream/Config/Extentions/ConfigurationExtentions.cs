using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TwitterStream.Config.Objects;

namespace TwitterStream.Config.Extentions
{
    public static class ConfigurationExtentions
    {
        public static T AssertAllConfigured<T>(this IConfigurable configurable) where T : IConfigurable
        {
            foreach (var prop in typeof(T).GetProperties())
            {
                // todo: if iterable, do deep check.
                if (prop.GetType() == typeof(string))
                {
                    var value = prop.GetValue(configurable) as string;
                    if (string.IsNullOrWhiteSpace(value))
                        throw new ArgumentException("Incomplete configuration.", prop.Name);
                }
                
            }

            return (T)configurable;
        }

        public static string ToHypenCase(this string source)
        {
            var chars = source.ToList();
            for (int i = 0; i < chars.Count - 1; i++)
            {
                if (!char.IsWhiteSpace(chars[i]) && char.IsUpper(chars[i + 1]))
                {
                    chars[i + 1] = char.ToLower(chars[i + 1]);
                    chars.Insert(i + 1, '-');
                }
            }

            return new string(chars.ToArray());
        }
    }
}
