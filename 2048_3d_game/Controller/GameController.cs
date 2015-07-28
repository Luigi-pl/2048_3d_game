using System;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;
using Model = _2048_3d_game.Model;
using View = _2048_3d_game.View;
using _2048_3d_game.Exceptions;
namespace _2048_3d_game.Controller
{
    /// <summary>
    /// Class controlls gameplay
    /// </summary>
    class GameController
    {
        Model.GameScoreModel gameStatistic;
        View.GameScoreView gameStatisticView;

        Model.GameBoardModel gameModel;
        View.GameBoardView gameModelView;
        Windows.UI.Xaml.Controls.Grid mainBoardGrid;


        int boardSize;
        int numberOfLayers;

        private int begin;
        private int end;
        private int change;

        private String beforeMove = "";

        public GameController(ref Windows.UI.Xaml.Controls.Grid mainBoard,
            ref Windows.UI.Xaml.Controls.TextBlock bestScore,
            ref Windows.UI.Xaml.Controls.TextBlock actualScore)
        {
            LoadBoardSize();

            gameStatistic = new Model.GameScoreModel();
            gameStatisticView = new View.GameScoreView(ref bestScore, ref actualScore);

            mainBoardGrid = mainBoard;
            gameModel = new Model.GameBoardModel(this.boardSize, this.numberOfLayers);
            gameModelView = new View.GameBoardView(gameModel);

            gameModelView.InsertGameBoardIntoMainGrid(ref mainBoardGrid, 0, 1);

            StartGame();
        }

        /// <summary>
        /// Method starts new game
        /// </summary>
        private void StartGame()
        {
            Model.GameSettings.GetInstance.ifTheGameIsOver = false;
            
            SetValueOfRandomField();
            SetValueOfRandomField();
            UpdateView();
        }

        /// <summary>
        /// Method restarts game
        /// </summary>
        public void ResetGame()
        {

            LoadBoardSize();

            gameModel = new Model.GameBoardModel(this.boardSize, this.numberOfLayers);
            gameModelView = new View.GameBoardView(gameModel);
            gameModelView.InsertGameBoardIntoMainGrid(ref mainBoardGrid, 0, 1);

            gameStatistic.ResetStatistic();

            StartGame();
        }

        /// <summary>
        /// Method converts string (saved gamed) to game board, game settings and game score
        /// </summary>
        /// <param name="savedGame"></param>
        public void ImportGame(String savedGame)
        {
            string []list = savedGame.Split('.');

            Model.GameSettings.GetInstance.ImportSettingsFromString(list[0], list[1], list[2], list[3], list[4]);
            LoadBoardSize();

            gameModel = new Model.GameBoardModel(this.boardSize, this.numberOfLayers);
            gameModelView = new View.GameBoardView(gameModel);
            gameModelView.InsertGameBoardIntoMainGrid(ref mainBoardGrid, 0, 1);
            gameModel.ImportGameBoardModelFromString(list[5], list[6]);

            gameStatistic.ResetStatistic();
            gameStatistic.ImportScoreModelFromString(list[7]);

            UpdateView();
        }

        public void SaveScoreToHighScores()
        {
            gameStatistic.AddScoreToHighScores();
        }

        /// <summary>
        /// Method converts game board, game settings and game score to string
        /// </summary>
        /// <param name="savedGame"></param>
        public String ExportGame()
        {
            String result = Model.GameSettings.GetInstance.ExportSettingsToString();
            result += gameModel.ExportGameBoardModelToString();
            result += gameStatistic.ExportScoreModelToString();

            return result;
        }
        /// <summary>
        /// Method withdraws last movement of player
        /// </summary>
        public void WithdrawLastMovement()
        {
            if (beforeMove.Length > 0)
            {
                ImportGame(beforeMove);
            }
        }

