using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jaxx.VideoDbNetStandard.DatabaseModel;
using Jaxx.VideoDbNetStandard;
using Microsoft.EntityFrameworkCore;
using Jaxx.VideoDbNetStandard.BusinessModel;

namespace Jaxx.VideoDbNetStandard.MySql
{
    public class VideoDbRepository : IVideoDbRepository
    {
        private readonly VideoDbContext _dbContext;

        public VideoDbRepository(VideoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<videodb_genres> GetGenres()
        {
            return _dbContext.Genres;
        }

        public IEnumerable<string> GetGenresForVideo(int Id)
        {
            var query = from vidgenres in _dbContext.Genre
                        where vidgenres.video_id == Id
                        join genres in _dbContext.Genres on vidgenres.genre_id equals genres.id
                        select genres.name;

            return query.ToList();

        }


        /// <summary>
        /// Returns the media type string for the given id.
        /// </summary>
        /// <param name="MediaTypeId"></param>
        /// <returns></returns>
        public string GetMediaType(int MediaTypeId)
        {
            var query = from media in _dbContext.MediaTypes
                        where media.id == MediaTypeId
                        select media.name;

            return query.FirstOrDefault();
        }

        public IEnumerable<videodb_videodata> GetMoviesByGenre(List<string> GenreNames)
        {
            var resultList = new List<videodb_videodata>();

            var genreIds = _dbContext.Genres
                .Where(g => GenreNames.Contains(g.name))
                .Select(g => g.id)
                .ToList();

            // just go on if there are any results (otherwise the resultList will be kept empty)
            if (genreIds.Any())
            {
                var movieIdsForSelctedGenre = from genre in _dbContext.Genre
                                              where genreIds.Contains(genre.genre_id)
                                              select genre.video_id;

                // group by movieids and return just these where all genres are present
                var movieIds = movieIdsForSelctedGenre.GroupBy(m => m).Where(grp => grp.Count() == genreIds.Count()).Select(m => m.Key).ToList();

                var movies = _dbContext.VideoData
                    .Where(m => movieIds.Contains(m.id));

                resultList = movies.ToList();
            }

            return resultList;
        }

        /// <summary>
        /// Returns the name of the user.
        /// </summary>
        /// <param name="UserId">UserId</param>
        /// <returns></returns>
        public string GetUserName(int UserId)
        {
            var query = from user in _dbContext.Users
                        where user.id == UserId
                        select user.name;

            return query.FirstOrDefault();
        }

       

        public videodb_videodata GetVideoDataById(int VideoId)
        {
            var videoData = _dbContext.VideoData
                .Include(o => o.VideoOwner)
                .Where(v => v.id == VideoId).FirstOrDefault();
            return videoData;
        }

        public IEnumerable<videodb_videodata> GetVideoDataByTitle(string title)
        {
            var videoData = _dbContext.VideoData
                //.Include(o => o.VideoOwner)
                .Where(v => v.title.ToString().Contains(title));
            return videoData.ToList();
        }

        public IEnumerable<videodb_videodata> GetVideoDataForUser(int UserId)
        {
            var userVideos = _dbContext.Users
                .Include(u => u.UserVideos)
                .Where(u => u.id == UserId).FirstOrDefault()
                .UserVideos;

            return userVideos;
        }

        /// <summary>
        /// Returns a list von videodata where plot is emtpy or incomplete.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<videodb_videodata> GetVideoDataWithIncompletePlot()
        {
            var videoData = _dbContext.VideoData
                .Where(v => String.IsNullOrEmpty(v.plot));

            return videoData;
        }



    }
}
