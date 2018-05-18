using Discord.Webhook;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TwitterStream.Config.Objects;

namespace TwitterStream.Publishers
{
    public class DiscordPublisher : ITweetPublisher
    {
        private DiscordWebhookClient _client;

        public void Init(PublisherConfig config)
        {
            if (config == null || config.Data == null)
            {
                throw new ArgumentNullException("Configuration data is required for the Discord publisher.");
            }

            if (config.Data.channelId == null || config.Data.token == null)
            {
                throw new ArgumentNullException("Discord publisher requires channelId and token to be configured.");
            }

            _client = new DiscordWebhookClient((ulong)config.Data.channelId, (string)config.Data.token);
        }

        public void Publish(Tweet tweet)
        {
            _client.SendMessageAsync(tweet.Message).Wait();
        }
    }
}
