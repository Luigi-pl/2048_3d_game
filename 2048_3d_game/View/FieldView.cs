using System;
using Windows.UI;
using UIX = Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Shapes;
using GameModel = _2048_3d_game.Model;

namespace _2048_3d_game.View
{
    /// <summary>
    /// Class is used to show single field
    /// </summary>
    class FieldView
    {
        private UIX.Shapes.Rectangle background;
        private UIX.Controls.Border backgroundBorder;
        private UIX.Controls.TextBlock content;
        private UIX.Controls.Viewbox contentBox;
        private GameModel.FieldValue fieldValue;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="value">Field value</param>
        public FieldView(GameModel.FieldValue value)
        {
            InitializeBackground();
            InitializeTextBlock();
            InitializeContentBox();

            UpdateField(value);
        }

        /// <summary>
        /// Method is used to insert single field to layer(grid of fields 3x3).
        /// </summary>
        /// <param name="layer">grid of fields</param>
        /// <param name="x">x position</param>
        /// <param name="y">y position</param>
        public void InsertFieldIntoLayer(ref UIX.Controls.Grid layer, int x, int y)
        {
            Grid.SetColumn(backgroundBorder, x);
            Grid.SetRow(backgroundBorder, y);

            Grid.SetColumn(contentBox, x);
            Grid.SetRow(contentBox, y);

            layer.Children.Add(backgroundBorder);
            layer.Children.Add(contentBox);
        }
        /// <summary>
        /// Method updates value of field
        /// </summary>
        /// <param name="value">New value</param>
        public void UpdateField(GameModel.FieldValue value)
        {
            SetFieldValue(value);
            FillBackground();
            UpdateTextBlock();
        }
        /////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Method sets field value
        /// </summary>
        /// <param name="value"></param>
        private void SetFieldValue(GameModel.FieldValue value)
        {
            this.fieldValue = value;
        }
        /// <summary>
        /// Method updates text inside text block
        /// </summary>
        private void UpdateTextBlock()
        {
            if (fieldValue == 0)
            {
                content.Text = "";
            }
            else
            {
                content.Text = ((int)fieldValue).ToString();
            }
        }
        /// <summary>
        /// Method fills field backgroud with color correlated to the value of the field
        /// </summary>
        private void FillBackground()
        {
            Color newBackgroundColor = FieldBackgroundColor.GetColorForFieldBackground(fieldValue);
            UIX.Media.SolidColorBrush brush = new UIX.Media.SolidColorBrush(newBackgroundColor);
            background.Fill = brush;
        }
        /////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Method initializes and sets background appearance 
        /// </summary>
        private void InitializeBackground()
        {
            background = new UIX.Shapes.Rectangle();
            background.Margin = new UIX.Thickness(0);

            backgroundBorder = new Border();
            Color borderColor = Colors.White;
            backgroundBorder.BorderBrush = new UIX.Media.SolidColorBrush(borderColor);
            backgroundBorder.BorderThickness = new UIX.Thickness(2);
            backgroundBorder.Child = background;
        }
        /// <summary>
        /// Method initializes and sets text block appearance 
        /// </summary>
        private void InitializeTextBlock()
        {
            content = new UIX.Controls.TextBlock();
            content.TextAlignment = UIX.TextAlignment.Center;
            content.VerticalAlignment = UIX.VerticalAlignment.Center;
            content.HorizontalAlignment = UIX.HorizontalAlignment.Center;
            content.Foreground = new SolidColorBrush(Colors.White);
        }
        /// <summary>
        /// Method initializes and sets view box appearance 
        /// </summary>
        private void InitializeContentBox()
        {
            contentBox = new UIX.Controls.Viewbox();
            contentBox.Stretch = Stretch.Uniform;
            contentBox.Height = Double.NaN;
            contentBox.Width = Double.NaN;
            contentBox.Child = content;
        }
    }
}
