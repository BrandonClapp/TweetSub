using System;
using System.Collections.Generic;
using System.Text;

namespace TwitterStream.Config.Objects
{
    public class PublisherRegistry : IConfigurable
    {
        public IEnumerable<PublisherConfig> Publishers { get; set; }
    }

    public class PublisherConfig
    {
        public string Name { get; set; }
        public dynamic Data { get; set; }
    }
}
