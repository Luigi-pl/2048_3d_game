using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2048_3d_game.Exceptions;
namespace _2048_3d_game.Model
{
    class GameHighScoresModel
    {
        public static void AddScoreToHighScores(int score)
        {
            String highScores = "";
            try
            {
                highScores = DataLoader.LoadStringFromLocalSettings(DataLoader.gameHighScores);
            }
            catch(NoDataInStorage)
            {
                highScores = "0;0;0;0;0";
            }

            List<int> listOfHighScores = highScores.Split(';').Select(Int32.Parse).ToList();

            if (score > listOfHighScores.Min())
            {
                listOfHighScores.RemoveAt(listOfHighScores.IndexOf(listOfHighScores.Min()));
                listOfHighScores.Add(score);
                listOfHighScores.Sort();
            }
            highScores = string.Join(";", listOfHighScores);
            if (highScores.Where(x => x == ';').Count() > 4)
            {
                highScores.Remove(highScores.Length - 1);
            }

            DataLoader.SaveStringToLocalSettings(DataLoader.gameHighScores, highScores);
        }
        public static List<int> GetHighScores()
        {
            String highScores = "";
            try
            {
                highScores = DataLoader.LoadStringFromLocalSettings(DataLoader.gameHighScores);
            }
            catch (NoDataInStorage)
            {
                highScores = "0;0;0;0;0";
            }
            return highScores.Split(';').Select(Int32.Parse).ToList();
        }
    }
}
