using System;
using System.Collections.Generic;
using System.Text;
using TwitterStream.Config.Objects;

namespace TwitterStream.Publishers
{
    public class ConsolePublisher : ITweetPublisher
    {
        private ConsoleColor Color = ConsoleColor.White;

        public void Init(dynamic data)
        {
            if (data == null)
                return;

            if (data.color == null)
                throw new ArgumentException("If specifying data for a ConsolePublisher, supported properties are: color");

            string color = data.color;
            Color = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), color);
        }

        public void Publish(Tweet tweet)
        {
            Console.ForegroundColor = Color;
            Console.WriteLine(tweet.Message);
        }
    }
}
