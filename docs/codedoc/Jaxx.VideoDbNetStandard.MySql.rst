Jaxx.VideoDbNetStandard.MySql
-----------------------------

T:Jaxx.VideoDbNetStandard.MySql.EnhancedVideoDbContextFactory
=============================================================

Factory class for EmployeesContext

M:Jaxx.VideoDbNetStandard.MySql.EnhancedVideoDbRepository.ConvertToVideoDbMovie(Jaxx.VideoDbNetStandard.DatabaseModel.videodb\_videodata)
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

Converts a videodb\_videodata object to VideoDbMovie object. Functions
makes additional queries to fill owner name, mediatype name and genres.

+-----------+---------------+
| Name      | Description   |
+===========+===============+
| source:   |               |
+-----------+---------------+

Returns:

M:Jaxx.VideoDbNetStandard.MySql.EnhancedVideoDbRepository.FilterDeletedRecords(System.Collections.Generic.IEnumerable{Jaxx.VideoDbNetStandard.DatabaseModel.videodb\_videodata})
''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

This method checks, if options "FilterDeletedRecords" is set. If so, the
records with the owener id given in option "DeletedOwnerId" will be
removed from result set.

+-----------+---------------+
| Name      | Description   |
+===========+===============+
| movies:   |               |
+-----------+---------------+

Returns:

M:Jaxx.VideoDbNetStandard.MySql.VideoDbRepository.GetAvailableGenres
''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

Returns all available genres from db (uses caching).

Returns:

M:Jaxx.VideoDbNetStandard.MySql.VideoDbRepository.GetAvailableUsers
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

Returns all available user from db (uses caching).

Returns:

M:Jaxx.VideoDbNetStandard.MySql.VideoDbRepository.GetUserName(System.Int32)
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

Returns the name of the user.

+-----------+---------------+
| Name      | Description   |
+===========+===============+
| UserId:   | UserId        |
+-----------+---------------+

Returns:

M:Jaxx.VideoDbNetStandard.MySql.VideoDbRepository.GetVideoDataWithIncompletePlot
''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

Returns a list von videodata where plot is emtpy or incomplete.

Returns:
