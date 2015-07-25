using UIX = Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

using GameModel = _2048_3d_game.Model;
namespace _2048_3d_game.View
{
    class LayerView
    {
        private FieldView[,] fields;
        private UIX.Controls.Grid layer;
        private int layerSize;

        public LayerView(GameModel.GameBoardModel boardModel, int z)
        {
            layerSize = boardModel.boardSize;

            CreateLayer();

            PrepareBoard();
            PrepareFields(boardModel.GetLayerAt(z));
        }
        public void InsertLayerIntoGameBoard(ref UIX.Controls.Grid gameBoard, int x, int y)
        {
            Grid.SetColumn(layer, x);
            Grid.SetRow(layer, y);

            gameBoard.Children.Add(layer);
        }
        public void UpdateLayer(GameModel.FieldValue[,] model)
        {
            for (int x = 0; x < layerSize; x++)
            {
                for (int y = 0; y < layerSize; y++)
                {
                    UpdateField(model[x, y], x, y);
                }
            }
        }
        /////////////////////////////////////////////////////////////////////////////
        private void UpdateField(GameModel.FieldValue value, int x, int y)
        {
            fields[x, y].UpdateField(value);
        }
        /////////////////////////////////////////////////////////////////////////////
        private void CreateLayer()
        {
            layer = new UIX.Controls.Grid();
            fields = new FieldView[layerSize, layerSize];
        }
        private void PrepareBoard()
        {
            layer.Margin = new UIX.Thickness(5);
            for (int i = 0; i < layerSize; i++)
            {
                layer.ColumnDefinitions.Add(new ColumnDefinition());
            }
            for (int i = 0; i < layerSize; i++)
            {
                layer.RowDefinitions.Add(new RowDefinition());
            }
        }
        private void PrepareFields(GameModel.FieldValue[,] model)
        {
            for (int x = 0; x < layerSize; x++)
            {
                for (int y = 0; y < layerSize; y++)
                {
                    fields[x, y] = new FieldView(model[x, y]);
                    fields[x, y].InsertFieldIntoLayer(ref layer, x, y);
                }
            }
        }
    }
}
