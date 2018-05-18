using System;
using System.Collections.Generic;
using System.Text;

namespace TwitterStream
{
    public class Tweet
    {
        public string Message { get; set; }
        public bool IsRetweet { get; set; }
    }
}
