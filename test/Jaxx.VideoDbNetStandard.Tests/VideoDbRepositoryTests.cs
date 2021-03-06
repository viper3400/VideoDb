﻿/******************************************************************************************
 * 
 * If you cloned this repository from GitHub you need to configure database access. 
 * Therefore:
 * - Copy ClientSecrets.json.example and rename it to ClientSecrets.json.
 * - Set output of this file to "Copy always" in solution properties.
 * - Open the file and configure your db connection string.
 * 
 *****************************************************************************************/

using Jaxx.VideoDbNetStandard.MySql;
using Jaxx.VideoDbNetStandard.DatabaseModel;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Jaxx.VideoDbNetStandard.Tests
{
    public class VideoDbRepositoryTests
    {
        public VideoDbRepositoryTests()
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
        /// Catch all videos for a user
        /// </summary>
        [Fact]
        public void UserVideosOverRepositoryTest()
        {
            IVideoDbRepository _videoDbRepostiory
                = new VideoDbRepository(
                    VideoDbContextFactory.Create(connectionString),
                    new Microsoft.Extensions.Caching.Memory.MemoryCache(
                        new Microsoft.Extensions.Caching.Memory.MemoryCacheOptions()));

            var videos = _videoDbRepostiory.GetVideoDataForUser(3);
            Assert.True(videos.Count() > 0);
        }

        /// <summary>
        /// Get video with the given id.
        /// </summary>
        [Fact]
        public void GetVideoDataOverRepositoryTest()
        {
            IVideoDbRepository _videoDbRepostiory
                = new VideoDbRepository(VideoDbContextFactory.Create(connectionString));

            var videos = _videoDbRepostiory.GetVideoDataById(52);
            Assert.True(videos.id == 52, "Wrong id.");
            Assert.Equal("Entführer & Gentlemen", videos.title);
            Assert.True(videos.VideoGenres.Count() == 1);
            Assert.Equal("Comedy", videos.VideoGenres.FirstOrDefault().Genre.name);
            Assert.Equal(2, videos.owner_id);
        }

        /// <summary>
        /// Get video with not available id
        /// </summary>
        [Fact]
        public void GetVideoDataWithUnvailableId()
        {
            IVideoDbRepository _videoDbRepostiory
                = new VideoDbRepository(VideoDbContextFactory.Create(connectionString));

            var video = _videoDbRepostiory.GetVideoDataById(82);
            Assert.Null(video);

        }

        /// <summary>
        /// Tests if available genres could be fetched from db (case insensitive)
        /// </summary>
        [Fact]
        public void GetAvailableVideoGenresTest()
        {
            IVideoDbRepository _videoDbRepostiory
                = new VideoDbRepository(
                    VideoDbContextFactory.Create(connectionString),
                    new Microsoft.Extensions.Caching.Memory.MemoryCache(
                        new Microsoft.Extensions.Caching.Memory.MemoryCacheOptions()));

            var genres = _videoDbRepostiory.GetAvailableGenres();
            // search should be case insensitive
            Assert.True(genres.Where(g => g.name == "Adventure").Count() > 0);
            // should use cache
            genres = _videoDbRepostiory.GetAvailableGenres();
            Assert.True(genres.Where(g => g.name == "Adventure").Count() > 0);
        }

        /// <summary>
        /// Tests if available media types could be fetched from db.
        /// </summary>
        [Fact]
        public void GetAvailableMediaTypesTest()
        {
            IVideoDbRepository _videoDbRepostiory
                = new VideoDbRepository(
                    VideoDbContextFactory.Create(connectionString),
                    new Microsoft.Extensions.Caching.Memory.MemoryCache(
                        new Microsoft.Extensions.Caching.Memory.MemoryCacheOptions()));

            var mediaTypes = _videoDbRepostiory.GetAvailableMediaTypes();
            Assert.True(mediaTypes.Where(t => t.name == "DVD").Count() > 0);
            // should use cache
            mediaTypes = _videoDbRepostiory.GetAvailableMediaTypes();
            Assert.True(mediaTypes.Where(t => t.name == "DVD").Count() > 0);
        }

        /// <summary>
        /// Tests if available users could be fetched from db.
        /// </summary>
        [Fact]
        public void GetAvailableUsersTest()
        {
            IVideoDbRepository _videoDbRepostiory
                = new VideoDbRepository(
                    VideoDbContextFactory.Create(connectionString),
                    new Microsoft.Extensions.Caching.Memory.MemoryCache(
                        new Microsoft.Extensions.Caching.Memory.MemoryCacheOptions()));

            var users = _videoDbRepostiory.GetAvailableUsers();
            Assert.True(users.Where(t => t.name == "Jan").Count() > 0);
            // should use cache
            users = _videoDbRepostiory.GetAvailableUsers();
            Assert.True(users.Where(t => t.name == "Jan").Count() > 0);
        }



        /// <summary>
        /// Get videos matching the title (case insensitive)
        /// </summary>
        [Fact]
        public void GetVideoDataByTitleTest()
        {
            IVideoDbRepository _videoDbRepostiory
                = new VideoDbRepository(VideoDbContextFactory.Create(connectionString));

            // call search with small letters "batman"
            var actual = _videoDbRepostiory.GetVideoDataByTitle("batman");

            Assert.True(actual.Where(v => v.title.Contains("Batman")).Count() > 0);
        }

        [Fact]
        public void InsertOrUpdateVideoDataTest()
        {
            var video = new videodb_videodata();
            video.title = "TestVideo";

            // plot was cut after 256 items, see https://github.com/viper3400/VideoDb/issues/1
            var plotBuilder = new StringBuilder();
            plotBuilder.Append("Lorem ipsum dolor sit amet, causae honestatis his eu. Justo quaeque gubergren at per, probo brute nihil vis no.");
            plotBuilder.Append("His id salutandi consectetuer. In qualisque incorrupte vis, iudico aliquip mei et, ei adolescens voluptaria duo. ");
            plotBuilder.Append("Atqui populo singulis cum at. In causae labores vivendo sea. In brute oporteat appellantur sed.");
            plotBuilder.Append("Cu odio error laboramus his, percipit iracundia argumentum eam te, ea tamquam utroque constituto mei. ");
            plotBuilder.Append("Odio albucius elaboraret ei est. Iudico possim nam ne, cu iusto efficiantur vix. ");
            plotBuilder.Append("Te eum simul albucius conclusionemque, et cum nulla eruditi intellegam. Impetus singulis no mel.");
            plotBuilder.Append("Usu quidam accumsan concludaturque an, pri dico inimicus.");
            video.plot = plotBuilder.ToString(); ;
            video.owner_id = 3;

            IVideoDbRepository _videoDbRepository
                = new VideoDbRepository(VideoDbContextFactory.Create(connectionString));

            var id = _videoDbRepository.InsertOrUpdateVideo(video);
            Assert.True(id != 0);

            var actual = _videoDbRepository.GetVideoDataById(id);

            Assert.Equal("TestVideo", actual.title);
            Assert.Equal(plotBuilder.ToString(), actual.plot);
            Assert.Equal(3, actual.owner_id);

            video.title = "TestVideo (Updated)";
            var updatedId = _videoDbRepository.InsertOrUpdateVideo(video);

            Assert.Equal(id, updatedId);

            Assert.Equal("TestVideo (Updated)", actual.title);
            Assert.Equal(plotBuilder.ToString(), actual.plot);
            Assert.Equal(3, actual.owner_id);

            var deleted = _videoDbRepository.DeleteVideo(updatedId);
            Assert.True(deleted);

            var deletedVideo = _videoDbRepository.GetVideoDataById(updatedId);
            Assert.Null(deletedVideo);
        }

        [Fact]
        public void DynamicWhereClauseTest()
        {
            var _videoDbRepository
               = new VideoDbRepository(VideoDbContextFactory.Create(connectionString));

            var result = _videoDbRepository.GetVideoDataDynamic("id=500").FirstOrDefault();
            Assert.Equal("In 80 Tagen um die Welt", result.title);
        }
    }
}
