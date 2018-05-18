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
using TwitterStream.Config;
using TwitterStream.Config.Extentions;
using TwitterStream.Config.Objects;
using TwitterStream.Publishers;

namespace TwitterStream
{
    class Program
    {
        static void Main(string[] args)
        {
            var credentials = LoadConfig<TwitterCredentials>("twitter");

            Auth.SetUserCredentials(credentials.ConsumerKey, credentials.ConsumerSecret, credentials.UserAccessToken, credentials.UserAccessSecret);

            var user = User.GetAuthenticatedUser();         // user information
            var userSettings = user.GetAccountSettings();   // user settings information

            var settings = LoadConfig<Settings>("settings");

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
                        var pub = PublisherFactory.Create(publisher);

                        stream.MatchingTweetReceived += (sender, argx) =>
                        {
                            pub.Publish(
                                new Tweet() { Message = argx.Tweet.ToString() }
                            );
                        };
                    }

                    await stream.StartStreamMatchingAnyConditionAsync();
                });

                groupTasks.Add(groupTask);
            }

            Task.WaitAll(groupTasks.ToArray());
        }

        // Load a config file from the Config directory and deserialize it to a specified type (T).
        // Loading config will prefer a ".dev.config.json" file, otherwise will use the ".config.json" version.
        static T LoadConfig<T>(string file, bool allowEmptySettings = false) where T : IConfigurable
        {
            var config = JsonConvert.DeserializeObject<T>(
                File.Exists($"./Config/{file}.dev.config.json") ? File.ReadAllText($"./Config/{file}.dev.config.json")
                    : File.ReadAllText($"./Config/{file}.config.json")
                );

            return allowEmptySettings ? config : config.AssertAllConfigured<T>();
        }
    }
}
