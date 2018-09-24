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
    }
}
