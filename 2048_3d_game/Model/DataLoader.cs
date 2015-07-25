using System;
using Windows.Storage;
using _2048_3d_game.Exceptions;
namespace _2048_3d_game.Model
{
    class DataLoader
    {
        public const String gameBestScore = "2048_bestScore";
        public const String gameHighScores = "2048_highScores";

        public const String saveGame_Name_First  = "SaveGame_Name_First";
        public const String saveGame_Value_First = "SaveGame_Value_First";

        public const String saveGame_Name_Second  = "SaveGame_Name_Second";
        public const String saveGame_Value_Second = "SaveGame_Value_Second";

        public const String saveGame_Name_Third  = "SaveGame_Name_Third";
        public const String saveGame_Value_Third = "SaveGame_Value_Third";

        public const String saveGame_Termineted = "SaveGame_Tombstoned";

        public static int LoadIntFromLocalSettings(String name)
        {
            if (Windows.Storage.ApplicationData.Current.LocalSettings.Values[name] == null)
            {
                throw new NoDataInStorage("No data in storage");
            }
            else
            {
                return (int)Windows.Storage.ApplicationData.Current.LocalSettings.Values[name];
            }
        }
        public static void SaveIntToLocalSettings(String name, int value)
        {
            Windows.Storage.ApplicationData.Current.LocalSettings.Values[name] = value;
        }

        public static String LoadStringFromLocalSettings(String name)
        {
            if (Windows.Storage.ApplicationData.Current.LocalSettings.Values[name] == null)
            {
                throw new NoDataInStorage("No data in storage");
            }
            else
            {
                return (String)Windows.Storage.ApplicationData.Current.LocalSettings.Values[name];
            }
        }
        public static void SaveStringToLocalSettings(String name, String value)
        {
            Windows.Storage.ApplicationData.Current.LocalSettings.Values[name] = value;
        }
    }
}
