using System;
using System.Collections.Generic;
using System.Text;
using TwitterStream.Config.Objects;

namespace TwitterStream.Config
{
    public class TwitterSubscription : IConfigurable
    {
        public IEnumerable<Group> Groups { get; set; }
    }

    public class Group
    {
        public bool Enabled { get; set; }
        public string Identity { get; set; }
        public IEnumerable<string> Filters { get; set; }
        public IEnumerable<string> Publishers { get; set; }
    }

}
