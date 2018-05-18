using System;
using System.Collections.Generic;
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

                var value = (string)prop.GetValue(configurable);
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Incomplete configuration.", prop.Name);
            }

            return (T)configurable;
        }
    }
}
