using System;
using TwitterStream.Config.Objects;

namespace TwitterStream.Publishers
{
    public class RedConsolePublisher : ITweetPublisher
    {
        public void Init(PublisherConfig config)
        {
            Console.ForegroundColor = ConsoleColor.Red;
        }

        public void Publish(Tweet tweet)
        {
            Console.WriteLine(tweet.Message);
        }
    }
}
