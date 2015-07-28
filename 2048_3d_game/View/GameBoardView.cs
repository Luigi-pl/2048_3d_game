using UIX = Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using GameModel = _2048_3d_game.Model;
namespace _2048_3d_game.View
{
    class GameBoardView
    {
        private LayerView[] layers;
        private UIX.Controls.Grid gameBoard;
        private int numberOfLayers;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="boardModel">Board model</param>
        public GameBoardView(GameModel.GameBoardModel boardModel)
        {
            numberOfLayers = boardModel.numberOfLayers;

            InitializeGameBoard();
            CreateLayers(boardModel);
            UpdateGameBoard(boardModel);
        }
        /// <summary>
        /// Method is used to insert game board view to main grid.
        /// </summary>
        public void InsertGameBoardIntoMainGrid(ref UIX.Controls.Grid mainGrid, int x, int y)
        {
            if (mainGrid.Children.Count == 2)
            {
                mainGrid.Children.RemoveAt(1);
            }
            Grid.SetColumn(gameBoard, x);
            Grid.SetRow(gameBoard, y);

            mainGrid.Children.Add(gameBoard);
        }
        /// <summary>
        /// Method updates all layers based on given model
        /// </summary>
        /// <param name="boardModel"></param>
        public void UpdateGameBoard(GameModel.GameBoardModel boardModel)
        {
            for (int z = 0; z < numberOfLayers; z++)
            {
                UpdateLayer(boardModel.GetLayerAt(z), z);
            }
        }
        /////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Method updates field value at specified layer
        /// </summary>
        /// <param name="layer">Array with fields values</param>
        /// <param name="z">Layer number</param>
        private void UpdateLayer(GameModel.FieldValue[,] layer, int z)
        {
            layers[z].UpdateLayer(layer);
        }
        /////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Method initializes and sets game board appearance
        /// </summary>
        private void InitializeGameBoard()
        {
            gameBoard = new Grid();
            layers = new LayerView[numberOfLayers];

            gameBoard.Margin = new UIX.Thickness(0);
            for (int i = 0; i < numberOfLayers; i++)
            {
                gameBoard.ColumnDefinitions.Add(new ColumnDefinition());
            }
        }
        /// <summary>
        /// Method creates layers based on given model
        /// </summary>
        /// <param name="model">Board model</param>
        private void CreateLayers(GameModel.GameBoardModel boardModel)
        {
            for (int z = 0; z < numberOfLayers; z++)
            {
                layers[z] = new LayerView(boardModel, z);
                layers[z].InsertLayerIntoGameBoard(ref gameBoard, z, 0);
            }
        }
    }
}
