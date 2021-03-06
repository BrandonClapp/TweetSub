﻿using System;
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
        public IEnumerable<string> Users { get; set; }
        public IEnumerable<string> Topics { get; set; }
        public IEnumerable<string> Handlers { get; set; }
    }

}
