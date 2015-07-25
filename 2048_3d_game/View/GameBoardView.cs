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
        public GameBoardView(GameModel.GameBoardModel boardModel)
        {
            numberOfLayers = boardModel.numberOfLayers;

            CreateGameBoard();

            PrepareGameBoard();
            PrepareLayers(boardModel);
            UpdateGameBoard(boardModel);
        }
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
        public void UpdateGameBoard(GameModel.GameBoardModel boardModel)
        {
            for (int z = 0; z < numberOfLayers; z++)
            {
                UpdateLayer(boardModel.GetLayerAt(z), z);
            }
        }
        /////////////////////////////////////////////////////////////////////////////
        private void UpdateLayer(GameModel.FieldValue[,] layer, int z)
        {
            layers[z].UpdateLayer(layer);
        }
        /////////////////////////////////////////////////////////////////////////////
        private void CreateGameBoard()
        {
            gameBoard = new Grid();
            layers = new LayerView[numberOfLayers];
        }
        private void PrepareGameBoard()
        {
            gameBoard.Margin = new UIX.Thickness(0);
            for (int i = 0; i < numberOfLayers; i++)
            {
                gameBoard.ColumnDefinitions.Add(new ColumnDefinition());
            }
        }
        private void PrepareLayers(GameModel.GameBoardModel boardModel)
        {
            for (int z = 0; z < numberOfLayers; z++)
            {
                layers[z] = new LayerView(boardModel, z);
                layers[z].InsertLayerIntoGameBoard(ref gameBoard, z, 0);
            }
        }
    }
}
