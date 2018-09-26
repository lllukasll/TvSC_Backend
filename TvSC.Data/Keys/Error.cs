using System;
using System.Collections.Generic;
using System.Text;

namespace TvSC.Data.Keys
{
    public class Error
    {
        public const string data_Invalid = "Zły format danych";

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
    }
}
