using Jaxx.VideoDbNetStandard.BusinessModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jaxx.VideoDbNetStandard
{
    public interface IEnhancedVideoDbRepository
    {
        /// <summary>
        /// Returns a list of movies matching to the given genre.
        /// </summary>
        /// <param name="GenreName"></param>
        /// <returns></returns>
        IEnumerable<VideoDbMovie> GetMoviesByGenre(List<string> GenreNames);

        IEnumerable<VideoDbMovie> GetMovieByTitle(string Title);

        VideoDbMovie GetVideoData(int Id);
    }
}
