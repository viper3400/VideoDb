using System;
using System.Collections.Generic;
using System.Text;

namespace Jaxx.VideoDbNetStandard
{
    public interface IEnhancedVideoDbOptions
    {
        /// <summary>
        /// Gets or sets the owner id. If a record has this owner id it is considered as deleted.
        /// </summary>
        int DeletedOwnerId { get; set; }
        
        /// <summary>
        /// Gets or sets the idicator, if the owner of delted records is just a virtual owner or if
        /// the owner is existing at database.
        /// </summary>
        bool IsDeletedOwnerVirtual { get; set; }

        /// <summary>
        /// Gets or sets the indicator, if records which are marked as deleted should be filtered from
        /// search results by default.
        /// </summary>
        bool FilterDeletedRecords { get; set; }
    }
}
