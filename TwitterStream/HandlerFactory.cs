using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using TwitterStream.Config;
using TwitterStream.Config.Objects;

namespace TwitterStream
{

    public static class HandlerFactory
    {
        // Local class to make instantiation easier.
        private class HandlerDetails
        {
            public HandlerDetails(string name, Type type, dynamic data)
            {
                Name = name;
                Type = type;
                Data = data;
            }

            public string Name { get; set; }
            public Type Type { get; set; }
            public dynamic Data { get; set; }
        }

        private static IDictionary<string, ITweetHandler> _loadedHandlers = new Dictionary<string, ITweetHandler>();

        public static void LoadRegistered()
        {
            _loadedHandlers.Clear();

            var settings = ConfigManager.LoadConfig<HandlerRegistry>("handlers");

            var handlerDetails = new List<HandlerDetails>();

            var assemblyTypes = Assembly.GetExecutingAssembly().GetTypes();
            foreach (var handler in settings.Handlers)
            {
                var name = handler.Name;
                Type correctType = null;
                try
                {
                    correctType = assemblyTypes.First(t => {
                        return t.Name == handler.Type &&
                            typeof(ITweetHandler).IsAssignableFrom(t) &&
                            typeof(ITweetHandler).Name != t.Name;
                    });
                }
                catch (InvalidOperationException ex)
                {
                    throw new InvalidOperationException($"Could not find tweet handler type for '{handler.Type}'", ex);
                }

                var data = handler.Data;

                HandlerDetails details = new HandlerDetails(name, correctType, data);
                handlerDetails.Add(details);
            }

            foreach (var details in handlerDetails)
            {
                ITweetHandler createdHandler = Create(details.Type, details.Data);
                _loadedHandlers.Add(details.Name, createdHandler);
            }
        }

        public static IDictionary<string, ITweetHandler> GetAllLoaded()
        {
            return _loadedHandlers;
        }

        public static bool TryGetHandler(string handlerName, out ITweetHandler handler)
        {
            if (_loadedHandlers == null)
            {
                throw new NullReferenceException("Handlers must be loaded before attempting to access them.");
            }

            _loadedHandlers.TryGetValue(handlerName, out handler);

            return handler != null;
        }

        private static ITweetHandler Create(Type handlerType, dynamic data)
        {
            var instance = (ITweetHandler)Activator.CreateInstance(handlerType);
            instance.Init(data);
            return instance;
        }
    }
}