        /// <summary>
        /// Method is called when there isn't any empty fields
        /// </summary>
        private async void EndGame_NoEmptyFields()
        {
            var dialog = new Windows.UI.Popups.MessageDialog("Game over!");
            dialog.Commands.Add(new UICommand { Label = "Restart", Id = 0 });
            var res = await dialog.ShowAsync();

            if ((int)res.Id == 0)
            {
                SaveScoreToHighScores();
                ResetGame();
            }
        }
        /// <summary>
        /// Method is called when player reaches target value of the field needed to win
        /// </summary>
        private async void EndGame_TargetValueOfTheFieldReached()
        {
            var dialog = new Windows.UI.Popups.MessageDialog("You reached target field value.\nWhat do you want to do?");
            dialog.Commands.Add(new UICommand { Label = "Restart", Id = 0 });
            dialog.Commands.Add(new UICommand { Label = "Continue game", Id = 1 });
            var res = await dialog.ShowAsync();

            if ((int)res.Id == 0)
            {
                SaveScoreToHighScores();
                ResetGame();
            }
            else if ((int)res.Id == 1)
            {
                Model.GameSettings.GetInstance.ifTheGameIsOver = true;
            }
        }

        /// <summary>
        /// Method loads from settings game board size
        /// </summary>
        private void LoadBoardSize()
        {
            Model.GameSettings settings = Model.GameSettings.GetInstance;

            this.boardSize = settings.boardSize;
            this.numberOfLayers = settings.numberOfLayers;
        }

        /// <summary>
        /// Method checks if the game is over (no empty fields, target value of the field reached)
        /// 
        /// </summary>
        /// <param name="numberOfMovements"></param>
        private void AfterMoveCheck(int numberOfMovements)
        {
            int numberOfFreeFields = Model.EmptyFieldsPosition.Instance.NumberOfEmptyFields();

            if (!Model.GameSettings.GetInstance.ifTheGameIsOver && 
                gameModel.IsTargetFieldValueReached(Model.GameSettings.GetInstance.targetValueOfTheField))
            {
                EndGame_TargetValueOfTheFieldReached();
            }

            if (numberOfMovements > 0 && numberOfFreeFields > 0)
            {
                for (int numberOfFieldsToAdd = Model.GameSettings.GetInstance.numberOfFieldsToAdd; numberOfFieldsToAdd > 0; numberOfFieldsToAdd--)
                {
                    SetValueOfRandomField();
                }
            }
            else if (numberOfMovements == 0 && numberOfFreeFields > 0)
            {

            }
            else if (numberOfMovements > 0 && numberOfFreeFields == 0)
            {
                EndGame_NoEmptyFields();
            }
            else if (numberOfMovements == 0 && numberOfFreeFields == 0)
            {
                EndGame_NoEmptyFields();
            }
            UpdateView();
        }

        /// <summary>
        /// Method sets the value of the random field
        /// </summary>
        private void SetValueOfRandomField()
        {
            try
            {
                Model.FieldPosition position = Model.EmptyFieldPositionFactory.GetRandomEmptyFieldPosition();

                gameModel.UpdateFieldToRandomValue(position, Model.FieldValue.first);
            }
            catch(NoFreeFieldException)
            {

            }
        }
        /// <summary>
        /// Method updates View to show changes after player move
        /// </summary>
        private void UpdateView()
        {
            gameModelView.UpdateGameBoard(gameModel);
            gameStatisticView.UpdateStatistic(gameStatistic);
        }

        internal void GestureInterpreter(GestureInterpreter gestureController)
        {
            beforeMove = ExportGame();
            if (gestureController.IsGestureDirectionTop())
            {
                MoveTop();
            }
            else if (gestureController.IsGestureDirectionBottom())
            {
                MoveBottom();
            }
            else if (gestureController.IsGestureDirectionLeft())
            {
                MoveLeft();
            }
            else if (gestureController.IsGestureDirectionRight())
            {
                MoveRight();
            }
            else if (gestureController.IsGestureDirectionUp())
            {
                MoveUp();
            }
            else if (gestureController.IsGestureDirectionDown())
            {
                MoveDown();
            }
        }

