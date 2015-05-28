using System;
using System.Collections.Generic;
using System.Text;

using Windows.UI;
using UIX = Windows.UI.Xaml;
using UIX.Media;
using UIX.Controls;
using UIX.Shapes;
namespace _2048_3d_game.Board
{
    class Board
    {

    }

    class Field
    {
        private UIX.Shapes.Rectangle background;
        private UIX.Controls.TextBlock content;
        private UIX.Controls.Viewbox contentBox;
        private int value;

        public Field()
        {
            background = new UIX.Shapes.Rectangle();
            content = new UIX.Controls.TextBlock();
            contentBox = new UIX.Controls.Viewbox();

            FillBackground();
            value = 0;

        }

        private void FillBackground()
        {
            Color newBackgroundColor = BackgroundColor.getColorForBackground(value);
            UIX.Media.SolidColorBrush brush = new UIX.Media.SolidColorBrush(newBackgroundColor);
            background.Fill = brush;
        }
        private void UpdateTextBlock()
        {
           //TODO
        }
    }

    static class BackgroundColor
    {
        private static Color[] colors = {   Colors.LightGray,           //empty field
                                            Colors.DarkGray,            //field value = 2
                                            Colors.Turquoise,           //field value = 4
                                            Colors.Brown,               //field value = 8
                                            Colors.Magenta,             //field value = 16
                                            Colors.YellowGreen,         //field value = 32
                                            Colors.Cyan,                //field value = 64
                                            Colors.Green,               //field value = 128
                                            Colors.Orange,              //field value = 256
                                            Colors.Lime,                //field value = 512
                                            Colors.Navy,                //field value = 1024
                                            Colors.Pink,                //field value = 2048
                                            Colors.Black };             //field value = 4096

        private static List<Color> availableBackgroundColors = new List<Color>(colors);
        
        public static Color getColorForBackground(int value)
        {
            if(value == 0)
            {
                return availableBackgroundColors[0];
            }
            else if (2 <= value && value <= 4096)
            {
                int index = (int) Math.Log(value, 2);
                return availableBackgroundColors[index];
            }
            else
            {
                return availableBackgroundColors[availableBackgroundColors.Count - 1];
            }
        }
    }
}
