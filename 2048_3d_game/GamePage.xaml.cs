using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.System;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Phone.UI.Input;
using Windows.UI.Input;
using Model = _2048_3d_game.Model;
using Controller = _2048_3d_game.Controller;

namespace _2048_3d_game
{
    public sealed partial class GamePage : Page
    {
        GestureRecognizer gestureRecognizer = new Windows.UI.Input.GestureRecognizer();
        Windows.UI.Xaml.UIElement element;

        Controller.GameController mainController;
        Controller.GestureController gestureController;

        public GamePage()
        {
            this.InitializeComponent();

            mainController = new Controller.GameController(ref this.MainBoard, ref this.BestScoreNumber, ref this.ActualScoreNumber);
            gestureController = new Controller.GestureController();

            this.NavigationCacheMode = NavigationCacheMode.Required;
            GestureInputProcessor(gestureRecognizer, this.MainBoard);
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Windows.Graphics.Display.DisplayInformation.AutoRotationPreferences = 
                Windows.Graphics.Display.DisplayOrientations.Landscape;

            
            HardwareButtons.BackPressed += HardwareButtons_BackPressed;

            var lastPage = Frame.BackStack.LastOrDefault();
            if (lastPage != null && lastPage.SourcePageType.Equals(typeof(NewGamePage)))
            {
                mainController.ResetGame();
            }
            else if (lastPage != null && lastPage.SourcePageType.Equals(typeof(LoadGamePage)))
            {
                mainController.ImportGame((String)e.Parameter);
            }
            else if (lastPage == null)
            {
                mainController.ImportGame((String)e.Parameter);
            }
        }
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            gestureRecognizer.ManipulationCompleted -= gestureRecognizer_ManipulationCompleted;
            gestureRecognizer.ManipulationStarted -= gestureRecognizer_ManipulationStarted;
            HardwareButtons.BackPressed -= HardwareButtons_BackPressed;

            base.OnNavigatedFrom(e);
        }

        void HardwareButtons_BackPressed(object sender, BackPressedEventArgs e)
        {
            mainController.UndoMovement();
            e.Handled = true;
        }

        public void GestureInputProcessor(Windows.UI.Input.GestureRecognizer gr, Windows.UI.Xaml.UIElement target)
        {
            this.gestureRecognizer = gr;
            //Targeted Ui element to be performing gestures on it.   
            this.element = target;

            //Enable gesture settings for Tap,Hold,RightTap,CrossSlide   
            this.gestureRecognizer.GestureSettings =
                 Windows.UI.Input.GestureSettings.ManipulationTranslateX |
                 Windows.UI.Input.GestureSettings.ManipulationTranslateY;

            // Set up pointer event handlers. These receive input events that are used by the gesture recognizer.   
            this.element.PointerCanceled += OnPointerCanceled;
            this.element.PointerPressed += OnPointerPressed;
            this.element.PointerReleased += OnPointerReleased;
            this.element.PointerMoved += OnPointerMoved;

            gestureRecognizer.ManipulationCompleted += gestureRecognizer_ManipulationCompleted;
            gestureRecognizer.ManipulationStarted += gestureRecognizer_ManipulationStarted;

        }
        private void gestureRecognizer_ManipulationStarted(GestureRecognizer sender, ManipulationStartedEventArgs args)
        {
            gestureController.SetStartPoint(args.Position);
        }
        private void gestureRecognizer_ManipulationCompleted(GestureRecognizer sender, ManipulationCompletedEventArgs args)
        {
            gestureController.SetEndPoint(args.Position);
            gestureController.Calculate();
            mainController.GestureInterpreter(gestureController);
            gestureController.PointersReleased();
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            GestureInputProcessor(gestureRecognizer, this.MainBoard);
            HardwareButtons.BackPressed += HardwareButtons_BackPressed;
        }
        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            gestureRecognizer.ManipulationCompleted -= gestureRecognizer_ManipulationCompleted;
            gestureRecognizer.ManipulationStarted -= gestureRecognizer_ManipulationStarted;
            HardwareButtons.BackPressed -= HardwareButtons_BackPressed;
        }
        void OnPointerPressed(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs args)
        {
            gestureController.PointerPressed();

            this.gestureRecognizer.ProcessDownEvent(args.GetCurrentPoint(this.element));
            this.element.CapturePointer(args.Pointer);
            args.Handled = true;
        }
        void OnPointerReleased(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs args)
        {
            this.gestureRecognizer.ProcessUpEvent(args.GetCurrentPoint(this.element));
            args.Handled = true;
        }
        void OnPointerCanceled(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs args)
        {
            this.gestureRecognizer.CompleteGesture();
            args.Handled = true;
        }
        
        void OnPointerMoved(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs args)
        {
            this.gestureRecognizer.ProcessMoveEvents(args.GetIntermediatePoints(this.element));
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            mainController.SaveScoreToHighScores();
            mainController.ResetGame();
        }

        private void SaveGameButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(SaveGamePage), mainController.ExportGame());
        }
        
        public void SaveGame_Suspended()
        {
            Model.DataLoader.SaveStringToLocalSettings(Model.DataLoader.saveGame_Termineted, mainController.ExportGame());
        }

        private void MenuButton_Click(object sender, RoutedEventArgs e)
        {
            mainController.SaveScoreToHighScores();
            Frame.Navigate(typeof(MainPage));
        }
    }
}
