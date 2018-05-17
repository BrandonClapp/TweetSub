using System;
using System.Collections.Generic;
using System.Text;

namespace TwitterStream
{
    public class Settings
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
