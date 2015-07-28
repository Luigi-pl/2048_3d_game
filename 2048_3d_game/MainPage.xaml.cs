using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.System;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using W = Windows.Phone.UI.Input;
using Windows.UI.Input;

using GameController = _2048_3d_game.Controller;

namespace _2048_3d_game
{
    /// <summary>
    /// Main page/Start page
    /// </summary>
    public sealed partial class MainPage : Page
    {

        public MainPage()
        {
            this.InitializeComponent();
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

        private void NewGame_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(NewGamePage));
        }

        private void LoadGame_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(LoadGamePage));
        }

        private void HighScores_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(HighScoresPage));
        }

        private void About_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AboutPage));
        }
    }
}
