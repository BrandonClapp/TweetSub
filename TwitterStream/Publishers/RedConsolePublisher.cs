using System;
using System.Collections.Generic;
using System.Text;

namespace TwitterStream.Publishers
{
    public class RedConsolePublisher : ITweetPublisher
    {
        public void Init(dynamic config = null)
        {
            Console.ForegroundColor = ConsoleColor.Red;
        }

        public void Publish(Tweet tweet)
        {
            
            Console.WriteLine(tweet.Message);
        }
    }
}
