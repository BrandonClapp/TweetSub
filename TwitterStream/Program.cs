using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Tweetinvi;

namespace TwitterStream
{
    class Program
    {
        static void Main(string[] args)
        {
            var credentials = GetUserCredentials();

            Auth.SetUserCredentials(credentials.ConsumerKey, credentials.ConsumerSecret, credentials.UserAccessToken, credentials.UserAccessSecret);

            var user = User.GetAuthenticatedUser();         // user information
            var userSettings = user.GetAccountSettings();   // user settings information

            var filters = GetFilters();
            var stream = Tweetinvi.Stream.CreateFilteredStream();

            foreach (var filter in filters)
            {
                stream.AddTrack(filter);
            }

            stream.MatchingTweetReceived += (sender, tweetReceivedEvent) =>
            {
                // todo: add publishers to publish to console, discord, slack ...etc
                Console.WriteLine(tweetReceivedEvent.Tweet);
            };

            stream.StartStreamMatchingAnyCondition();
        }

        static Credentials GetUserCredentials()
        {
            var credentials = JsonConvert.DeserializeObject<Credentials>(
                File.Exists("./credentials.json") ? File.ReadAllText("./credentials.json")
                    : File.ReadAllText("./credentials.dev.json")
                );

            foreach (var prop in credentials.GetType().GetProperties())
            {
                var value = (string)prop.GetValue(credentials);
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Fill in all authentication values in credentials.json or credentials.dev.json");
            }

            return credentials;
        }

        static IEnumerable<string> GetFilters()
        {
            var filters = JsonConvert.DeserializeObject<IEnumerable<string>>(File.ReadAllText("./tweet-filters.json"));
            return filters;
        }
    }
}
