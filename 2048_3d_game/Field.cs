using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Shapes.Rectangle;
using Windows.UI.Xaml.Controls.Viewbox;
using Windows.UI.Xaml.Controls.TextBlock;
using Windows.UI.Xaml.Controls.Grid;
using UI = Windows.UI.Xaml;
namespace _2048_3d_game
{
    class Field
    {
        private UI.Shapes.Rectangle background;
        private UI.Controls.Viewbox contentBox;
        private UI.Controls.TextBlock content;

        Field()
        {
            background = new UI.Shapes.Rectangle();
            background.Margin = new UI.Thickness(0);

            contentBox = new UI.Controls.Viewbox();
            content = new UI.Controls.TextBlock();
        }
    }
}
