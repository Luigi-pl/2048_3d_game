using System;
namespace _2048_3d_game.Model
{
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
        public FieldValue targetFieldValue = FieldValue.eleventh;

        public const FieldValue minTargetFieldValue = FieldValue.seventh;
        public const FieldValue maxTargetFieldValue = FieldValue.fifteenth;
        //////////////////////////////////////////////////
        public bool continuousGame = false;


        private GameSettings() { }

        public static GameSettings Instance
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

        public String ExportSettingsToString()
        {
            String result = "";

            result += TranslateData.TrasnlateNumberToChar(boardSize) + ".";
            result += TranslateData.TrasnlateNumberToChar(numberOfLayers) + ".";
            result += TranslateData.TrasnlateNumberToChar(numberOfFieldsToAdd) + ".";
            result += TranslateData.TrasnlateNumberToChar((int)Math.Log((int)targetFieldValue, 2)) + ".";

            if(continuousGame == false)
            {
                result += TranslateData.TrasnlateNumberToChar(0) + ".";
            }
            else
            {
                result += TranslateData.TrasnlateNumberToChar(1) + ".";
            }
            return result;
        }
        public void ImportSettingsFromString(string boardSize, string numberOfLayers,
            string numberOfFieldsToAdd,
            string targetFieldValue,
            string continuousGame)
        {
            this.boardSize = TranslateData.TranslateCharToNumber(boardSize[0]);
            this.numberOfLayers = TranslateData.TranslateCharToNumber(numberOfLayers[0]);
            this.numberOfFieldsToAdd = TranslateData.TranslateCharToNumber(numberOfFieldsToAdd[0]);
            this.targetFieldValue = (FieldValue) Math.Pow(2, TranslateData.TranslateCharToNumber(targetFieldValue[0]));
            if (TranslateData.TranslateCharToNumber(continuousGame[0]) == 0)
            {
                this.continuousGame = false;
            }
            else
            {
                this.continuousGame = true;
            }
        }

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

        public void SetTargetFieldValue(int value)
        {
            FieldValue fieldValue = (FieldValue)(int)Math.Pow(2, value);
            if (minTargetFieldValue <= fieldValue && fieldValue <= maxTargetFieldValue)
            {
                targetFieldValue = fieldValue;
            }
            else if (minTargetFieldValue > fieldValue)
            {
                targetFieldValue = minTargetFieldValue;
            }
            else
            {
                targetFieldValue = maxTargetFieldValue;
            }
        }
        public int GetMaxTargetFieldValue()
        {
            return (int)Math.Log((int)maxTargetFieldValue, 2);
        }
        public int GetMinTargetFieldValue()
        {
            return (int)Math.Log((int)minTargetFieldValue, 2);
        }
        public int GetTargetFieldValue()
        {
            return (int)Math.Log((int)targetFieldValue, 2);
        }
        public String GetTargetFieldValueToString()
        {
            return ((int)targetFieldValue).ToString();
        }
    }

}
