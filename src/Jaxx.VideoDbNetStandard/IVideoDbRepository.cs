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
        IEnumerable<videodb_videodata> GetVideoDataForUser(int UserId);
        videodb_videodata GetVideoDataById(int VideoId);

        VideoDbMovie GetVideoData(int Id);

        /// <summary>
        /// Returns a list of all available genres in the videodb repository.
        /// </summary>
        /// <returns></returns>
        IEnumerable<videodb_genres> GetGenres();

        /// <summary>
        /// Returns a list of videodata where title contains the given title string
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        IEnumerable<videodb_videodata> GetVideoDataByTitle(string title);

        /// <summary>
        /// Returns a list von videodata where plot is emtpy or incomplete.
        /// </summary>
        /// <returns></returns>
        IEnumerable<videodb_videodata> GetVideoDataWithIncompletePlot();

        /// <summary>
        /// Returns all genres to the video with the given id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        IEnumerable<string> GetGenresForVideo(int VideoId);

        /// <summary>
        /// Returns the name of the user.
        /// </summary>
        /// <param name="UserId">UserId</param>
        /// <returns></returns>
        string GetUserName (int UserId);

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
        IEnumerable<VideoDbMovie> GetMoviesByGenre(List<string> GenreNames);

    }
}
