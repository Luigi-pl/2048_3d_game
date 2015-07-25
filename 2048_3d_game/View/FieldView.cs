using System;
using Windows.UI;
using UIX = Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Shapes;

using GameModel = _2048_3d_game.Model;

namespace _2048_3d_game.View
{
    class FieldView
    {
        private UIX.Shapes.Rectangle background;
        private UIX.Controls.Border backgroundBorder;
        private UIX.Controls.TextBlock content;
        private UIX.Controls.Viewbox contentBox;
        private GameModel.FieldValue fieldValue;

        public FieldView(GameModel.FieldValue value)
        {
            CreateField();

            PrepareBackground();
            PrepareBackgroundBorder();

            PrepareTextBlock();
            PrepareContentBox();

            UpdateField(value);
        }
        public void InsertFieldIntoLayer(ref UIX.Controls.Grid layer, int x, int y)
        {
            Grid.SetColumn(backgroundBorder, x);
            Grid.SetRow(backgroundBorder, y);

            Grid.SetColumn(contentBox, x);
            Grid.SetRow(contentBox, y);

            layer.Children.Add(backgroundBorder);
            layer.Children.Add(contentBox);
        }
        public void UpdateField(GameModel.FieldValue value)
        {
            SetFieldValue(value);
            FillBackground();
            UpdateTextBlock();
        }
        /////////////////////////////////////////////////////////////////////////////
        private void SetFieldValue(GameModel.FieldValue value)
        {
            this.fieldValue = value;
        }
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
        private void FillBackground()
        {
            Color newBackgroundColor = FieldBackgroundColor.GetColorForFieldBackground(fieldValue);
            UIX.Media.SolidColorBrush brush = new UIX.Media.SolidColorBrush(newBackgroundColor);
            background.Fill = brush;
        }
        /////////////////////////////////////////////////////////////////////////////
        private void CreateField()
        {
            background = new UIX.Shapes.Rectangle();
            backgroundBorder = new Border();
            content = new UIX.Controls.TextBlock();
            contentBox = new UIX.Controls.Viewbox();
        }
        private void PrepareBackground()
        {
            background.Margin = new UIX.Thickness(0);
        }
        private void PrepareBackgroundBorder()
        {
            Color borderColor = Colors.White;
            backgroundBorder.BorderBrush = new UIX.Media.SolidColorBrush(borderColor);
            backgroundBorder.BorderThickness = new UIX.Thickness(2);
            backgroundBorder.Child = background;
        }
        private void PrepareTextBlock()
        {
            content.TextAlignment = UIX.TextAlignment.Center;
            content.VerticalAlignment = UIX.VerticalAlignment.Center;
            content.HorizontalAlignment = UIX.HorizontalAlignment.Center;
            content.Foreground = new SolidColorBrush(Colors.White);
        }
        private void PrepareContentBox()
        {
            contentBox.Stretch = Stretch.Uniform;
            contentBox.Height = Double.NaN;
            contentBox.Width = Double.NaN;
            contentBox.Child = content;
        }
    }
}
