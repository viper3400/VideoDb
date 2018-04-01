using System;
using System.Collections.Generic;
using System.Text;

namespace Jaxx.VideoDbNetStandard.DatabaseModel
{
    public partial class videodb_genres
    {
        public virtual IEnumerable<videodb_videogenre> VideosForGenre { get; set; }
    }
}
