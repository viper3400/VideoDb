## Jaxx.VideoDbNetStandard ##

# T:Jaxx.VideoDbNetStandard.BusinessModel.VideoDbMovie


This is the presentation model for video list.




---
##### P:Jaxx.VideoDbNetStandard.BusinessModel.VideoDbMovie.MediaType


MediaType of the video




---
##### P:Jaxx.VideoDbNetStandard.BusinessModel.VideoDbMovie.MediaTypeId


Id of the MediaType




---
##### P:Jaxx.VideoDbNetStandard.BusinessModel.VideoDbMovie.Owner


Owner of the video




---
##### P:Jaxx.VideoDbNetStandard.BusinessModel.VideoDbMovie.OwnerId


OwenerId




---
##### P:Jaxx.VideoDbNetStandard.BusinessModel.VideoDbMovie.DigitalVideoFilePath


The file path of a video which is available as digital copy or file.




---
##### P:Jaxx.VideoDbNetStandard.BusinessModel.VideoDbMovie.LastViewDate


The date when the movie has been marked as seen the last time.




---
##### P:Jaxx.VideoDbNetStandard.BusinessModel.VideoDbMovie.DaysSinceLastView


Days since the movie was seen for the last time




---
##### P:Jaxx.VideoDbNetStandard.BusinessModel.VideoDbMovie.OverallViewCount


Count of how many times the movie has been seen overall




---
##### P:Jaxx.VideoDbNetStandard.BusinessModel.VideoDbMovie.LastSeenSentence


Provides a sentence which describes all last seen informations.




---
##### P:Jaxx.VideoDbNetStandard.BusinessModel.VideoDbMovie.IsFavorite


Indicates if this movie is marked as favorite by the current user.




---
##### P:Jaxx.VideoDbNetStandard.BusinessModel.VideoDbMovie.IsWatchAgain


Indicates if this movie is marked as "WatchAgain" by the current user.




---
##### P:Jaxx.VideoDbNetStandard.IEnhancedVideoDbOptions.DeletedOwnerId


Gets or sets the owner id. If a record has this owner id it is considered as deleted.




---
##### P:Jaxx.VideoDbNetStandard.IEnhancedVideoDbOptions.IsDeletedOwnerVirtual


Gets or sets the idicator, if the owner of delted records is just a virtual owner or if
the owner is existing at database.




---
##### P:Jaxx.VideoDbNetStandard.IEnhancedVideoDbOptions.FilterDeletedRecords


Gets or sets the indicator, if records which are marked as deleted should be filtered from
search results by default.




---
##### M:Jaxx.VideoDbNetStandard.IEnhancedVideoDbRepository.GetMoviesByGenre(System.Collections.Generic.List{System.String})


Returns a list of movies matching to the given genre.


|Name | Description |
|-----|------|
|GenreName: ||
Returns: 



---
##### M:Jaxx.VideoDbNetStandard.IVideoDbRepository.DeleteVideo(System.Int32)


Deletes record and all of it's denpencies from videodb.


|Name | Description |
|-----|------|
|id: ||
Returns: 



---
##### M:Jaxx.VideoDbNetStandard.IVideoDbRepository.GetAvailableGenres


Returns a list of all available genres in the videodb repository.


Returns: 



---
##### M:Jaxx.VideoDbNetStandard.IVideoDbRepository.GetAvailableMediaTypes


Returns a list of all available media types in the videodb repository.


Returns: 



---
##### M:Jaxx.VideoDbNetStandard.IVideoDbRepository.GetAvailableUsers


Returns a list of all available users in the videodb repository.


Returns: 



---
##### M:Jaxx.VideoDbNetStandard.IVideoDbRepository.GetGenresForVideo(System.Int32)


Returns all genres to the video with the given id


|Name | Description |
|-----|------|
|Id: ||
Returns: 



---
##### M:Jaxx.VideoDbNetStandard.IVideoDbRepository.GetMediaType(System.Int32)


Returns the media type string for the given id.


|Name | Description |
|-----|------|
|MediaTypeId: ||
Returns: 



---
##### M:Jaxx.VideoDbNetStandard.IVideoDbRepository.GetMoviesByGenre(System.Collections.Generic.List{System.String})


Returns a list of movies matching to the given genre.


|Name | Description |
|-----|------|
|GenreName: ||
Returns: 



---
##### M:Jaxx.VideoDbNetStandard.IVideoDbRepository.GetUserName(System.Int32)


Returns the name of the user.


|Name | Description |
|-----|------|
|UserId: |UserId|
Returns: 



---
##### M:Jaxx.VideoDbNetStandard.IVideoDbRepository.GetVideoDataById(System.Int32)


Get the videodata record with the given id.


|Name | Description |
|-----|------|
|VideoId: ||
Returns: Returns the videodata object.



---
##### M:Jaxx.VideoDbNetStandard.IVideoDbRepository.GetVideoDataByTitle(System.String)


Returns a list of videodata where title contains the given title string


|Name | Description |
|-----|------|
|title: ||
Returns: 



---
##### M:Jaxx.VideoDbNetStandard.IVideoDbRepository.GetVideoDataWithIncompletePlot


Returns a list von videodata where plot is emtpy or incomplete.


Returns: 



---
##### M:Jaxx.VideoDbNetStandard.IVideoDbRepository.InsertOrUpdateVideo(Jaxx.VideoDbNetStandard.DatabaseModel.videodb_videodata)


Creates a new record if the id of the given object is 0.
Otherwise the record with the given id will be updated.


|Name | Description |
|-----|------|
|Video: ||
Returns: Returns the id of the record.



---


