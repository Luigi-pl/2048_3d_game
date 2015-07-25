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
    /// An empty page that can be used on its own or navigated to within a Frame.
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
            Model.GameSettings settings = Model.GameSettings.Instance;

            this.BoardSizeSlider.Value = settings.boardSize;
            this.BoardSizeSlider.Minimum = Model.GameSettings.minBoardSize;
            this.BoardSizeSlider.Maximum = Model.GameSettings.maxBoardSize;
            

            this.BoardSizeNumber.Text = settings.boardSize.ToString();
        }
        private void LoadLayerNumberSettings()
        {
            Model.GameSettings settings = Model.GameSettings.Instance;

            this.LayersSlider.Value = settings.numberOfLayers;
            this.LayersSlider.Minimum = Model.GameSettings.minNumberofLayers;
            this.LayersSlider.Maximum = Model.GameSettings.maxNumberofLayers;

            this.LayersNumber.Text = settings.numberOfLayers.ToString();
        }
        private void LoadNewFieldNumberSettings()
        {
            Model.GameSettings settings = Model.GameSettings.Instance;
            this.NewFieldSlider.Minimum = Model.GameSettings.minNumberOfFieldsToAdd;
            SetNewFieldSliderMaximum();
        }
        private void LoadTargetValueField()
        {
            Model.GameSettings settings = Model.GameSettings.Instance;

            this.TargetValueFieldSlider.Value = settings.GetTargetFieldValue();
            this.TargetValueFieldSlider.Minimum = settings.GetMinTargetFieldValue();
            this.TargetValueFieldSlider.Maximum = settings.GetMaxTargetFieldValue();

            this.TargetValueFieldNumber.Text = settings.GetTargetFieldValueToString();
        }

        private void SetNewFieldSliderMaximum()
        {
            Model.GameSettings settings = Model.GameSettings.Instance;

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
            Model.GameSettings settings = Model.GameSettings.Instance;
            settings.boardSize = (int)e.NewValue;
            this.BoardSizeNumber.Text = settings.boardSize.ToString();

            SetNewFieldSliderMaximum();
        }

        private void LayersSlider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            Model.GameSettings settings = Model.GameSettings.Instance;
            settings.numberOfLayers = (int)e.NewValue;
            this.LayersNumber.Text = settings.numberOfLayers.ToString();

            SetNewFieldSliderMaximum();
        }

        private void NewFieldSlider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            Model.GameSettings settings = Model.GameSettings.Instance;
            settings.numberOfFieldsToAdd = (int)e.NewValue;
            this.NewFieldNumber.Text = settings.numberOfFieldsToAdd.ToString();
        }

        private void TargetValueFieldSlider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            Model.GameSettings settings = Model.GameSettings.Instance;
            settings.SetTargetFieldValue((int)e.NewValue);
            this.TargetValueFieldNumber.Text = settings.GetTargetFieldValueToString();
        }
    }
}
