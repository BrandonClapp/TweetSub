using System;
using System.Collections.Generic;
using System.Text;
using TwitterStream.Config.Objects;

namespace TwitterStream
{
    public interface ITweetPublisher
    {
        void Init(PublisherConfig config);
        void Publish(Tweet tweet);
    }
}
