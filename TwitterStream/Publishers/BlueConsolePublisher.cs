using System;
using System.Collections.Generic;
using System.Text;

namespace TwitterStream.Publishers
{
    public class BlueConsolePublisher : ITweetPublisher
    {
        public void Init(dynamic config = null)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
        }

        public void Publish(Tweet tweet)
        {
            
            Console.WriteLine(tweet.Message);
        }
    }
}
