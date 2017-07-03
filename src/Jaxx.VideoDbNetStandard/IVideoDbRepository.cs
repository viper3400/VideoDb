using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jaxx.VideoDbNetStandard.DatabaseModel;
using Jaxx.VideoDbNetStandard.BusinessModel;

namespace Jaxx.VideoDbNetStandard
{
    public interface  IVideoDbRepository
    {
        #region Public Methods

        /// <summary>
        /// Deletes record and all of it's denpencies from videodb.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool DeleteVideo(int id);

        /// <summary>
        /// Get all records without filtering
        /// </summary>
        /// <returns></returns>
        IEnumerable<videodb_videodata> GetAllRecords();        

        /// <summary>
        /// Returns a list of all available genres in the videodb repository.
        /// </summary>
        /// <returns></returns>
        IEnumerable<videodb_genres> GetAvailableGenres();

        /// <summary>
        /// Returns a list of all available media types in the videodb repository.
        /// </summary>
        /// <returns></returns>
        IEnumerable<videodb_mediatypes> GetAvailableMediaTypes();

        /// <summary>
        /// Returns a list of all available users in the videodb repository.
        /// </summary>
        /// <returns></returns>
        IEnumerable<videodb_users> GetAvailableUsers();

        /// <summary>
        /// Returns all genres to the video with the given id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        IEnumerable<string> GetGenresForVideo(int VideoId);

        /// <summary>
        /// Returns the media type string for the given id.
        /// </summary>
        /// <param name="MediaTypeId"></param>
        /// <returns></returns>
        string GetMediaType(int MediaTypeId);

        /// <summary>
        /// Returns a list of movies matching to the given genre.
        /// </summary>
        /// <param name="GenreName"></param>
        /// <returns></returns>
        IEnumerable<videodb_videodata> GetMoviesByGenre(List<string> GenreNames);

        /// <summary>
        /// Returns the name of the user.
        /// </summary>
        /// <param name="UserId">UserId</param>
        /// <returns></returns>
        string GetUserName(int UserId);

        /// <summary>
        /// Get the videodata record with the given id.
        /// </summary>
        /// <param name="VideoId"></param>
        /// <returns>Returns the videodata object.</returns>
        videodb_videodata GetVideoDataById(int VideoId);

        /// <summary>
        /// Returns a list of videodata where title contains the given title string
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        IEnumerable<videodb_videodata> GetVideoDataByTitle(string title);

        IEnumerable<videodb_videodata> GetVideoDataForUser(int UserId);

        /// <summary>
        /// Returns a list von videodata where plot is emtpy or incomplete.
        /// </summary>
        /// <returns></returns>
        IEnumerable<videodb_videodata> GetVideoDataWithIncompletePlot();

        /// <summary>
        /// Creates a new record if the id of the given object is 0.
        /// Otherwise the record with the given id will be updated.
        /// </summary>
        /// <param name="Video"></param>
        /// <returns>Returns the id of the record.</returns>
        int InsertOrUpdateVideo(videodb_videodata Video);

        #endregion Public Methods
    }
}
