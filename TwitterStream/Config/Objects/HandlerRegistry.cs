using System;
using System.Collections.Generic;
using System.Text;

namespace TwitterStream.Config.Objects
{
    public class HandlerRegistry : IConfigurable
    {
        public IEnumerable<HandlerConfig> Handlers { get; set; }
    }

    public class HandlerConfig
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public dynamic Data { get; set; }
    }
}
