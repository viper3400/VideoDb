using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jaxx.VideoDbNetStandard.DatabaseModel
{
    public  partial class videodb_videodata
    {
        public virtual videodb_users VideoOwner { get; set; }
    }
}
