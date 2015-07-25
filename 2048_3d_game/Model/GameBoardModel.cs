using System;
namespace _2048_3d_game.Model
{
    class GameBoardModel
    {
        public FieldValue[][,] fields { get; set; }
        public int boardSize { get; set; }
        public int numberOfLayers { get; set; }

        public const FieldValue emptyField = FieldValue.empty;
        public const FieldValue firstFieldValue = FieldValue.first;
        public const FieldValue lastFieldValue = FieldValue.twelfth;

        public FieldValue highestValue = firstFieldValue;

        public GameBoardModel(int boardSize, int numberOfLayers)
        {
            this.boardSize = boardSize;
            this.numberOfLayers = numberOfLayers;


            fields = new FieldValue[numberOfLayers][,];

            ClearListOfPositionsOfEmptyField();

            for (int z = 0; z < numberOfLayers; z++)
            {
                fields[z] = new FieldValue[boardSize, boardSize];
                for (int x = 0; x < boardSize; x++)
                {
                    for (int y = 0; y < boardSize; y++)
                    {
                        fields[z][x, y] = emptyField;

                        AddFieldPositionToListOfEmptyFields(z, x, y);
                    }
                }
            }
        }

        public void ImportGameBoardModelFromString(string highestValue, string gameBoardModel)
        {
            this.highestValue = (FieldValue)Math.Pow(2, TranslateData.TranslateCharToNumber(highestValue[0]));
            int fieldValue = 0;

            EmptyFieldsPosition emptyFieldsPositions = EmptyFieldsPosition.Instance;
            emptyFieldsPositions.ClearListOfEmptyFields();

            for (int z = 0; z < numberOfLayers; z++)
            {
                for (int x = 0; x < boardSize; x++)
                {
                    for (int y = 0; y < boardSize; y++)
                    {
                        fieldValue = (int)Math.Pow(2, TranslateData.TranslateCharToNumber(
                            gameBoardModel[z * boardSize * boardSize + x * boardSize + y]));
                        if (fieldValue > 1)
                        {
                            this.fields[z][x, y] = (FieldValue)fieldValue;
                        }
                        else
                        {
                            this.fields[z][x, y] = FieldValue.empty;
                            emptyFieldsPositions.AddEmptyFieldToList(z, x, y);
                        }
                    }
                }
            }
        }

        public String ExportGameBoardModelToString()
        {
            String result = "";
            result += TranslateData.TrasnlateNumberToChar((int) Math.Log((int)highestValue, 2)) + ".";

            for (int z = 0; z < numberOfLayers; z++)
            {
                for (int x = 0; x < boardSize; x++)
                {
                    for (int y = 0; y < boardSize; y++)
                    {
                        result += TranslateData.TrasnlateNumberToChar((int)Math.Log((int)fields[z][x, y], 2));
                    }
                }
            }
            result += ".";
            return result;
        }

        public void UpdateFieldToRandomValue(FieldPosition position, FieldValue fieldValue)
        {
            fields[position.z][position.x, position.y] = fieldValue;
        }

        public FieldValue[,] GetLayerAt(int z)
        {
            return fields[z];
        }

        private void EmptifyField(FieldPosition position)
        {
            fields[position.z][position.x, position.y] = GameBoardModel.emptyField;
        }

        public bool HaveFieldsSameValue(FieldPosition positionA, FieldPosition positionB)
        {
            if (fields[positionA.z][positionA.x, positionA.y] == fields[positionB.z][positionB.x, positionB.y])
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool IsFieldNonEmpty(FieldPosition position)
        {
            return (fields[position.z][position.x, position.y] > GameBoardModel.emptyField);
        }
        public bool IsFieldEmpty(FieldPosition position)
        {
            return !IsFieldNonEmpty(position);
        }

        public void MoveFieldToEmptyField(FieldPosition from, FieldPosition to)
        {
            MoveFieldTo(from, to);

            EmptifyField(from);
            AddFieldPositionToListOfEmptyFields(from);

            RemoveFieldPositionFromListOfEmptyFields(to);
        }

        private void MoveFieldTo(FieldPosition from, FieldPosition to)
        {
            fields[to.z][to.x, to.y] = fields[from.z][from.x, from.y];
        }

        public void MoveFieldToNonEmptyField(FieldPosition from, FieldPosition to)
        {
            SumFields(from, to);
            ChangeHighestValueOfField(to);

            EmptifyField(from);
            AddFieldPositionToListOfEmptyFields(from);
        }

        private void SumFields(FieldPosition from, FieldPosition to)
        {
            fields[to.z][to.x, to.y] += (int)fields[from.z][from.x, from.y];
        }

        private void ChangeHighestValueOfField(FieldPosition position)
        {
            if (fields[position.z][position.x, position.y] > highestValue)
            {
                highestValue = fields[position.z][position.x, position.y];
            }
        }
        public bool IsTargetFieldValueReached(FieldValue value)
        {
            //if (highestValue >= FieldValue.second)
            if (highestValue >= value)
            {
                return true;
            }
            return false;
        }

        public FieldValue GetValue(FieldPosition position)
        {
            return fields[position.z][position.x, position.y];
        }

        private void AddFieldPositionToListOfEmptyFields(int z, int x, int y)
        {
            EmptyFieldsPosition freeFields = EmptyFieldsPosition.Instance;
            freeFields.AddEmptyFieldToList(z, x, y);
        }

        private void AddFieldPositionToListOfEmptyFields(FieldPosition position)
        {
            AddFieldPositionToListOfEmptyFields(position.z, position.x, position.y);
        }

        private void RemoveFieldPositionFromListOfEmptyFields(int z, int x, int y)
        {
            EmptyFieldsPosition freeFields = EmptyFieldsPosition.Instance;
            freeFields.RemoveEmptyFieldFromList(z, x, y);
        }

        private void RemoveFieldPositionFromListOfEmptyFields(FieldPosition position)
        {
            RemoveFieldPositionFromListOfEmptyFields(position.z, position.x, position.y);
        }

        private void ClearListOfPositionsOfEmptyField()
        {
            EmptyFieldsPosition freeFields = EmptyFieldsPosition.Instance;
            freeFields.ClearListOfEmptyFields();
        }
    }
    
}
