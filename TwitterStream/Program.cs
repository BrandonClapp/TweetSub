using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tweetinvi;
using Tweetinvi.Models;
using TwitterStream.Config;

namespace TwitterStream
{
    class Program
    {
        static void Main(string[] args)
        {
            var credentials = ConfigManager.LoadConfig<TwitterStream.Config.TwitterCredentials>();

            Auth.SetUserCredentials(credentials.ConsumerKey, credentials.ConsumerSecret, credentials.UserAccessToken, credentials.UserAccessSecret);

            if (args.ToList().Any(arg => arg.ToLower() == "-uf" || arg.ToLower() == "-unfollow"))
            {
                Unfollower.Unfollow(); // Quick and dirty for now.
                return;
            }

            var subscription = ConfigManager.LoadConfig<TwitterSubscription>();

            HandlerFactory.LoadRegistered();

            var groupTasks = new List<Task>();
            foreach (var group in subscription.Groups)
            {
                if (!group.Enabled)
                    continue;

                var groupTask = Task.Run(async () =>
                {
                    var stream = Tweetinvi.Stream.CreateFilteredStream();
                    stream.AddTweetLanguageFilter(LanguageFilter.English);

                    // Subscribe to group topics.
                    foreach (var topic in group.Topics)
                    {
                        stream.AddTrack(topic);
                    }

                    // Subscribe to group users.
                    foreach (var userName in group.Users)
                    {
                        var user = User.GetUserFromScreenName(userName);
                        stream.AddFollow(user);
                    }

                    foreach (var handlerName in group.Handlers)
                    {
                        if (HandlerFactory.TryGetHandler(handlerName, out var handler))
                        {
                            stream.MatchingTweetReceived += (sender, argx) =>
                            {
                                var tweet = new Tweet()
                                {
                                    Message = argx.Tweet.ToString(),
                                    IsRetweet = argx.Tweet.IsRetweet || argx.Tweet.ToString().Contains("RT:"),
                                    ScreenName = argx.Tweet.CreatedBy.ScreenName,
                                    Url = argx.Tweet.Url,
                                    AuthorFollowers = argx.Tweet.CreatedBy.FollowersCount
                                };

                                TweetDispatcher.Dispatch(tweet, handler);
                            };
                        }   
                    }

                    await stream.StartStreamMatchingAnyConditionAsync();
                });

                groupTasks.Add(groupTask);
            }

            Task.WaitAll(groupTasks.ToArray()); // todo: add cancelation token.
        }
        
    }
}
