using UIX = Windows.UI.Xaml;
using GameModel = _2048_3d_game.Model;
namespace _2048_3d_game.View
{
    class GameScoreView
    {
        UIX.Controls.TextBlock bestScore;
        UIX.Controls.TextBlock actualScore;

        public GameScoreView(ref UIX.Controls.TextBlock bestScore, ref UIX.Controls.TextBlock actualScore)
        {
            this.bestScore = bestScore;
            this.actualScore = actualScore;
        }

        public void UpdateStatistic(GameModel.GameScoreModel statistic)
        {
            bestScore.Text = statistic.bestScore.ToString();
            actualScore.Text = statistic.score.ToString();
        }
    }
}
