using System;
using System.Collections.Generic;
using System.Text;

namespace Jaxx.VideoDbNetStandard
{
    public interface IVideoDbOptions
    {
        TimeSpan CacheExpirationTime { get; set; }
    }
}
