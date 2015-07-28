using UIX = Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

using GameModel = _2048_3d_game.Model;
namespace _2048_3d_game.View
{
    /// <summary>
    /// Class is used to show single layer (grid 3x3 of fields)
    /// </summary>
    class LayerView
    {
        private FieldView[,] fields;
        private UIX.Controls.Grid layer;
        private int layerSize;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="boardModel">Board model</param>
        /// <param name="z">Layer's number from boardModel</param>
        public LayerView(GameModel.GameBoardModel boardModel, int z)
        {
            layerSize = boardModel.boardSize;

            InitializeLayer();
            CreateFields(boardModel.GetLayerAt(z));
        }
        /// <summary>
        /// Method is used to insert single layer to board(grid of layer 3x1).
        /// </summary>
        /// <param name="gameBoard">grid of layers</param>
        /// <param name="x">x position</param>
        /// <param name="y">y position</param>
        public void InsertLayerIntoGameBoard(ref UIX.Controls.Grid gameBoard, int x, int y)
        {
            Grid.SetColumn(layer, x);
            Grid.SetRow(layer, y);

            gameBoard.Children.Add(layer);
        }
        /// <summary>
        /// Method updates the value of the fields based on given model
        /// </summary>
        /// <param name="model"></param>
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
       /// <summary>
        /// Method updates field value at specified position
       /// </summary>
       /// <param name="value">new field value</param>
       /// <param name="x">x-position</param>
       /// <param name="y">y-position</param>
        private void UpdateField(GameModel.FieldValue value, int x, int y)
        {
            fields[x, y].UpdateField(value);
        }
        /////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Method initializes and sets layer appearance
        /// </summary>
        private void InitializeLayer()
        {
            layer = new UIX.Controls.Grid();
            fields = new FieldView[layerSize, layerSize];

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
        /// <summary>
        /// Method creates fields based on given model
        /// </summary>
        /// <param name="model">Array with fields values</param>
        private void CreateFields(GameModel.FieldValue[,] model)
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
