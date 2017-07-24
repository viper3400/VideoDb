using System;
using System.Collections.Generic;
using System.Text;

namespace Jaxx.VideoDbNetStandard.BusinessModel
{
    public class VideoDbUserSeenViewModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string ViewgroupName { get; set; }
        public DateTime ViewDate { get; set; }        
        public string MovieName { get; set; }
        public int MovieId { get; set; }
        public int ViewCount { get; set; }
    }
}
