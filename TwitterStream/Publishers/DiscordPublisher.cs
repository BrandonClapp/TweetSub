using Discord.Webhook;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TwitterStream.Publishers
{
    public class DiscordPublisher : ITweetPublisher
    {
        private DiscordWebhookClient _client;

        public void Init(dynamic config = null)
        {
            if (config == null || config.token == null || config.channelId == null)
            {
                throw new ArgumentNullException("Discord publisher requires channel id and token to be configured.");
            }

            _client = new DiscordWebhookClient((ulong)config.channelId, (string)config.token);
        }

        public void Publish(Tweet tweet)
        {
            _client.SendMessageAsync(tweet.Message).Wait();
        }
    }
}
