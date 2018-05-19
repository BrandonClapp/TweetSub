using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tweetinvi.Events;
using TwitterStream.Config;

namespace TwitterStream
{
    public static class TweetDispatcher
    {
        public static void Dispatch(Tweet tweet, ITweetHandler handler)
        {
            if (tweet.IsRetweet)
                return;

            var encodedWords = ConfigManager.LoadConfig<string>("word-filter");
            var decodedString = Encoding.UTF8.GetString(Convert.FromBase64String(encodedWords));

            // Bad words base 64 encoded for obvious reasons.
            var badWords = decodedString.ToLower().Split("\r\n");
            var tweetWords = tweet.Message.ToLower().Replace("#", "").Split(" ");

            var badTweetWords = tweetWords.Intersect(badWords);
            if (badTweetWords.Count() > 0)
            {
                Console.WriteLine("Filtering tweet: ", tweet);
                return;
            }

            handler.Handle(tweet);
        }
    }
}
