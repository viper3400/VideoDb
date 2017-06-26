/******************************************************************************************
 * 
 * If you cloned this repository from GitHub you need to configure database access. 
 * Therefore:
 * - Copy ClientSecrets.json.example and rename it to ClientSecrets.json.
 * - Set output of this file to "Copy always" in solution properties.
 * - Open the file and configure your db connection string.
 * 
 *****************************************************************************************/

using Xunit;
using Jaxx.VideoDbNetStandard.MySql;
using Jaxx.VideoDbNetStandard;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace Jaxx.VideoDbNetStandard.Tests
{
    public class VideoDbAccessTests
    {
        public VideoDbAccessTests()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("ClientSecrets.json")
                .Build();

            connectionString = config["TESTDB_CONNECTIONSTRING"];
        }

        private string connectionString;

        /// <summary>
        /// Test if all tables on database are accessible via EF
        /// </summary>
        [Fact]
        public void AccessibiltiyOfTables()
        {
            using (var context = VideoDbContextFactory.Create(connectionString))
            {
                var result = context.Actors.ToList();
                context.Cache.ToList();
                context.Config.ToList();
                context.Genre.ToList();
                context.Genres.ToList();
                context.Lent.ToList();
                context.MediaTypes.ToList();
                context.Permissions.ToList();
                context.UserConfig.ToList();
                var users = context.Users.ToList();
                context.UserSeen.ToList();
                var videoData = context.VideoData.ToList();
                Assert.True(videoData.FirstOrDefault().VideoOwner.id > 0);
                Assert.True(users.Where(u => u.id == 3).FirstOrDefault().UserVideos.Count() > 0);
            }
        }       
    }
}
