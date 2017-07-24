using Jaxx.VideoDbNetStandard.BusinessModel;
using Jaxx.VideoDbNetStandard.DatabaseModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jaxx.VideoDbNetStandard
{
    public interface IVideoDbUserSeenHandler
    {
        /// <summary>
        /// Erstellt einen Eintag für einen Benutzer, für eine Film, für einen Tag
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="movieid"></param>
        /// <param name="date"></param>
        void SetMovieSeen(string username, int movieid, string viewgroupname, DateTime date);

        /// <summary>
        /// Erstellt einen Eintrag für den aktuellen Benutzer, in seiner Viewgroup für einen Tag
        /// </summary>
        /// <param name="movieId"></param>
        /// <param name="date"></param>
        /// <exception cref="SetMovieSeenTwiceForUserException">Der Film wurde für den aktuellen Benutzer für das Datum bereits gemeldet</exception>
        void SetMovieSeenByCurrentUser(int movieId, DateTime date);

        /// <summary>
        /// Erstellt einen Eintrag für einen Benutzer für heute
        /// </summary>
        /// <param name="movieId"></param>
        /// <exception cref="SetMovieSeenTwiceForUserException">Der Film wurde für den aktuellen Benutzer für das Datum bereits gemeldet</exception>
        void SetMovieSeenByCurrentUserToday(int movieId);        

        /// <summary>
        /// Erstellt einen Eintrag für einen Benutzer für gestern
        /// </summary>
        /// <param name="movieId"></param>
        /// <exception cref="SetMovieSeenTwiceForUserException">Der Film wurde für den aktuellen Benutzer für das Datum bereits gemeldet</exception>
        void SetMovieSeenByCurrentUserYesterday(int movieId);
      
        /// <summary>
        /// Erstellt einen Eintrag für einen Benutzer für vorgestern
        /// </summary>
        /// <param name="movieId"></param>
        /// <exception cref="SetMovieSeenTwiceForUserException">Der Film wurde für den aktuellen Benutzer für das Datum bereits gemeldet</exception>
        void SetMovieSeenByCurrentUserDayBeforeYesterday(int movieId);

        /// <summary>
        /// Gibt den letzten Eintrag für einen Benutzer zurück
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        homewebbridge_userseen GetLastSeenEntryForUser(string username);            
       
        /// <summary>
        /// Gibt eine Ergebnisliste aller gesehenen Videos bezogen auf die Viewgroup des akutellen Benutzers zurück.
        /// </summary>
        /// <param name="MovieId">(optional) Es kann eine bestimmte MovieId übergeben werden, wenn nur die Ergebnisse für einen bestimmten
        /// Film gewünscht werden. Defaultwert ist 0, das heisst alle Filme werden übergeben</param>
        /// <returns>List of VideoDbUserSeenViewModel</returns>
        List<VideoDbUserSeenViewModel> GetAllSeenEntriesForViewgroup(int MovieId = 0);
       
        /// <summary>
        /// Gets a set of LastSeenInformation from a list of VideoDbUserSeenViewModels
        /// </summary>
        /// <param name="SeenList"></param>
        /// <returns>A set of LastSeenInformation</returns>
        LastSeenInformation GetLastSeenInformations(List<VideoDbUserSeenViewModel> SeenList);    


        /// <summary>
        /// Prüft die Plausibilität, da ein Film für eine Viewgroup an einem Tag nur einmal als gesehen gemeldet werden kann
        /// </summary>
        /// <param name="username"></param>
        /// <param name="movieid"></param>
        /// <param name="date"></param>
        /// <returns>true, wenn der Film noch nicht eingetragen wurde --> Das Datum ist plausibel; false, wenn der Film bereits eingetragen wurde
        /// --> nicht plausibel</returns>
        bool CheckViewDatePlausibility(string viewgroup, int movieid, DateTime date);        

        /// <summary>
        /// Löscht einen Film aus der userseen-Tabelle anhand der dortigen ID (PrimareyKey)
        /// </summary>
        /// <param name="userSeenId">Die eindeutige Id auf der userseen Tabelle</param>
        void DeleteViewDateById(int userSeenId);
     


    }
}
