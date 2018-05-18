using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace TwitterStream
{
    public static class PublisherFactory
    {
        public static ITweetPublisher Create(string publisherName)
        {
            var pubType = Assembly.GetExecutingAssembly()
                .GetExportedTypes()
                .First(t => t.FullName.EndsWith(publisherName));

            var instance = (ITweetPublisher)Activator.CreateInstance(pubType);
            instance.Init();
            return instance;
        }
    }
}