        private void MoveTop()
        {
            if (numberOfLayers == 1)
            {
                return;
            }
            int numberOfMovements = 0;
            Model.FieldPosition pos = new Model.FieldPosition();
            for (pos.x = 0; pos.x < gameModel.boardSize; pos.x++)
            {
                for (pos.y = 0; pos.y < gameModel.boardSize; pos.y++)
                {
                    pos.z = gameModel.boardSize - 1;

                    begin = gameModel.numberOfLayers - 1;
                    end = 0;
                    change = 1;

                    numberOfMovements += ZAxisMovement(pos);
                }
            }
            AfterMoveCheck(numberOfMovements);
        }
        private void MoveBottom()
        {
            if (numberOfLayers == 1)
            {
                return;
            }
            int numberOfMovements = 0;
            Model.FieldPosition pos = new Model.FieldPosition();
            for (pos.x = 0; pos.x < gameModel.boardSize; pos.x++)
            {
                for (pos.y = 0; pos.y < gameModel.boardSize; pos.y++)
                {
                    pos.z = 0;

                    begin = 0;
                    end = gameModel.numberOfLayers - 1;
                    change = -1;

                    numberOfMovements += ZAxisMovement(pos);
                }
            }
            AfterMoveCheck(numberOfMovements);
        }
        private void MoveLeft()
        {
            int numberOfMovements = 0;
            Model.FieldPosition pos = new Model.FieldPosition();
            for (pos.z = 0; pos.z < gameModel.numberOfLayers; pos.z++)
            {
                for (pos.y = 0; pos.y < gameModel.boardSize; pos.y++)
                {
                    pos.x = 0;

                    begin = gameModel.boardSize - 1;
                    end = 0;
                    change = 1;

                    numberOfMovements += XAxisMovement(pos);
                }
            }
            AfterMoveCheck(numberOfMovements);
        }
        private void MoveRight()
        {
            int numberOfMovements = 0;
            Model.FieldPosition pos = new Model.FieldPosition();
            for (pos.z = 0; pos.z < gameModel.numberOfLayers; pos.z++)
            {
                for (pos.y = 0; pos.y < gameModel.boardSize; pos.y++)
                {
                    pos.x = 0;

                    begin = 0;
                    end = gameModel.boardSize - 1;
                    change = -1;

                    numberOfMovements += XAxisMovement(pos);
                }
            }
            AfterMoveCheck(numberOfMovements);
        }
        private void MoveUp()
        {
            int numberOfMovements = 0;
            Model.FieldPosition pos = new Model.FieldPosition();
            for (pos.z = 0; pos.z < gameModel.numberOfLayers; pos.z++)
            {
                for (pos.x = 0; pos.x < gameModel.boardSize; pos.x++)
                {
                    pos.y = 0;

                    begin = gameModel.boardSize - 1;
                    end = 0;
                    change = 1;

                    numberOfMovements += YAxisMovement(pos);
                }
            }
            AfterMoveCheck(numberOfMovements);
        }
        private void MoveDown()
        {
            int numberOfMovements = 0;
            Model.FieldPosition pos = new Model.FieldPosition();
            for (pos.z = 0; pos.z < gameModel.numberOfLayers; pos.z++)
            {
                for (pos.x = 0; pos.x < gameModel.boardSize; pos.x++)
                {
                    pos.y = 0;

                    begin = 0;
                    end = gameModel.boardSize - 1;
                    change = -1;

                    numberOfMovements += YAxisMovement(pos);
                }
            }
            AfterMoveCheck(numberOfMovements);
        }

