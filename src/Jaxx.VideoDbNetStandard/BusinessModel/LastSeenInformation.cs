using System;
using System.Collections.Generic;
using System.Text;

namespace Jaxx.VideoDbNetStandard.BusinessModel
{
    public class LastSeenInformation
    {
        public DateTime LastSeenDate { get; set; }
        public int SeenCount { get; set; }
        public int DaysSinceLastView { get; set; }
        public string LastSeenSentence { get; set; }
    }
}
