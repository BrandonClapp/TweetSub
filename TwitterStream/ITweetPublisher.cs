using System;
using System.Collections.Generic;
using System.Text;

namespace TwitterStream
{
    public interface ITweetPublisher
    {
        void Init(dynamic config = null);
        void Publish(Tweet tweet);
    }
}
