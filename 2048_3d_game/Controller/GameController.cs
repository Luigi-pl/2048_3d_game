using System;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;
using Model = _2048_3d_game.Model;
using View = _2048_3d_game.View;
using _2048_3d_game.Exceptions;
namespace _2048_3d_game.Controller
{
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

        private void StartGame()
        {
            Model.GameSettings.Instance.continuousGame = false;
            Update();
        }

        public void ResetGame()
        {

            LoadBoardSize();

            gameModel = new Model.GameBoardModel(this.boardSize, this.numberOfLayers);
            gameModelView = new View.GameBoardView(gameModel);
            gameModelView.InsertGameBoardIntoMainGrid(ref mainBoardGrid, 0, 1);

            gameStatistic.ResetStatistic();

            StartGame();
        }

        public void ImportGame(String savedGame)
        {
            string []list = savedGame.Split('.');

            Model.GameSettings.Instance.ImportSettingsFromString(list[0], list[1], list[2], list[3], list[4]);
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

        public String ExportGame()
        {
            String result = Model.GameSettings.Instance.ExportSettingsToString();
            result += gameModel.ExportGameBoardModelToString();
            result += gameStatistic.ExportScoreModelToString();

            return result;
        }
        public void UndoMovement()
        {
            if (beforeMove.Length > 0)
            {
                ImportGame(beforeMove);
            }
        }
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

        private async void EndGame_TargetFieldValueReached()
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
                Model.GameSettings.Instance.continuousGame = true;
            }
        }

        private void LoadBoardSize()
        {
            Model.GameSettings settings = Model.GameSettings.Instance;

            this.boardSize = settings.boardSize;
            this.numberOfLayers = settings.numberOfLayers;
        }

        private void Update()
        {
            UpdateModel();
            UpdateModel();
            UpdateView();
        }

        private void Update(int numberOfMovements)
        {
            int numberOfFreeFields = Model.EmptyFieldsPosition.Instance.NumberOfEmptyFields();

            if (!Model.GameSettings.Instance.continuousGame && 
                gameModel.IsTargetFieldValueReached(Model.GameSettings.Instance.targetFieldValue))
            {
                EndGame_TargetFieldValueReached();
            }

            if (numberOfMovements > 0 && numberOfFreeFields > 0)
            {
                for (int numberOfFieldsToAdd = Model.GameSettings.Instance.numberOfFieldsToAdd; numberOfFieldsToAdd > 0; numberOfFieldsToAdd--)
                {
                    UpdateModel();
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

        private void UpdateModel()
        {
            try
            {
                Model.FieldPosition position = Model.EmptyFieldPositionFactory.GetRandomEmptyFieldPosition();

                gameModel.UpdateFieldToRandomValue(position, Model.FieldValueFactory.GetRandomFieldValue());
            }
            catch(NoFreeFieldException)
            {

            }
        }
        private void UpdateView()
        {
            gameModelView.UpdateGameBoard(gameModel);
            gameStatisticView.UpdateStatistic(gameStatistic);
        }

        internal void GestureInterpreter(GestureController gestureController)
        {
            beforeMove = ExportGame();
            if (gestureController.IsMovedTop())
            {
                MoveTop();
            }
            else if (gestureController.IsMovedBottom())
            {
                MoveBottom();
            }
            else if (gestureController.IsMovedLeft())
            {
                MoveLeft();
            }
            else if (gestureController.IsMovedRight())
            {
                MoveRight();
            }
            else if (gestureController.IsMovedUp())
            {
                MoveUp();
            }
            else if (gestureController.IsMovedDown())
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
            Update(numberOfMovements);
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
            Update(numberOfMovements);
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
            Update(numberOfMovements);
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
            Update(numberOfMovements);
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
            Update(numberOfMovements);
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
            Update(numberOfMovements);
        }

        private int YAxisMovement(Model.FieldPosition position)
        {
            int numberOfMovements = 0;
            Model.FieldPosition newPosition = new Model.FieldPosition();
            newPosition.Reset(position);

            numberOfMovements += MoveFields(position, newPosition, "y");
            newPosition.Reset(position);

            numberOfMovements += AddFields(position, newPosition, "y");
            newPosition.Reset(position);

            numberOfMovements += MoveFields(position, newPosition, "y");
            newPosition.Reset(position);

            return numberOfMovements;
        }
        private int XAxisMovement(Model.FieldPosition position)
        {
            int numberOfMovements = 0;
            Model.FieldPosition newPosition = new Model.FieldPosition();
            newPosition.Reset(position);

            numberOfMovements += MoveFields(position, newPosition, "x");
            newPosition.Reset(position);

            numberOfMovements += AddFields(position, newPosition, "x");
            newPosition.Reset(position);

            numberOfMovements += MoveFields(position, newPosition, "x");
            newPosition.Reset(position);

            return numberOfMovements;
        }
        private int ZAxisMovement(Model.FieldPosition position)
        {
            int numberOfMovements = 0;
            Model.FieldPosition newPosition = new Model.FieldPosition();
            newPosition.Reset(position);

            numberOfMovements += MoveFields(position, newPosition, "z");
            newPosition.Reset(position);

            numberOfMovements += AddFields(position, newPosition, "z");
            newPosition.Reset(position);

            numberOfMovements += MoveFields(position, newPosition, "z");
            newPosition.Reset(position);

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
