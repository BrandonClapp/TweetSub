using System;
using TwitterStream.Config.Objects;

namespace TwitterStream.Publishers
{
    public class RedConsolePublisher : ITweetPublisher
    {
        public void Init(PublisherConfig config)
        {
            
        }

        public void Publish(Tweet tweet)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(tweet.Message);
        }
    }
}
