using System;
using System.Collections.Generic;
using System.Text;

namespace TwitterStream.Publishers
{
    public class BlueConsolePublisher : ITweetPublisher
    {
        public void Publish(Tweet tweet)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(tweet.Message);
        }
    }
}
