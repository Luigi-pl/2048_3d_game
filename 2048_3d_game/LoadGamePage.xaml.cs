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
using _2048_3d_game.Exceptions;

namespace _2048_3d_game
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoadGamePage : Page
    {
        public LoadGamePage()
        {
            this.InitializeComponent();
            PrepareLoadGameButtons();
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

        private void PrepareLoadGameButtons()
        {
            PrepareLoadGameButton(this.LoadGame1, DataLoader.saveGame_Name_First);
            PrepareLoadGameButton(this.LoadGame2, DataLoader.saveGame_Name_Second);
            PrepareLoadGameButton(this.LoadGame3, DataLoader.saveGame_Name_Third);
        }

        private void PrepareLoadGameButton(Windows.UI.Xaml.Controls.Button loadGameButton, String dataSymbol)
        {
            try
            {
                String content = "";
                content = DataLoader.LoadStringFromLocalSettings(dataSymbol);
                if(content.Count() == 0)
                {
                    loadGameButton.Content = "Empty";
                }
                else
                {
                    loadGameButton.Content = content;
                }
            }
            catch(NoDataInStorage)
            {
                DataLoader.SaveStringToLocalSettings(dataSymbol, "");
                loadGameButton.Content = "Empty";
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

        private void LoadGame1_Click(object sender, RoutedEventArgs e)
        {
            LoadGameButton_Click(DataLoader.saveGame_Value_First);
        }

        private void LoadGame2_Click(object sender, RoutedEventArgs e)
        {
            LoadGameButton_Click(DataLoader.saveGame_Value_Second);
        }

        private void LoadGame3_Click(object sender, RoutedEventArgs e)
        {
            LoadGameButton_Click(DataLoader.saveGame_Value_Third);
        }

        private void LoadGameButton_Click(String dataSymbol)
        {
            try
            {
                String loadedGame = DataLoader.LoadStringFromLocalSettings(dataSymbol);
                Frame.Navigate(typeof(GamePage), loadedGame);
            }
            catch (NoDataInStorage)
            {

            }
        }
    }
}
