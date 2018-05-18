using System;
using System.Collections.Generic;
using System.Text;
using TwitterStream.Config.Objects;

namespace TwitterStream.Config
{
    public class Settings : IConfigurable
    {
        public IEnumerable<Group> Groups { get; set; }
        public IEnumerable<Publisher> Publishers { get; set; }
    }

    public class Group
    {
        public bool Enabled { get; set; }
        public string Identity { get; set; }
        public IEnumerable<string> Filters { get; set; }
        public IEnumerable<string> Publishers { get; set; }
    }

    public class Publisher
    {
        public string Name { get; set; }
        public bool Enabled { get; set; }
    }
}
