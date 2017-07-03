using System;
using System.Collections.Generic;
using System.Text;

namespace Jaxx.VideoDbNetStandard.BusinessLogic
{
    /// <summary>
    /// Exception thrown, if a disk id don't match the expected pattern.
    /// </summary>
    public class DiskIdException : Exception
    {
        /// <summary>
        /// DiskIdException
        /// </summary>
        public DiskIdException()
            : base()
        { }

        /// <summary>
        /// DiskIdException
        /// </summary>
        /// <param name="message"></param>
        public DiskIdException(string message)
            : base(message)
        { }

        /// <summary>
        /// DiskIdException
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public DiskIdException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }

}
