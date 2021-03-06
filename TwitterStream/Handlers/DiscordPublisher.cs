﻿using Discord.Webhook;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TwitterStream.Config.Objects;

namespace TwitterStream.Handlers
{
    public class DiscordPublisher : ITweetHandler
    {
        private DiscordWebhookClient _client;

        public void Init(dynamic data)
        {
            if (data == null)
            {
                throw new ArgumentNullException("Configuration data is required for the Discord publisher.");
            }

            if (data.channelId == null || data.token == null)
            {
                throw new ArgumentNullException("Discord publisher requires channelId and token to be configured.");
            }

            _client = new DiscordWebhookClient((ulong)data.channelId, (string)data.token);
        }

        public void Handle(Tweet tweet)
        {
            try
            {
                var msg = $"{tweet.Url}";
                _client.SendMessageAsync(msg).Wait();
            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Could not post message to Discord. Continuing.");
            }
            
        }
    }
}
