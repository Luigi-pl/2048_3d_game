using System;
using Windows.Storage;
using _2048_3d_game.Exceptions;
namespace _2048_3d_game.Model
{
    class GameScoreModel
    {
        public int score { get; set; }
        public int bestScore { get; set; }

        
        public GameScoreModel()
        {
            score = 0;
            LoadBestScore();
        }

        public void ImportScoreModelFromString(String textScore)
        {
            score = Int32.Parse(textScore);
        }
        public String ExportScoreModelToString()
        {
            return score.ToString();
        }

        public void ResetStatistic()
        {
            score = 0;
            LoadBestScore();
        }

        public void AddPoints(int points)
        {
            score += points;
            if (score >= bestScore)
            {
                bestScore = score;
                SaveBestScore();
            }
        }
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
        private void SaveBestScore()
        {
            DataLoader.SaveIntToLocalSettings(DataLoader.gameBestScore, bestScore);
        }

        public void AddScoreToHighScores()
        {
            GameHighScoresModel.AddScoreToHighScores(score);
        }
    }
}
