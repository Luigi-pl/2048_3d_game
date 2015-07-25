using System.Collections.Generic;
using System.Linq;
using _2048_3d_game.Exceptions;
namespace _2048_3d_game.Model
{
    class EmptyFieldsPosition
    {
        private static List<int> listOfEmptyFieldsPoisition;
        private static EmptyFieldsPosition instance;

        private EmptyFieldsPosition() { }

        public static EmptyFieldsPosition Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new EmptyFieldsPosition();
                    listOfEmptyFieldsPoisition = new List<int>();
                }
                return instance;
            }
        }

        public void ClearListOfEmptyFields()
        {
            listOfEmptyFieldsPoisition.Clear();
        }

        private int EncodePositionOfField(int z, int x, int y)
        {
            return z * 100 + x * 10 + y;
        }
        private FieldPosition DecodePositionOfField(int code)
        {
            int z = (int)(code) / 100;
            int x = (int)(code - z * 100) / 10;
            int y = (int)(code - z * 100 - x * 10) / 1;
            return new FieldPosition(x, y, z);
        }

        public void AddEmptyFieldToList(int z, int x, int y)
        {
            listOfEmptyFieldsPoisition.Add(EncodePositionOfField(z, x, y));
        }

        public void RemoveEmptyFieldFromList(int z, int x, int y)
        {
            listOfEmptyFieldsPoisition.Remove(EncodePositionOfField(z, x, y));
        }
        public void RemoveEmptyFieldFromList(FieldPosition position)
        {
            listOfEmptyFieldsPoisition.Remove(EncodePositionOfField(position.z, position.x, position.y));
        }

        public FieldPosition GetEmptyFieldFromListAt(int i)
        {
            try
            {
                return DecodePositionOfField(listOfEmptyFieldsPoisition.ElementAt(i));
            }
            catch (System.ArgumentOutOfRangeException e)
            {
                throw new NoFreeFieldException("There isn't any free field", e);
            }
        }

        public int NumberOfEmptyFields()
        {
            return listOfEmptyFieldsPoisition.Count;
        }
    }
    
}
