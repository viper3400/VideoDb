using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Jaxx.VideoDbNetStandard.BusinessLogic
{
    /// <summary>
    /// Represents a disk id object. The disk id is the unique identificator for
    /// each disk in the collection, which helps to physical locate the object
    /// (in example in a shelf)
    /// </summary>
    public class DiskId
    {
        public readonly string Id;

        private bool isAvailable;

        public DiskId(string DiskId)
        {
            if (IsValidDiskID(DiskId))
                this.Id = DiskId;
            else throw new DiskIdException(DiskId + ": Das ist keine gültige DiskID");
        }

        public string ToString()
        {
            return this.Id;
        }

        /// <summary>
        /// Checks if the this id matches the pattern.
        /// </summary>
        /// <param name="DiskID"></param>
        /// <returns></returns>
        public static bool IsValidDiskID(string DiskID)
        {
            bool result = false;

            if (DiskID != null)
            {
                Regex regex = new Regex(@"R\d{2}F\d{1,2}D\d{2}");
                result = regex.IsMatch(DiskID);
            }

            return result;
        }
    }
}
