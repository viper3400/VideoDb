using System;
using System.Collections.Generic;
using System.Text;

namespace Jaxx.VideoDbNetStandard
{
    public class EnhancedVideoDbOptions : IEnhancedVideoDbOptions
    {
        #region Private Fields

        private int _deltedOwenerId = -1;
        private bool _filterDeletedRecords = true;
        private bool _isDeletedOwnerVirtual = true;

        #endregion Private Fields

        #region Public Properties

        public int DeletedOwnerId
        {
            get
            {
                if (_deltedOwenerId == -1)
                { throw new NullReferenceException("_deletedOwnerId was not set"); }
                return _deltedOwenerId;
            }
            set => _deltedOwenerId = value;
        }

        public bool FilterDeletedRecords
        {
            get { return _filterDeletedRecords; }
            set => _filterDeletedRecords = value;
        }

        public bool IsDeletedOwnerVirtual
        {
            get
            {
                return _isDeletedOwnerVirtual;
            }
            set => _isDeletedOwnerVirtual = value;
        }

        #endregion Public Properties
    }
}
