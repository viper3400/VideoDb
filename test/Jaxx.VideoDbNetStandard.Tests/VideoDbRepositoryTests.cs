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
                = new VideoDbRepository(VideoDbContextFactory.Create(connectionString));

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
            Assert.Equal("Jan", videos.VideoOwner.name);
        }

        /// <summary>
        /// Get videos for the given genre.
        /// </summary>
        [Fact]
        public void GetAvailableVideoGenresTest()
        {
            IVideoDbRepository _videoDbRepostiory
                = new VideoDbRepository(VideoDbContextFactory.Create(connectionString));

            var genres = _videoDbRepostiory.GetGenres();
            Assert.True(genres.Where(g => g.name == "Adventure").Count() > 0);
        }

        /// <summary>
        /// Get videos matching the title
        /// </summary>
        [Fact]
        public void GetVideoDataByTitleTest()
        {
            IVideoDbRepository _videoDbRepostiory
                = new VideoDbRepository(VideoDbContextFactory.Create(connectionString));

            var actual = _videoDbRepostiory.GetVideoDataByTitle("Batman");

            Assert.True(actual.Where(v => v.title.Contains("Batman")).Count() > 0);
        }

        [Fact]
        public void InsertOrUpdateVideoDataTest()
        {
            var video = new videodb_videodata();
            video.title = "TestVideo";
            video.plot = "TestPlot";
            video.owner_id = 3;

            IVideoDbRepository _videoDbRepository
                = new VideoDbRepository(VideoDbContextFactory.Create(connectionString));

            var id = _videoDbRepository.InsertOrUpdateVideo(video);
            Assert.True(id != 0);

            var actual = _videoDbRepository.GetVideoDataById(id);

            Assert.Equal("TestVideo", actual.title);
            Assert.Equal("TestPlot", actual.plot);
            Assert.Equal(3, actual.owner_id);

            video.title = "TestVideo (Updated)";
            var updatedId = _videoDbRepository.InsertOrUpdateVideo(video);

            Assert.Equal(id, updatedId);

            Assert.Equal("TestVideo (Updated)", actual.title);
            Assert.Equal("TestPlot", actual.plot);
            Assert.Equal(3, actual.owner_id);
        }
    }
}