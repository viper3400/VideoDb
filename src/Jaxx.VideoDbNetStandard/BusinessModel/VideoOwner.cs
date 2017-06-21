using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jaxx.VideoDbNetStandard.BusinessModel
{
    public class VideoOwner : DatabaseModel.videodb_users
    {
        public List<VideoData> OwnedVideos { get; set; }
    }
}
