﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Jaxx.VideoDbNetStandard.DatabaseModel
{
   public partial class videodb_videogenre
    {
        public videodb_genres Genre { get; set; }
        public videodb_videodata Video { get; set; }
    }
}
