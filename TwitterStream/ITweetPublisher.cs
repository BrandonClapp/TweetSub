using System;
using System.Collections.Generic;
using System.Text;
using TwitterStream.Config.Objects;

namespace TwitterStream
{
    public interface ITweetHandler
    {
        void Init(dynamic data);
        void Handle(Tweet tweet);
    }
}
