using Jaxx.VideoDbNetStandard.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Jaxx.VideoDbNetStandard.Tests
{
    /// <summary>
    /// Test for disk id consistence
    /// </summary>
    public class DiskIdTests
    {
        private List<string> validIds = new List<string> { "R10F5D01", "R01F12D15" };
        private List<string> invalidIds = new List<string> { "R100F5D01", "R01F12", "R01D05D05", "X03D5D01" };

        [Fact]
        public void DiskIdIsValid()
        {
            foreach (var id in validIds)
            {
                var actual = DiskId.IsValidDiskID(id);
                Assert.True(actual, $"Id: {id}");
            }
                   
        }

        [Fact]
        public void DiskIdIsInvalid()
        {
            foreach (var id in invalidIds)
            {
                var actual = DiskId.IsValidDiskID(id);
                Assert.False(actual, $"Id: {id}");
            }            
        }

        [Fact]
        public void DiskIdIsNull()
        {           
          var actual = DiskId.IsValidDiskID(null);
          Assert.False(actual, $"Id null");         
        }

        [Fact]
        public void CreateDiskIdViaConstructor()
        {
            foreach (var id in validIds)
            {
                var actual = new DiskId(id);
                Assert.True(actual.Id  == id, $"Id: {id}");                
            }
        }

        [Fact]
        public void CreateInvalidDiskIdViaConstructor()
        {
            foreach (var id in invalidIds)
            {
                Assert.Throws<DiskIdException>(()=> new DiskId(id));                                
            }
        }

        [Fact]
        public void CreateDiskIdNullViaConstructor()
        {           
            Assert.Throws<DiskIdException>(() => new DiskId(null));         
        }

    }
}
