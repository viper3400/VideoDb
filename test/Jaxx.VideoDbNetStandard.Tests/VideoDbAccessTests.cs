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
        /// Test if an "ü" is supported.
        /// </summary>
        [Fact]
        public void CharSetTest()
        {
            var id = 2472;
            var expectedMovie = "Kirschblüten und rote Bohnen";

            using (var context = VideoDbContextFactory.Create(connectionString))
            {
                var result = context.VideoData.Where(v => v.id == id);
                var actual = result.FirstOrDefault().title;

                Assert.Equal(expectedMovie, actual);
            }
        }

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

        [Fact]
        public void UserVideosOverRepositoryTest()
        {
            IVideoDbRepository _videoDbRepostiory
                = new VideoDbRepository(VideoDbContextFactory.Create(connectionString));

            var videos = _videoDbRepostiory.GetVideoDataForUser(3);
            Assert.True(videos.Count() > 0);
        }

        [Fact]
        public void GetVideoDataOverRepositoryTest()
        {
            IVideoDbRepository _videoDbRepostiory
                = new VideoDbRepository(VideoDbContextFactory.Create(connectionString));

            var videos = _videoDbRepostiory.GetVideoDataById(52);
            Assert.True(videos.id == 52,"Wrong id.");
            Assert.Equal("Entführer & Gentlemen", videos.title);
            Assert.Equal("Jan", videos.VideoOwner.name);
        }

        [Fact]
        public void GetAvailableVideoGenresTest()
        {
            IVideoDbRepository _videoDbRepostiory
                = new VideoDbRepository(VideoDbContextFactory.Create(connectionString));

            var genres = _videoDbRepostiory.GetGenres();
            Assert.True(genres.Where(g => g.name == "Adventure").Count() > 0);
        }
        [Fact]
        public void GetVideoDataByTitleTest()
        {
            IVideoDbRepository _videoDbRepostiory
                = new VideoDbRepository(VideoDbContextFactory.Create(connectionString));

            var actual = _videoDbRepostiory.GetVideoDataByTitle("Batman");

            Assert.True(actual.Where(v => v.title.Contains("Batman")).Count() > 0);
                
        }

        [Fact]
        public void GetVideoDbMovieData()
        {
            IEnhancedVideoDbRepository _videoDbRepostiory
                = new EnhancedVideoDbRepository(
                    new VideoDbRepository(VideoDbContextFactory.Create(connectionString)),
                    EnhancedVideoDbContextFactory.Create(connectionString));

            var actual = _videoDbRepostiory.GetVideoData(1249);

            Assert.Equal(1249, actual.Id);
            Assert.Equal("Der Zoowärter", actual.Title);
            Assert.True(string.IsNullOrWhiteSpace(actual.DigitalVideoFilePath));
            Assert.Equal("R08F3D02", actual.DiskId);
            Assert.Equal("./coverpics/1249.jpg", actual.ImgUrl);
            Assert.Equal("101", actual.Length);
            Assert.NotNull(actual.Plot);
            Assert.Equal("5.79", actual.Rating);
            Assert.True(string.IsNullOrWhiteSpace(actual.SubTitle));
            Assert.Equal("Daniel", actual.Owner);
            Assert.Equal(3, actual.OwnerId);
           
            Assert.Equal(4, actual.Genres.Count());
            Assert.Collection(actual.Genres,
                a => { Assert.Equal("Comedy", a); },
                a => { Assert.Equal("Documentary", a); },
                a => { Assert.Equal("Family", a); },
                a => { Assert.Equal("Romance", a); }
                );

            Assert.Equal("Blu-ray", actual.MediaType);
            Assert.Equal(16, actual.MediaTypeId);
        }

        [Fact]
        public void GetVideoDbMovieByGenreTest()
        {
            IEnhancedVideoDbRepository _videoDbRepostiory
                  = new EnhancedVideoDbRepository(
                      new VideoDbRepository(VideoDbContextFactory.Create(connectionString)),
                      EnhancedVideoDbContextFactory.Create(connectionString));

            var actual = _videoDbRepostiory.GetMoviesByGenre(new List<string> { "Sci-Fi","Action" });
            Assert.Equal(122, actual.Count());
        }

    }
}
