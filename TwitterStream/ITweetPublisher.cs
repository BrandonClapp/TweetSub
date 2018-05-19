using System;
using System.Collections.Generic;
using System.Text;
using TwitterStream.Config.Objects;

namespace TwitterStream
{
    public interface ITweetPublisher
    {
        void Init(dynamic data);
        void Publish(Tweet tweet);
    }
}
