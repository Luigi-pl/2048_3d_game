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
    /// Save game page.
    /// </summary>
    public sealed partial class SaveGamePage : Page
    {
        String gameToSave;

        public SaveGamePage()
        {
            this.InitializeComponent();
            PrepareSaveGameButtons();
            gameToSave = "";
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
            gameToSave = (String) e.Parameter;
        }


        private void PrepareSaveGameButtons()
        {
            PrepareSaveGameButton(this.SaveGame1, DataLoader.saveGame_Name_First);
            PrepareSaveGameButton(this.SaveGame2, DataLoader.saveGame_Name_Second);
            PrepareSaveGameButton(this.SaveGame3, DataLoader.saveGame_Name_Third);
        }

        private void PrepareSaveGameButton(Windows.UI.Xaml.Controls.Button saveGameButton, String dataSymbol)
        {
            try
            {
                String content = "";
                content = DataLoader.LoadStringFromLocalSettings(dataSymbol);
                if (content.Count() == 0)
                {
                    saveGameButton.Content = "Empty";
                }
                else
                {
                    saveGameButton.Content = content;
                }
            }
            catch (NoDataInStorage)
            {
                DataLoader.SaveStringToLocalSettings(dataSymbol, "");
                saveGameButton.Content = "Empty";
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(GamePage));
        }

        private void SaveGame1_Click(object sender, RoutedEventArgs e)
        {
            SaveGameButtonName_Click(DataLoader.saveGame_Name_First);
            SaveGameButtonValue_Click(DataLoader.saveGame_Value_First);
            PrepareSaveGameButton(this.SaveGame1, DataLoader.saveGame_Name_First);
            Frame.Navigate(typeof(GamePage));
        }

        private void SaveGame2_Click(object sender, RoutedEventArgs e)
        {
            SaveGameButtonName_Click(DataLoader.saveGame_Name_Second);
            SaveGameButtonValue_Click(DataLoader.saveGame_Value_Second);
            PrepareSaveGameButton(this.SaveGame2, DataLoader.saveGame_Name_Second);
            Frame.Navigate(typeof(GamePage));
        }

        private void SaveGame3_Click(object sender, RoutedEventArgs e)
        {
            SaveGameButtonName_Click(DataLoader.saveGame_Name_Third);
            SaveGameButtonValue_Click(DataLoader.saveGame_Value_Third);
            PrepareSaveGameButton(this.SaveGame3, DataLoader.saveGame_Name_Third);
            Frame.Navigate(typeof(GamePage));
        }

        private void SaveGameButtonName_Click(String dataSymbol)
        {
            String name = "Saved";
            DataLoader.SaveStringToLocalSettings(dataSymbol, name);
        }
        private void SaveGameButtonValue_Click(String dataSymbol)
        {
            DataLoader.SaveStringToLocalSettings(dataSymbol, gameToSave);
        }
        public void SaveGame_Suspended()
        {
            Model.DataLoader.SaveStringToLocalSettings(Model.DataLoader.saveGame_Termineted, gameToSave);
        }
    }
}
