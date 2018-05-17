using System;
using System.Collections.Generic;
using System.Text;

namespace TwitterStream.Publishers
{
    public class RedConsolePublisher : ITweetPublisher
    {
        public void Publish(Tweet tweet)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(tweet.Message);
        }
    }
}
