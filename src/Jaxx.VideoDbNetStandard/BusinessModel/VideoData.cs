using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jaxx.VideoDbNetStandard.BusinessModel
{
    public class VideoData : DatabaseModel.videodb_videodata
    {
        public VideoOwner Owner { get; set; }
    }
}
