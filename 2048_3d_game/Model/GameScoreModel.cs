using System;
using Windows.Storage;
using _2048_3d_game.Exceptions;
namespace _2048_3d_game.Model
{
    /// <summary>
    /// Class is used to store the results of the current game
    /// </summary>
    class GameScoreModel
    {
        public int score { get; set; }
        public int bestScore { get; set; }

        /// <summary>
        /// Constructor, loads best score from local storage
        /// </summary>
        public GameScoreModel()
        {
            score = 0;
            LoadBestScore();
        }

        /// <summary>
        /// Method converts string to game score model
        /// </summary>
        /// <param name="textScore"></param>
        public void ImportScoreModelFromString(String textScore)
        {
            score = Int32.Parse(textScore);
        }
        /// <summary>
        /// Method converts game score model to string
        /// </summary>
        /// <returns></returns>
        public String ExportScoreModelToString()
        {
            return score.ToString();
        }
        /// <summary>
        /// Method resets score and load best score from local storage
        /// </summary>
        public void ResetStatistic()
        {
            score = 0;
            LoadBestScore();
        }
        /// <summary>
        /// Method adds points to score
        /// </summary>
        /// <param name="points"></param>
        public void AddPoints(int points)
        {
            score += points;
            if (score >= bestScore)
            {
                bestScore = score;
                SaveBestScore();
            }
        }
        /// <summary>
        /// Method loads best score from local storage
        /// </summary>
        private void LoadBestScore()
        {
            try
            {
                bestScore = DataLoader.LoadIntFromLocalSettings(DataLoader.gameBestScore);
            }
            catch (NoDataInStorage)
            {
                bestScore = 0;
            }
        }
        /// <summary>
        /// Method saves best score to local storage
        /// </summary>
        private void SaveBestScore()
        {
            DataLoader.SaveIntToLocalSettings(DataLoader.gameBestScore, bestScore);
        }
        /// <summary>
        /// Method adds actual score to high scores list
        /// </summary>
        public void AddScoreToHighScores()
        {
            GameHighScoresModel.AddScoreToHighScores(score);
        }
    }
}
