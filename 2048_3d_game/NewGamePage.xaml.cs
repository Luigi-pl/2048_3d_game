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

using Controller = _2048_3d_game.Controller;

namespace _2048_3d_game
{
    /// <summary>
    /// New game page.
    /// </summary>
    public sealed partial class NewGamePage : Page
    {
        public NewGamePage()
        {
            this.InitializeComponent();

            LoadBoardSizeSettings();
            LoadLayerNumberSettings();
            LoadNewFieldNumberSettings();
            LoadTargetValueField();
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

        private void LoadBoardSizeSettings()
        {
            Model.GameSettings settings = Model.GameSettings.GetInstance;

            this.BoardSizeSlider.Value = settings.boardSize;
            this.BoardSizeSlider.Minimum = Model.GameSettings.minBoardSize;
            this.BoardSizeSlider.Maximum = Model.GameSettings.maxBoardSize;
            
            this.BoardSizeNumber.Text = settings.boardSize.ToString();
        }
        private void LoadLayerNumberSettings()
        {
            Model.GameSettings settings = Model.GameSettings.GetInstance;

            this.LayersSlider.Value = settings.numberOfLayers;
            this.LayersSlider.Minimum = Model.GameSettings.minNumberofLayers;
            this.LayersSlider.Maximum = Model.GameSettings.maxNumberofLayers;

            this.LayersNumber.Text = settings.numberOfLayers.ToString();
        }
        private void LoadNewFieldNumberSettings()
        {
            Model.GameSettings settings = Model.GameSettings.GetInstance;
            this.NewFieldSlider.Minimum = Model.GameSettings.minNumberOfFieldsToAdd;
            SetNewFieldSliderMaximum();
        }
        private void LoadTargetValueField()
        {
            Model.GameSettings settings = Model.GameSettings.GetInstance;

            this.TargetValueFieldSlider.Value = settings.GetTargetValueOfTheField();
            this.TargetValueFieldSlider.Minimum = settings.GetMinTargetValueOfTheField();
            this.TargetValueFieldSlider.Maximum = settings.GetMaxTargetValueOfTheField();

            this.TargetValueFieldNumber.Text = settings.GetTargetValueOfTheFieldString();
        }

        private void SetNewFieldSliderMaximum()
        {
            Model.GameSettings settings = Model.GameSettings.GetInstance;

            int maximum = settings.numberOfLayers;
            if (settings.boardSize > ((Model.GameSettings.minBoardSize + Model.GameSettings.maxBoardSize) / 2))
            {
                maximum += 1;
            }
            int value = (int) this.NewFieldSlider.Value;
            this.NewFieldSlider.Maximum = maximum;
            this.NewFieldSlider.Value = value;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(GamePage));
        }

        private void BoardSizeSlider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            Model.GameSettings settings = Model.GameSettings.GetInstance;
            settings.boardSize = (int)e.NewValue;
            this.BoardSizeNumber.Text = settings.boardSize.ToString();

            SetNewFieldSliderMaximum();
        }

        private void LayersSlider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            Model.GameSettings settings = Model.GameSettings.GetInstance;
            settings.numberOfLayers = (int)e.NewValue;
            this.LayersNumber.Text = settings.numberOfLayers.ToString();

            SetNewFieldSliderMaximum();
        }

        private void NewFieldSlider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            Model.GameSettings settings = Model.GameSettings.GetInstance;
            settings.numberOfFieldsToAdd = (int)e.NewValue;
            this.NewFieldNumber.Text = settings.numberOfFieldsToAdd.ToString();
        }

        private void TargetValueFieldSlider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            Model.GameSettings settings = Model.GameSettings.GetInstance;
            settings.SetTargetValueOfTheField((int)e.NewValue);
            this.TargetValueFieldNumber.Text = settings.GetTargetValueOfTheFieldString();
        }
    }
}
