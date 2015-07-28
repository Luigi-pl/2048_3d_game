using System;
namespace _2048_3d_game.Model
{
    /// <summary>
    /// Class is used to store game settings.
    /// </summary>
    class GameSettings
    {
        private static GameSettings instance;

        public int boardSize = 4;
        public int numberOfLayers = 3;
        public int numberOfFieldsToAdd = 1;

        public const int minBoardSize = 3;
        public const int maxBoardSize = 6;

        public const int minNumberofLayers = 1;
        public const int maxNumberofLayers = 3;

        public const int minNumberOfFieldsToAdd = 1;
        public const int maxNumberOfFieldsToAdd = 4;
        //////////////////////////////////////////////////
        public FieldValue targetValueOfTheField = FieldValue.eleventh;

        public const FieldValue minTargetValueOfTheField = FieldValue.seventh;
        public const FieldValue maxTargetValueOfTheField = FieldValue.fifteenth;
        //////////////////////////////////////////////////
        public bool ifTheGameIsOver = false;


        private GameSettings() { }


        public static GameSettings GetInstance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GameSettings();
                }
                return instance;
            }
        }

        /// <summary>
        /// Method converts game settings to string.
        /// </summary>
        /// <returns>string</returns>
        public String ExportSettingsToString()
        {
            String result = "";

            result += TranslateData.TrasnlateNumberToChar(boardSize) + ".";
            result += TranslateData.TrasnlateNumberToChar(numberOfLayers) + ".";
            result += TranslateData.TrasnlateNumberToChar(numberOfFieldsToAdd) + ".";
            result += TranslateData.TrasnlateNumberToChar((int)Math.Log((int)targetValueOfTheField, 2)) + ".";

            if(ifTheGameIsOver == false)
            {
                result += TranslateData.TrasnlateNumberToChar(0) + ".";
            }
            else
            {
                result += TranslateData.TrasnlateNumberToChar(1) + ".";
            }
            return result;
        }

        /// <summary>
        /// Method converts splitted string to game settings
        /// </summary>
        /// <param name="boardSize"></param>
        /// <param name="numberOfLayers"></param>
        /// <param name="numberOfFieldsToAdd"></param>
        /// <param name="targetFieldValue"></param>
        /// <param name="continuousGame"></param>
        public void ImportSettingsFromString(string boardSize, string numberOfLayers,
            string numberOfFieldsToAdd,
            string targetFieldValue,
            string continuousGame)
        {
            this.boardSize = TranslateData.TranslateCharToNumber(boardSize[0]);
            this.numberOfLayers = TranslateData.TranslateCharToNumber(numberOfLayers[0]);
            this.numberOfFieldsToAdd = TranslateData.TranslateCharToNumber(numberOfFieldsToAdd[0]);
            this.targetValueOfTheField = (FieldValue) Math.Pow(2, TranslateData.TranslateCharToNumber(targetFieldValue[0]));
            if (TranslateData.TranslateCharToNumber(continuousGame[0]) == 0)
            {
                this.ifTheGameIsOver = false;
            }
            else
            {
                this.ifTheGameIsOver = true;
            }
        }

        /// <summary>
        /// Method sets game settings
        /// </summary>
        /// <param name="boardSize">Size of single layer</param>
        /// <param name="numberOfLayers">the number of layers</param>
        /// <param name="numberOfFieldsToAdd">number of fields to be added</param>
        public void SetGameSettings(int boardSize, int numberOfLayers, int numberOfFieldsToAdd)
        {
            if (minBoardSize <= boardSize && boardSize <= maxBoardSize)
            {
                this.boardSize = boardSize;
            }
            else if (boardSize < minBoardSize)
            {
                this.boardSize = minBoardSize;
            }
            else
            {
                this.boardSize = maxBoardSize;
            }


            if (minNumberofLayers <= numberOfLayers && numberOfLayers <= maxNumberofLayers)
            {
                this.numberOfLayers = numberOfLayers;
            }
            else if (numberOfLayers < minNumberofLayers)
            {
                this.numberOfLayers = minNumberofLayers;
            }
            else
            {
                this.numberOfLayers = maxNumberofLayers;
            }


            if (minNumberOfFieldsToAdd <= numberOfFieldsToAdd && numberOfFieldsToAdd <= maxNumberOfFieldsToAdd)
            {
                this.numberOfFieldsToAdd = numberOfFieldsToAdd;
            }
            else if (numberOfFieldsToAdd < minNumberOfFieldsToAdd)
            {
                this.numberOfFieldsToAdd = minNumberOfFieldsToAdd;
            }
            else
            {
                this.numberOfFieldsToAdd = maxNumberOfFieldsToAdd;
            }

        }
        /// <summary>
        /// Method sets a target value of the field to win the game
        /// </summary>
        /// <param name="value"></param>
        public void SetTargetValueOfTheField(int value)
        {
            FieldValue fieldValue = (FieldValue)(int)Math.Pow(2, value);
            if (minTargetValueOfTheField <= fieldValue && fieldValue <= maxTargetValueOfTheField)
            {
                targetValueOfTheField = fieldValue;
            }
            else if (minTargetValueOfTheField > fieldValue)
            {
                targetValueOfTheField = minTargetValueOfTheField;
            }
            else
            {
                targetValueOfTheField = maxTargetValueOfTheField;
            }
        }
        /// <summary>
        /// Method returns a maximum target value of the field
        /// </summary>
        /// <returns></returns>
        public int GetMaxTargetValueOfTheField()
        {
            return (int)Math.Log((int)maxTargetValueOfTheField, 2);
        }
        /// <summary>
        /// Method returns a minimum target value of the field
        /// </summary>
        /// <returns></returns>
        public int GetMinTargetValueOfTheField()
        {
            return (int)Math.Log((int)minTargetValueOfTheField, 2);
        }
        /// <summary>
        /// Method returns a target value of the field
        /// </summary>
        /// <returns></returns>
        public int GetTargetValueOfTheField()
        {
            return (int)Math.Log((int)targetValueOfTheField, 2);
        }
        /// <summary>
        /// Method returns a target value of the field to win the game as string
        /// </summary>
        /// <returns></returns>
        public String GetTargetValueOfTheFieldString()
        {
            return ((int)targetValueOfTheField).ToString();
        }
    }

}
