using System;
using System.Collections.Generic;
using System.Text;

namespace Jaxx.VideoDbNetStandard
{
    public class EnhancedVideoDbOptions : IEnhancedVideoDbOptions
    {
        private int _deltedOwenerId = -1;
        private bool _isDeletedOwnerVirtual = true;

        public int DeletedOwnerId
        {
            get
            {
                if (_deltedOwenerId == -1)
                { throw new NullReferenceException("_deletedOwnerId was not set");  }
                return _deltedOwenerId;
            }
            set => _deltedOwenerId = value;
        }

        public bool IsDeletedOwnerVirtual
        {
            get
            {
                return _isDeletedOwnerVirtual;
            }
            set => _isDeletedOwnerVirtual = value; }
    }
}
