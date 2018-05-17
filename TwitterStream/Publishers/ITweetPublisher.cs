using System;
using System.Collections.Generic;
using System.Text;

namespace TwitterStream.Publishers
{
    public interface ITweetPublisher
    {
        void Publish(Tweet tweet);
    }
}
