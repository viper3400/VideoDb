using System;
using System.Collections.Generic;
using System.Text;

namespace Jaxx.VideoDbNetStandard.BusinessModel
{

    /// <summary>
    /// This is the presentation model for video list.
    /// </summary>
    public class VideoDbMovie
    {
        public int Id { get; set; }
        public bool Deleted { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string DiskId { get; set; }
        public string ImgUrl { get; set; }
        public string Plot { get; set; }
        public string LentTo { get; set; }
        /// <summary>
        /// MediaType of the video
        /// </summary>
        public string MediaType { get; set; }
        /// <summary>
        /// Id of the MediaType
        /// </summary>
        public int MediaTypeId { get; set; }
        public string Length { get; set; }
        public List<string> Genres { get; set; }
        public string Rating { get; set; }
        /// <summary>
        /// Owner of the video
        /// </summary>
        public string Owner { get; set; }
        /// <summary>
        /// OwenerId
        /// </summary>
        public int OwnerId { get; set; }
        /// <summary>
        /// The file path of a video which is available as digital copy or file.
        /// </summary>
        public string DigitalVideoFilePath { get; set; }
        /// <summary>
        /// The date when the movie has been marked as seen the last time.
        /// </summary>
        public string LastViewDate { get; set; }
        /// <summary>
        /// Days since the movie was seen for the last time
        /// </summary>
        public string DaysSinceLastView { get; set; }
        /// <summary>
        /// Count of how many times the movie has been seen overall
        /// </summary>
        public string OverallViewCount { get; set; }
        /// <summary>
        /// Provides a sentence which describes all last seen informations.
        /// </summary>
        public string LastSeenSentence { get; set; }
        /// <summary>
        /// Indicates if this movie is marked as favorite by the current user.
        /// </summary>
        public bool IsFavorite { get; set; }
        /// <summary>
        /// Indicates if this movie is marked as "WatchAgain" by the current user.
        /// </summary>
        public bool IsWatchAgain { get; set; }        
    }
}