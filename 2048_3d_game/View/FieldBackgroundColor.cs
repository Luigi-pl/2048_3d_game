using System;
using System.Collections.Generic;
using Windows.UI;
using GameModel = _2048_3d_game.Model;
namespace _2048_3d_game.View
{
    static class FieldBackgroundColor
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

        private static List<Color> availableFieldBackgroundColors = new List<Color>(colors);

        public static Color GetColorForFieldBackground(GameModel.FieldValue value)
        {
            if (value == GameModel.GameBoardModel.emptyField)
            {
                return availableFieldBackgroundColors[0];
            }
            else if (GameModel.GameBoardModel.firstFieldValue <= value && value <= GameModel.GameBoardModel.lastFieldValue)
            {
                int index = (int)Math.Log((int)value, 2);
                return availableFieldBackgroundColors[index];
            }
            else
            {
                return availableFieldBackgroundColors[availableFieldBackgroundColors.Count - 1];
            }
        }
    }
}
