using System;
using System.Collections.Generic;
using System.Text;
using Tweetinvi;

namespace TwitterStream.Handlers
{
    public class FollowHandler : ITweetHandler
    {
        public void Init(dynamic data)
        {
            
        }

        public void Handle(Tweet tweet)
        {
            var author = User.GetUserFromScreenName(tweet.ScreenName);
            var authenticatedUser = User.GetAuthenticatedUser();

            var relationship = Friendship.GetRelationshipDetailsBetween(authenticatedUser.Id, author.Id);

            if (!relationship.Following)
            {
                authenticatedUser.FollowUser(author);
            }
        }

    }
}
