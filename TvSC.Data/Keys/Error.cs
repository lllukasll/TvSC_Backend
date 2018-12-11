using System;
using System.Collections.Generic;
using System.Text;

namespace TvSC.Data.Keys
{
    public class Error
    {
        public const string data_Invalid = "Zły format danych";

        public const string account_Login = "Musisz się zalogować";
        public const string account_UserExists = "Użytkownik o takiej nazwie już istnineje.";
        public const string account_EmailExists = "Użytkownik o takim e-mail'u już istnieje.";
        public const string account_WrongCredentials = "Błędny login lub hasło.";
        public const string account_UserNotFound = "Nie znaleziona użytkownika w bazie danych.";

        public const string tvShow_Name_Required = "Nazwa jest wymagana";
        public const string tvShow_Adding = "Wystąpił problem podczas dodawania serialu";
        public const string tvShow_Exists = "Serial o podanej nazwie już istnieje";
        public const string tvShow_NotFound = "Serial o podanym Id nie istnieje";
        public const string tvShow_Updating = "Wystąpił problem podczas edycji serialu";
        public const string tvShow_Deleting = "Wystąpił problem podczas usuwania serialu";

        public const string season_Exists = "Sezon o podanym numerze już istnieje";
        public const string season_Adding = "Wystapił problem podczas dodawania sezonu";
        public const string season_NotFound = "Sezon o podanym id nie istnieje";
        public const string season_Updating = "Wystąpił problem podczas edycji sezonu";
        public const string season_Deleting = "Wystąpił problem podczas usuwania sezonu";

        public const string episode_Exists = "Odcinek o podanym numerze już istnieje";
        public const string episode_Adding = "Wystąpił problem podczas dodawania odcinka";
        public const string episode_NotFound = "Odcinek o podanym Id nie istnieje";
        public const string episode_Updating = "Wystąpił problem podczas edycji odcinka";
        public const string episode_Deleting = "Wystąpił problem podczas usuwania odcinka";

        public const string calendar_Wrong_Month = "Numer miesiąca musi być między 1 i 12";

        public const string rating_Adding = "Wystąpił problem podczas dodawania oceny";
        public const string rating_Already_Added = "Już dodałeś ocenę dla tego serialu";
        public const string rating_NotFound = "Ocena o podanym id nie istnieje";
        public const string rating_User_Not_Assigned = "Zalogowany użytkownik nie jest autorem oceny o podanym id";
        public const string rating_Updating = "Wystąpił problem podczas edytowania oceny";
        public const string rating_Deleting = "Wystąpił problem podczas usuwania oceny";
        public const string rating_Not_Added = "Użytkownik nie dodał jeszcze oceny";

        public const string favouriteTvShow_Adding = "Wystąpił problem podczas dodawania serialu do ulubionych";
        public const string favouriteTvShow_Already_Exists = "Serial o podanym id został już dodany do ulubionych";
        public const string favouriteTvShow_Deleting = "Wystąpił problem podczas usuwania serialu z ulubionych";

        public const string watchedTvShow_Adding = "Wystąpił problem podczas dodawanie odcinka do objerzanych";
        public const string watchedEpisode_Already_Exists = "Odcinek o podanym id został już dodany do obejrzanych";
        public const string watchedEpisode_Deleting = "Wystąpił problem podczas usuwania odcinka z objerzanych";

        public const string actor_Adding = "Wystąpił problem podczas dodawania aktora";
        public const string actor_NotExists = "Aktor o podanym id nie istnieje";
        public const string actor_Deleting = "Wystąpił problem podczas usuwania aktora";
        public const string actor_Assignment_Exists = "Aktor o podanym id jest już przypisany do tego serialu";
        public const string actor_Assignment_NotExists = "Aktor o podanym id nie jest przypisany do tego serialu";

        public const string assignment_Adding = "Wystąpił problem podczas przypisywania aktora do serialu";
        public const string assingment_Not_Exists = "Przypisanie o podanym id nie istnieje";
        public const string assignment_Deleting = "Wystąpił problem podczas usuwania przypisania";

        public const string category_Already_Exists = "Kategoria o podanej nazwie już istnieje";
        public const string category_Adding = "Wystąpił problem podczas dodawania kategorii";
        public const string category_NotFound = "Kategoria o podanym id nie istnieje";

        public const string categoryAssignment_Already_Exists = "Dana kategoria została już przypisana do tego serialu";
        public const string categoryAssignment_Adding = "Wystąpił problem podczas przypisywania kategorii do serialu";
        public const string categoryAssignment_NotFound = "Podane przypisanie kategorii do serialu nie istnieje";
        public const string categoryAssignment_Deleting = "Wystąpił problem podczas usuwania przypisania kategorii";

        public const string comment_Adding = "Wystąpił problem podczas dodawania komentarza";
        public const string comment_Deleting = "Wystąpił problem podczas usuwania komentarza";
        public const string comment_Author = "Nie jesteś autorem tego komentarza";
        public const string comment_Updating = "Wystąpił problem podczas aktualizowania komentarza";

        public const string notification_Adding = "Wystąpił problem podczas dodawania notyfikacji";
        public const string notification_NotFound = "Podana notyfikacja nie została znaleziona w bazie danych";
        public const string notification_Deleting = "Wystąpił problem podczas usuwania notyfikacji";
    }
}
