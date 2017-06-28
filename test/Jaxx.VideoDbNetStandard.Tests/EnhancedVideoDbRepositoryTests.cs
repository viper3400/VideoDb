/******************************************************************************************
 * 
 * If you cloned this repository from GitHub you need to configure database access. 
 * Therefore:
 * - Copy ClientSecrets.json.example and rename it to ClientSecrets.json.
 * - Set output of this file to "Copy always" in solution properties.
 * - Open the file and configure your db connection string.
 * 
 *****************************************************************************************/

using Jaxx.VideoDbNetStandard.MySql;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Jaxx.VideoDbNetStandard.Tests
{
    public class EnhancedVideoDbRepositoryTests
    {
        public EnhancedVideoDbRepositoryTests()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("ClientSecrets.json")
                .Build();

            connectionString = config["TESTDB_CONNECTIONSTRING"];

            _options = new EnhancedVideoDbOptions
            {
                DeletedOwnerId = 999,
                IsDeletedOwnerVirtual = true
            };

        }

        private string connectionString;
        private IEnhancedVideoDbOptions _options;

        [Fact]
        public void GetVideoDbMovieData()
        {
            IEnhancedVideoDbRepository _videoDbRepostiory
                = new EnhancedVideoDbRepository(
                    new VideoDbRepository(VideoDbContextFactory.Create(connectionString),
                     new Microsoft.Extensions.Caching.Memory.MemoryCache(
                        new Microsoft.Extensions.Caching.Memory.MemoryCacheOptions())),
                    EnhancedVideoDbContextFactory.Create(connectionString),
                    _options);

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
                    new VideoDbRepository(VideoDbContextFactory.Create(connectionString),
                     new Microsoft.Extensions.Caching.Memory.MemoryCache(
                        new Microsoft.Extensions.Caching.Memory.MemoryCacheOptions())),
                    EnhancedVideoDbContextFactory.Create(connectionString),
                    _options);

            var actual = _videoDbRepostiory.GetMoviesByGenre(new List<string> { "Sci-Fi", "Action" });
            Assert.Equal(122, actual.Count());
        }

        [Fact]
        public void DeletedOwnerTests()
        {
            IEnhancedVideoDbRepository _enhancedRepo
               = new EnhancedVideoDbRepository(
                   new VideoDbRepository(VideoDbContextFactory.Create(connectionString),
                    new Microsoft.Extensions.Caching.Memory.MemoryCache(
                       new Microsoft.Extensions.Caching.Memory.MemoryCacheOptions())),
                   EnhancedVideoDbContextFactory.Create(connectionString),
                   _options);

            var video = new DatabaseModel.videodb_videodata();
            video.title = "TestVideoDeleted";
            video.plot = "TestPlot";
            video.owner_id = _options.DeletedOwnerId;

            IVideoDbRepository _videoDbRepository
                = new VideoDbRepository(VideoDbContextFactory.Create(connectionString));

            var id = _videoDbRepository.InsertOrUpdateVideo(video);

            var actual =_enhancedRepo.GetMovieByTitle("TestVideoDeleted");
            Assert.True(actual.Count() == 0);

            var deleted = _enhancedRepo.GetVideoData(id);
            Assert.Equal("Video has been marked as deleted (no owner).", deleted.Owner);
            Assert.Equal(_options.DeletedOwnerId, deleted.OwnerId);

            _videoDbRepository.DeleteVideo(id);
        }

    }
}
