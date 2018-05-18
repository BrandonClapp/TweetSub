using System;
using System.Collections.Generic;
using System.Text;
using TwitterStream.Config.Objects;

namespace TwitterStream.Publishers
{
    public class BlueConsolePublisher : ITweetPublisher
    {
        public void Init(PublisherConfig config)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
        }

        public void Publish(Tweet tweet)
        {
            Console.WriteLine(tweet.Message);
        }
    }
}
