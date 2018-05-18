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
            
        }

        public void Publish(Tweet tweet)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(tweet.Message);
        }
    }
}
