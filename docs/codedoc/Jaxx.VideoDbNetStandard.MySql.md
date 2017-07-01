## Jaxx.VideoDbNetStandard.MySql ##

# T:Jaxx.VideoDbNetStandard.MySql.EnhancedVideoDbContextFactory


Factory class for EmployeesContext




---
##### M:Jaxx.VideoDbNetStandard.MySql.EnhancedVideoDbRepository.GetMoviesByGenre(System.Collections.Generic.List{System.String})


Returns a list of movies matching to the given genre.


|Name | Description |
|-----|------|
|GenreName: ||
Returns: 



---
##### M:Jaxx.VideoDbNetStandard.MySql.EnhancedVideoDbRepository.ConvertToVideoDbMovie(Jaxx.VideoDbNetStandard.DatabaseModel.videodb_videodata)


Converts a videodb_videodata object to VideoDbMovie object.
Functions makes additional queries to fill owner name, mediatype name and genres.


|Name | Description |
|-----|------|
|source: ||
Returns: 



---
##### M:Jaxx.VideoDbNetStandard.MySql.EnhancedVideoDbRepository.ConvertToVideoDbMovie(System.Collections.Generic.IEnumerable{Jaxx.VideoDbNetStandard.DatabaseModel.videodb_videodata})


Converts an IEnuerable of videodb_videodata object to an IEnumerable of VideoDbMovie object.
Functions makes additional queries to fill owner name, mediatype name and genres.


|Name | Description |
|-----|------|
|source: ||
Returns: 



---
##### M:Jaxx.VideoDbNetStandard.MySql.EnhancedVideoDbRepository.FilterDeletedRecords(System.Collections.Generic.IEnumerable{Jaxx.VideoDbNetStandard.DatabaseModel.videodb_videodata})


This method checks, if options "FilterDeletedRecords" is set. If so, the records with the owener id
given in option "DeletedOwnerId" will be removed from result set.


|Name | Description |
|-----|------|
|movies: ||
Returns: 



---
# T:Jaxx.VideoDbNetStandard.MySql.VideoDbContextFactory


Factory class for EmployeesContext




---
##### M:Jaxx.VideoDbNetStandard.MySql.VideoDbRepository.GetAvailableGenres


Returns all available genres from db (uses caching).


Returns: 



---
##### M:Jaxx.VideoDbNetStandard.MySql.VideoDbRepository.GetAvailableMediaTypes


Returns all available media types from db (uses caching).


Returns: 



---
##### M:Jaxx.VideoDbNetStandard.MySql.VideoDbRepository.GetAvailableUsers


Returns all available user from db (uses caching).


Returns: 



---
##### M:Jaxx.VideoDbNetStandard.MySql.VideoDbRepository.GetMediaType(System.Int32)


Returns the media type string for the given id.


|Name | Description |
|-----|------|
|MediaTypeId: ||
Returns: 



---
##### M:Jaxx.VideoDbNetStandard.MySql.VideoDbRepository.GetUserName(System.Int32)


Returns the name of the user.


|Name | Description |
|-----|------|
|UserId: |UserId|
Returns: 



---
##### M:Jaxx.VideoDbNetStandard.MySql.VideoDbRepository.GetVideoDataById(System.Int32)


Get the videodata record with the given id.


|Name | Description |
|-----|------|
|VideoId: ||
Returns: Returns the videodata object.



---
##### M:Jaxx.VideoDbNetStandard.MySql.VideoDbRepository.GetVideoDataWithIncompletePlot


Returns a list von videodata where plot is emtpy or incomplete.


Returns: 



---
##### M:Jaxx.VideoDbNetStandard.MySql.VideoDbRepository.InsertOrUpdateVideo(Jaxx.VideoDbNetStandard.DatabaseModel.videodb_videodata)


Creates a new record if the id of the given object is 0.
Otherwise the record with the given id will be updated.


|Name | Description |
|-----|------|
|Video: ||
Returns: Returns the id of the record.



---


