using System;
namespace _2048_3d_game.Model
{
    /// <summary>
    /// Class is used to store field values in current game
    /// </summary>
    class GameBoardModel
    {
        public FieldValue[][,] fields { get; set; }
        public int boardSize { get; set; }
        public int numberOfLayers { get; set; }

        public const FieldValue emptyField = FieldValue.empty;
        public const FieldValue firstFieldValue = FieldValue.first;
        public const FieldValue lastFieldValue = FieldValue.twelfth;

        public FieldValue highestValue = firstFieldValue;

        /// <summary>
        /// Constructor creates and fills fields.
        /// </summary>
        /// <param name="boardSize">Size of single layer</param>
        /// <param name="numberOfLayers">the number of layers</param>
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
        /// <summary>
        /// Method converts splitted string to highest field value and field values
        /// </summary>
        /// <param name="highestValue"></param>
        /// <param name="gameBoardModel"></param>
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
        /// <summary>
        /// Method converts field values and highest field value to string
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Method adds specified value on specified position
        /// </summary>
        /// <param name="position"></param>
        /// <param name="fieldValue"></param>
        public void UpdateFieldToRandomValue(FieldPosition position, FieldValue fieldValue)
        {
            fields[position.z][position.x, position.y] = fieldValue;
        }

        /// <summary>
        /// Method returns layer (array of field values)
        /// </summary>
        /// <param name="z"></param>
        /// <returns></returns>
        public FieldValue[,] GetLayerAt(int z)
        {
            return fields[z];
        }

        /// <summary>
        /// Method sets field value to empty
        /// </summary>
        /// <param name="position"></param>
        private void EmptifyField(FieldPosition position)
        {
            fields[position.z][position.x, position.y] = GameBoardModel.emptyField;
        }

        /// <summary>
        /// Method checks if two fields have the same value
        /// </summary>
        /// <param name="positionA"></param>
        /// <param name="positionB"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Method checks if field is not empty
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public bool IsFieldNonEmpty(FieldPosition position)
        {
            return (fields[position.z][position.x, position.y] > GameBoardModel.emptyField);
        }
        /// <summary>
        /// Method checks if field is empty
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public bool IsFieldEmpty(FieldPosition position)
        {
            return !IsFieldNonEmpty(position);
        }
        /// <summary>
        /// Method moves field with value to field without value
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        public void MoveFieldToEmptyField(FieldPosition from, FieldPosition to)
        {
            fields[to.z][to.x, to.y] = fields[from.z][from.x, from.y];

            EmptifyField(from);
            AddFieldPositionToListOfEmptyFields(from);

            RemoveFieldPositionFromListOfEmptyFields(to);
        }
        /// <summary>
        /// Method adds the value of the first field to the value of the second field, 
        /// then the value of the first field is set to empty
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        public void MoveFieldToNonEmptyField(FieldPosition from, FieldPosition to)
        {
            fields[to.z][to.x, to.y] += (int)fields[from.z][from.x, from.y];
            ChangeHighestValueOfField(to);

            EmptifyField(from);
            AddFieldPositionToListOfEmptyFields(from);
        }

        /// <summary>
        /// Method changes the value of highestValue
        /// </summary>
        /// <param name="position"></param>
        private void ChangeHighestValueOfField(FieldPosition position)
        {
            if (fields[position.z][position.x, position.y] > highestValue)
            {
                highestValue = fields[position.z][position.x, position.y];
            }
        }
        /// <summary>
        /// Method checks if player won the game by achievieng target value of the field
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool IsTargetFieldValueReached(FieldValue value)
        {
            if (highestValue >= value)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Method returns field value
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public FieldValue GetValue(FieldPosition position)
        {
            return fields[position.z][position.x, position.y];
        }

        /// <summary>
        /// Method adds specified field to list of empty fields
        /// </summary>
        /// <param name="z"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        private void AddFieldPositionToListOfEmptyFields(int z, int x, int y)
        {
            EmptyFieldsPosition freeFields = EmptyFieldsPosition.Instance;
            freeFields.AddEmptyFieldToList(z, x, y);
        }
        /// <summary>
        /// Method adds specified field to list of empty fields
        /// </summary>
        /// <param name="position"></param>
        private void AddFieldPositionToListOfEmptyFields(FieldPosition position)
        {
            AddFieldPositionToListOfEmptyFields(position.z, position.x, position.y);
        }
        /// <summary>
        /// Method removes specified field from list of empty fields
        /// </summary>
        /// <param name="z"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        private void RemoveFieldPositionFromListOfEmptyFields(int z, int x, int y)
        {
            EmptyFieldsPosition freeFields = EmptyFieldsPosition.Instance;
            freeFields.RemoveEmptyFieldFromList(z, x, y);
        }
        /// <summary>
        /// Method removes specified field from list of empty fields
        /// </summary>
        /// <param name="position"></param>
        private void RemoveFieldPositionFromListOfEmptyFields(FieldPosition position)
        {
            RemoveFieldPositionFromListOfEmptyFields(position.z, position.x, position.y);
        }
        /// <summary>
        /// Method clears list of empty fields
        /// </summary>
        private void ClearListOfPositionsOfEmptyField()
        {
            EmptyFieldsPosition freeFields = EmptyFieldsPosition.Instance;
            freeFields.ClearListOfEmptyFields();
        }
    }
    
}
