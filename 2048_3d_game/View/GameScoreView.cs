using UIX = Windows.UI.Xaml;
using GameModel = _2048_3d_game.Model;
namespace _2048_3d_game.View
{
    class GameScoreView
    {
        UIX.Controls.TextBlock bestScore;
        UIX.Controls.TextBlock actualScore;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bestScore">Field which shows best score</param>
        /// <param name="actualScore">Field which shows actual score</param>
        public GameScoreView(ref UIX.Controls.TextBlock bestScore, ref UIX.Controls.TextBlock actualScore)
        {
            this.bestScore = bestScore;
            this.actualScore = actualScore;
        }
        /// <summary>
        /// Method updates the value of the field with results
        /// </summary>
        /// <param name="statistic">GameScoreModel</param>
        public void UpdateStatistic(GameModel.GameScoreModel statistic)
        {
            bestScore.Text = statistic.bestScore.ToString();
            actualScore.Text = statistic.score.ToString();
        }
    }
}
