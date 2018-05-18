using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tweetinvi;
using TwitterStream.Config;

namespace TwitterStream
{
    class Program
    {
        static void Main(string[] args)
        {
            var credentials = ConfigManager.LoadConfig<TwitterCredentials>();

            Auth.SetUserCredentials(credentials.ConsumerKey, credentials.ConsumerSecret, credentials.UserAccessToken, credentials.UserAccessSecret);

            var user = User.GetAuthenticatedUser();         // user information
            var userSettings = user.GetAccountSettings();   // user settings information

            var subscription = ConfigManager.LoadConfig<TwitterSubscription>();

            var publishers = PublisherFactory.GetRegistered();

            var groupTasks = new List<Task>();
            foreach (var group in subscription.Groups)
            {
                var groupTask = Task.Run(async () =>
                {
                    var stream = Tweetinvi.Stream.CreateFilteredStream();

                    foreach (var filter in group.Filters)
                    {
                        stream.AddTrack(filter);
                    }

                    foreach (var publisherName in group.Publishers)
                    {
                        var pub = publishers[publisherName];

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
        
    }
}
