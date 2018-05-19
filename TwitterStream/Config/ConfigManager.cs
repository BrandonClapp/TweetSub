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
        {
            if (file == null)
            {
                file = typeof(T).Name.ToHypenCase().ToLower();
            }

            T config = default(T);
            try
            {
                config = JsonConvert.DeserializeObject<T>(
                    File.Exists($"./Config/{file}.dev.config.json") ? File.ReadAllText($"./Config/{file}.dev.config.json")
                        : File.ReadAllText($"./Config/{file}.config.json")
                    );
            }
            catch (FileNotFoundException ex)
            {
                throw new FileNotFoundException($"Ensure that ./Config/{file}.dev.config.json or ./Config/{file}.config.json exist.", ex);
            }
            catch (JsonSerializationException ex)
            {
                throw new JsonSerializationException($"Could not serialize settings from '{file}' to type {typeof(T)}", ex);
            } 

            if (!allowEmptySettings)
            {
                config.AssertAllConfigured<T>();
            }

            return config;
        }
    }
}
