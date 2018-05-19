using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using TwitterStream.Config;
using TwitterStream.Config.Objects;

namespace TwitterStream
{

    internal class PublisherDetails
    {
        public PublisherDetails(string name, Type type, dynamic data)
        {
            Name = name;
            Type = type;
            Data = data;
        }

        public string Name { get; set; }
        public Type Type { get; set; }
        public dynamic Data { get; set; }
    }

    public static class PublisherFactory
    {

        private static IDictionary<string, ITweetPublisher> _loadedPublishers = new Dictionary<string, ITweetPublisher>();

        public static void LoadRegistered()
        {
            _loadedPublishers.Clear();

            var settings = ConfigManager.LoadConfig<PublisherRegistry>("publishers");
            var publishers = new List<PublisherConfig>();

            var publisherDetails = new List<PublisherDetails>();

            var assemblyTypes = Assembly.GetExecutingAssembly().GetTypes();
            foreach (var publisher in settings.Publishers)
            {
                var name = publisher.Name;
                var correctType = assemblyTypes.First(t => {
                    return t.Name == publisher.Type && 
                        typeof(ITweetPublisher).IsAssignableFrom(t) &&
                        typeof(ITweetPublisher).Name != t.Name;
                });

                var data = publisher.Data;

                PublisherDetails details = new PublisherDetails(name, correctType, data);
                publisherDetails.Add(details);
            }

            foreach (var details in publisherDetails)
            {
                ITweetPublisher createdPub = Create(details.Type, details.Data);
                _loadedPublishers.Add(details.Name, createdPub);
            }
        }

        public static IDictionary<string, ITweetPublisher> GetAllLoaded()
        {
            return _loadedPublishers;
        }

        public static bool TryGetPublisher(string publisherName, out ITweetPublisher pub)
        {
            if (_loadedPublishers == null)
            {
                throw new NullReferenceException("Publishers must be loaded before attempting to access them.");
            }

            _loadedPublishers.TryGetValue(publisherName, out pub);

            return pub != null;
        }

        private static ITweetPublisher Create(Type publisherType, dynamic data)
        {
            var instance = (ITweetPublisher)Activator.CreateInstance(publisherType);
            instance.Init(data);
            return instance;
        }
    }
}
