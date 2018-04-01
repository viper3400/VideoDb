using Jaxx.VideoDbNetStandard.DatabaseModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace Jaxx.VideoDbNetStandard.MySql
{
    /// <summary>
    /// VideoDbRepository implements interface IVideoDbRepository. It's the basic repository to access videodb database directly by data model.
    /// </summary>
    public class VideoDbRepository : IVideoDbRepository
    {
        #region Private Fields

        const string GENRECACHE = "GENRECACHE";
        const string MEDIATYPECACHE = "MEDIATYPECACHE";
        const string USERSCACHE = "USERSCACHE";
        private readonly MemoryCacheEntryOptions _cacheEntryOptions;
        private readonly VideoDbContext _dbContext;
        private readonly IMemoryCache _memoryCache;
        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dbContext">Instance of the VideoDbContext</param>
        /// <param name="memoryCache">Instance of the IMemoryCache</param>
        public VideoDbRepository(VideoDbContext dbContext, IMemoryCache memoryCache = null)
        {
            _dbContext = dbContext;
            _memoryCache = memoryCache;
            _cacheEntryOptions = new MemoryCacheEntryOptions()
                  .SetSlidingExpiration(TimeSpan.FromMinutes(5));
        }

        #endregion Public Constructors

        #region Public Methods

        public bool DeleteVideo(int id)
        {
            var record = _dbContext.VideoData.Where(r => r.id == id).FirstOrDefault();
            _dbContext.VideoData.Remove(record);
            _dbContext.SaveChanges();
            return true;

        }

        /// <summary>
        /// Get all records without filtering.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<videodb_videodata> GetAllRecords()
        {
            return _dbContext.VideoData;
        }

        /// <summary>
        /// Returns all available genres from db (uses caching).
        /// </summary>
        /// <returns></returns>
        public IEnumerable<videodb_genres> GetAvailableGenres()
        {
            IEnumerable<videodb_genres> genres;

            var exists = _memoryCache.TryGetValue(GENRECACHE, out genres);
            if (!exists)
            {
                genres = _dbContext.Genres;
                _memoryCache.Set(GENRECACHE, genres, _cacheEntryOptions);
            }
            return genres;
        }

        /// <summary>
        /// Returns all available media types from db (uses caching).
        /// </summary>
        /// <returns></returns>
        public IEnumerable<videodb_mediatypes> GetAvailableMediaTypes()
        {
            IEnumerable<videodb_mediatypes> mediaTypes;

            var exists = _memoryCache.TryGetValue(MEDIATYPECACHE, out mediaTypes);
            if (!exists)
            {
                mediaTypes = _dbContext.MediaTypes;
                _memoryCache.Set(MEDIATYPECACHE, mediaTypes, _cacheEntryOptions);
            }

            return mediaTypes;
        }

        /// <summary>
        /// Returns all available user from db (uses caching).
        /// </summary>
        /// <returns></returns>
        public IEnumerable<videodb_users> GetAvailableUsers()
        {
            IEnumerable<videodb_users> users;

            var exists = _memoryCache.TryGetValue(USERSCACHE, out users);
            if (!exists)
            {
                users = _dbContext.Users;
                _memoryCache.Set(USERSCACHE, users, _cacheEntryOptions);
            }

            return users;
        }

        public IEnumerable<string> GetGenresForVideo(int Id)
        {
            var query = from vidgenres in _dbContext.Genre
                        where vidgenres.video_id == Id
                        join genres in GetAvailableGenres() on vidgenres.genre_id equals genres.id
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
            var query = from media in GetAvailableMediaTypes()
                        where media.id == MediaTypeId
                        select media.name;

            return query.FirstOrDefault();
        }

        public IEnumerable<videodb_videodata> GetMoviesByGenre(List<string> GenreNames)
        {
            var resultList = new List<videodb_videodata>();

            var genreIds = _dbContext.Genres
                .Where(g => GenreNames.Contains(g.name, StringComparer.OrdinalIgnoreCase))
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

        /// <summary>
        /// Get the videodata record with the given id.
        /// </summary>
        /// <param name="VideoId"></param>
        /// <returns>Returns the videodata object.</returns>
        public videodb_videodata GetVideoDataById(int VideoId)
        {
            var videoData = _dbContext.VideoData
                .Include(v => v.VideoOwner)
                .Include(g => g.VideoGenres)
                .ThenInclude(z => z.Genre)
                .Where(v => v.id == VideoId).FirstOrDefault();
           
            return videoData;
        }

        public IEnumerable<videodb_videodata> GetVideoDataByTitle(string title)
        {
            var videoData = _dbContext.VideoData                
                .Where(v => v.title.ToLower().Contains(title.ToLower()) ||
                     (v.subtitle != null && v.subtitle.ToLower().Contains(title.ToLower())));
            return videoData;
        }

        public IEnumerable<videodb_videodata> GetVideoDataForUser(int UserId)
        {
            var userVideos = _dbContext.VideoData
                .Where(v => v.owner_id == UserId);

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

        /// <summary>
        /// Creates a new record if the id of the given object is 0.
        /// Otherwise the record with the given id will be updated.
        /// </summary>
        /// <param name="Video"></param>
        /// <returns>Returns the id of the record.</returns>
        public int InsertOrUpdateVideo(videodb_videodata Video)
        {
            if (Video.id.Equals(0))
            {
                // if id is 0 we will create a new record
                Video.created = DateTime.Now;
                Video.lastupdate = DateTime.Now;
                _dbContext.VideoData.Add(Video);
                _dbContext.SaveChanges();
            }
            else
            {
                // in this case an existing record may be modified
                Video.lastupdate = DateTime.Now;
                _dbContext.Entry(Video).State = EntityState.Modified;
                _dbContext.SaveChanges();
            }

            return Video.id;
        }

        public IEnumerable<videodb_videodata> GetVideoDataDynamic(string whereClause)
        {
            var query = _dbContext.VideoData.Where(whereClause);
            return query;
        }
        #endregion Public Methods
    }
}