using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jaxx.VideoDbNetStandard.DatabaseModel
{
    public class homewebbridge_usermoviesettings
    {
        public int id { get; set; }
        public int vdb_movieid { get; set; }
        public string asp_username { get; set; }
        public int is_favorite { get; set; }
        public string watchagain { get; set; }
    }
}
