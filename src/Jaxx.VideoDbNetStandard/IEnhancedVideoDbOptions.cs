using System;
using System.Collections.Generic;
using System.Text;

namespace Jaxx.VideoDbNetStandard
{
    public interface IEnhancedVideoDbOptions
    {
        int DeletedOwnerId { get; set; }
        bool IsDeletedOwnerVirtual { get; set; }
    }
}
