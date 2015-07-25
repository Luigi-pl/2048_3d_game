using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using _2048_3d_game.Model;
// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace _2048_3d_game
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HighScoresPage : Page
    {
        public HighScoresPage()
        {
            this.InitializeComponent();
            ChangeComponents();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Windows.Graphics.Display.DisplayInformation.AutoRotationPreferences =
                Windows.Graphics.Display.DisplayOrientations.Portrait;
        }

        private void ChangeComponents()
        {
            List<int> highScores = GameHighScoresModel.GetHighScores();
            highScores.Sort();

            ChangeTextBlock(this.HighScore_1, highScores[4].ToString());
            ChangeTextBlock(this.HighScore_2, highScores[3].ToString());
            ChangeTextBlock(this.HighScore_3, highScores[2].ToString());
            ChangeTextBlock(this.HighScore_4, highScores[1].ToString());
            ChangeTextBlock(this.HighScore_5, highScores[0].ToString());
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }
        private void ChangeTextBlock(TextBlock block, String text)
        {
            block.Text = text;
        }
    }
}
