using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Tweetinvi;
using Tweetinvi.Streaming;
using TwitterStream.Publishers;

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

                    foreach (var publisher in group.Publishers)
                    {
                        var pub = GetPublisher(publisher);

                        stream.MatchingTweetReceived += (sender, argx) =>
                        {
                            pub.Publish(
                                new Publishers.Tweet() { Message = argx.Tweet.ToString() }
                            );
                        };
                    }

                    await stream.StartStreamMatchingAnyConditionAsync();
                });

                groupTasks.Add(groupTask);
            }

            Task.WaitAll(groupTasks.ToArray());
        }

        static ITweetPublisher GetPublisher(string publisher)
        {
            var pubType = Assembly.GetExecutingAssembly()
                .GetExportedTypes()
                .First(t => t.FullName.EndsWith(publisher));

            var instance = (ITweetPublisher)Activator.CreateInstance(pubType);
            return instance;
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
