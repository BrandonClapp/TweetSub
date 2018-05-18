using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TwitterStream.Config.Extentions;
using TwitterStream.Config.Objects;

namespace TwitterStream.Config
{
    public static class ConfigManager
    {
        public static T LoadConfig<T>(string file = null, bool allowEmptySettings = false) 
            where T : IConfigurable, new()
        {
            if (file == null)
            {
                file = typeof(T).Name.ToHypenCase().ToLower();
            }

            var config = JsonConvert.DeserializeObject<T>(
                File.Exists($"./Config/{file}.dev.config.json") ? File.ReadAllText($"./Config/{file}.dev.config.json")
                    : File.ReadAllText($"./Config/{file}.config.json")
                );

            if (!allowEmptySettings)
            {
                config.AssertAllConfigured<T>();
            }

            return config;
        }
    }
}
