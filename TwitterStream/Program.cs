using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
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

            var settings = LoadSettings();
            var groupTasks = new List<Task>();
            foreach (var group in settings.Groups)
            {
                var groupTask = Task.Run(async () =>
                {
                    var stream = Tweetinvi.Stream.CreateFilteredStream();

                    foreach (var filter in group.Filters)
                    {
                        stream.AddTrack(filter);
                    }

                    stream.MatchingTweetReceived += (sender, tweetReceivedEvent) =>
                    {
                        // todo: publish to all group.Publishers
                        Console.WriteLine(tweetReceivedEvent.Tweet);
                    };

                    await stream.StartStreamMatchingAnyConditionAsync();
                });

                groupTasks.Add(groupTask);
            }

            Task.WaitAll(groupTasks.ToArray());
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

        static Settings LoadSettings()
        {
            var settings = JsonConvert.DeserializeObject<Settings>(File.ReadAllText("./settings.json"));
            return settings;
        }
    }
}
