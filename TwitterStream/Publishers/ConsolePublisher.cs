using System;
using System.Collections.Generic;
using System.Text;

namespace TwitterStream.Publishers
{
    public class ConsolePublisher : ITweetPublisher
    {
        public void Publish(Tweet tweet)
        {
            Console.WriteLine(tweet.Message);
        }
    }
}
