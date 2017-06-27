using Jaxx.VideoDbNetStandard.BusinessModel;
using Jaxx.VideoDbNetStandard.DatabaseModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Jaxx.VideoDbNetStandard.MySql
{
    public class EnhancedVideoDbRepository : IEnhancedVideoDbRepository
    {
        #region Private Fields

        private readonly EnhancedVideoDbContext _enhancedVideoDbContext;
        private IVideoDbRepository _videoDbRepo;

        #endregion Private Fields

        #region Public Constructors

        public EnhancedVideoDbRepository(IVideoDbRepository videoDbRepo, EnhancedVideoDbContext enhancedVideoDbContext)
        {
            _videoDbRepo = videoDbRepo;
            _enhancedVideoDbContext = enhancedVideoDbContext;
        }

        #endregion Public Constructors

        #region Public Methods

        public IEnumerable<VideoDbMovie> GetMovieByTitle(string Title)
        {
            var movies = _videoDbRepo.GetVideoDataByTitle(Title);
            return ConvertToVideoDbMovie(movies.ToList()).ToList();

        }

        /// <summary>
        /// Returns a list of movies matching to the given genre.
        /// </summary>
        /// <param name="GenreName"></param>
        /// <returns></returns>
        public IEnumerable<VideoDbMovie> GetMoviesByGenre(List<string> GenreNames)
        {
            var movies = _videoDbRepo.GetMoviesByGenre(GenreNames);

            var resultList = ConvertToVideoDbMovie(movies.ToList()).ToList();
            return resultList;
        }

        public VideoDbMovie GetVideoData(int Id)
        {
            var movie = _videoDbRepo.GetVideoDataById(Id);
            var movieResult = ConvertToVideoDbMovie(movie);

            return movieResult;
        }

        #endregion Public Methods

        #region Private Methods

        /// <summary>
        /// Converts a videodb_videodata object to VideoDbMovie object.
        /// Functions makes additional queries to fill owner name, mediatype name and genres.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        private VideoDbMovie ConvertToVideoDbMovie(videodb_videodata source)
        {
            var converted = new VideoDbMovie
            {
                DigitalVideoFilePath = source.filename,
                DiskId = source.diskid,
                Id = source.id,
                ImgUrl = source.imgurl,
                Length = source.runtime.ToString(),
                Plot = source.plot,
                Rating = source.rating,
                SubTitle = source.subtitle,
                Title = source.title,
                OwnerId = source.owner_id,
                MediaTypeId = source.mediatype
            };

            converted.Genres = _videoDbRepo.GetGenresForVideo(source.id).ToList();
            converted.Owner = _videoDbRepo.GetUserName(source.owner_id);
            converted.MediaType = _videoDbRepo.GetMediaType(source.mediatype);

            return converted;
        }

        /// <summary>
        /// Converts an IEnuerable of videodb_videodata object to an IEnumerable of VideoDbMovie object.
        /// Functions makes additional queries to fill owner name, mediatype name and genres.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        private IEnumerable<VideoDbMovie> ConvertToVideoDbMovie(IEnumerable<videodb_videodata> source)
        {
            var convertedMovieList = new List<VideoDbMovie>();

            foreach (var movie in source)
            {
                var convertedMovie = ConvertToVideoDbMovie(movie);
                convertedMovieList.Add(convertedMovie);
            }

            return convertedMovieList;
        }

        #endregion Private Methods
    }
}