        private int YAxisMovement(Model.FieldPosition position)
        {
            int numberOfMovements = 0;
            Model.FieldPosition newPosition = new Model.FieldPosition();
            newPosition.SetFieldPosition(position);

            numberOfMovements += MoveFields(position, newPosition, "y");
            newPosition.SetFieldPosition(position);

            numberOfMovements += AddFields(position, newPosition, "y");
            newPosition.SetFieldPosition(position);

            numberOfMovements += MoveFields(position, newPosition, "y");
            newPosition.SetFieldPosition(position);

            return numberOfMovements;
        }
        private int XAxisMovement(Model.FieldPosition position)
        {
            int numberOfMovements = 0;
            Model.FieldPosition newPosition = new Model.FieldPosition();
            newPosition.SetFieldPosition(position);

            numberOfMovements += MoveFields(position, newPosition, "x");
            newPosition.SetFieldPosition(position);

            numberOfMovements += AddFields(position, newPosition, "x");
            newPosition.SetFieldPosition(position);

            numberOfMovements += MoveFields(position, newPosition, "x");
            newPosition.SetFieldPosition(position);

            return numberOfMovements;
        }
        private int ZAxisMovement(Model.FieldPosition position)
        {
            int numberOfMovements = 0;
            Model.FieldPosition newPosition = new Model.FieldPosition();
            newPosition.SetFieldPosition(position);

            numberOfMovements += MoveFields(position, newPosition, "z");
            newPosition.SetFieldPosition(position);

            numberOfMovements += AddFields(position, newPosition, "z");
            newPosition.SetFieldPosition(position);

            numberOfMovements += MoveFields(position, newPosition, "z");
            newPosition.SetFieldPosition(position);

            return numberOfMovements;
        }


        private int MoveFields(Model.FieldPosition position, Model.FieldPosition newPosition, String axis)
        {
            int numberOfMovements = 0;
            int valueA;
            int valueB;
            for (valueA = end + change; AXORB(valueA, begin); valueA += change)
            {
                ChangePosition(ref position, valueA, axis);

                if (gameModel.IsFieldEmpty(position))
                {
                    continue;
                }

                for (valueB = valueA - change; AXORB(valueB, end); valueB -= change)
                {
                    ChangePosition(ref newPosition, valueB, axis);

                    if (gameModel.IsFieldNonEmpty(newPosition))
                    {
                        valueB += change;
                        ChangePosition(ref newPosition, valueB, axis);
                        break;
                    }

                    if (valueB == end)
                    {
                        break;
                    }
                }

                if (!position.Equals(newPosition))
                {
                    MoveFieldToEmptyField(position, newPosition);
                    numberOfMovements += 1;
                }
            }
            return numberOfMovements;
        }
        private int AddFields(Model.FieldPosition position, Model.FieldPosition newPosition, string axis)
        {
            int numberOfMovements = 0;
            int valueA;
            for (valueA = end + change; AXORB(valueA, begin); valueA += change)
            {
                ChangePosition(ref position, valueA, axis);
                ChangePosition(ref newPosition, valueA - change, axis);

                if (!gameModel.HaveFieldsSameValue(position, newPosition))
                {
                    continue;
                }
                else if (gameModel.IsFieldEmpty(position) || gameModel.IsFieldEmpty(newPosition))
                {
                    continue;
                }
                else if (gameModel.HaveFieldsSameValue(position, newPosition))
                {
                    if (change < 0)
                    {
                        MoveFieldToNonEmptyField(newPosition, position);
                    }
                    else
                    {
                        MoveFieldToNonEmptyField(position, newPosition);
                    }
                }
            }
            return numberOfMovements;
        }

        private void MoveFieldToEmptyField(Model.FieldPosition from, Model.FieldPosition to)
        {
            gameModel.MoveFieldToEmptyField(from, to);
        }
        private void MoveFieldToNonEmptyField(Model.FieldPosition from, Model.FieldPosition to)
        {
            AddPoints(to);

            gameModel.MoveFieldToNonEmptyField(from, to);
        }

        private void ChangePosition(ref Model.FieldPosition position, int value, String axis)
        {
            if (axis.Equals("x"))
            {
                position.x = value;
            }
            else if (axis.Equals("y"))
            {
                position.y = value;
            }
            else if (axis.Equals("z"))
            {
                position.z = value;
            }
        }

        private void AddPoints(Model.FieldPosition position)
        {
            gameStatistic.AddPoints((int)gameModel.GetValue(position) * 2);
        }

        private bool AXORB(int argA, int argB)
        {
            if ((argB == (int)Model.GameBoardModel.emptyField && argA >= argB) ^ (argB > (int)Model.GameBoardModel.emptyField && argA <= argB))
            {
                return true;
            }
            return false;
        }
    }
}
