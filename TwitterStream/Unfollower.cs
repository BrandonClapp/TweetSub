using System;
using System.Collections.Generic;
using System.Text;
using Tweetinvi;

namespace TwitterStream
{
    public static class Unfollower
    {
        public static void Unfollow()
        {
            var authUser = User.GetAuthenticatedUser();
            var friends = authUser.GetFriends(1000);
            foreach (var friend in friends)
            {
                var relationnship = Friendship.GetRelationshipDetailsBetween(friend.UserIdentifier, authUser);
                if (!relationnship.Following)
                {
                    Console.WriteLine("Unfollowing " + friend.ScreenName);
                    authUser.UnFollowUser(friend);
                }
            }
        }
    }
}
