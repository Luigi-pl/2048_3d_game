using System.Collections.Generic;
using System.Linq;
using _2048_3d_game.Exceptions;
namespace _2048_3d_game.Model
{
    /// <summary>
    /// Class is used to store list of empty fields
    /// </summary>
    class EmptyFieldsPosition
    {
        private static List<int> listOfEmptyFieldsPosition;
        private static EmptyFieldsPosition instance;

        private EmptyFieldsPosition() { }

        public static EmptyFieldsPosition Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new EmptyFieldsPosition();
                    listOfEmptyFieldsPosition = new List<int>();
                }
                return instance;
            }
        }

        /// <summary>
        /// Method clears list of empty fields
        /// </summary>
        public void ClearListOfEmptyFields()
        {
            listOfEmptyFieldsPosition.Clear();
        }

        /// <summary>
        /// Method encodes field postion to number
        /// </summary>
        /// <param name="z"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private int EncodePositionOfField(int z, int x, int y)
        {
            return z * 100 + x * 10 + y;
        }
        /// <summary>
        /// Method decodes number (encoded field position) to field position
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        private FieldPosition DecodePositionOfField(int code)
        {
            int z = (int)(code) / 100;
            int x = (int)(code - z * 100) / 10;
            int y = (int)(code - z * 100 - x * 10) / 1;
            return new FieldPosition(x, y, z);
        }
        /// <summary>
        /// Method adds field on specified position (z, x, y) to list of empty fields
        /// </summary>
        /// <param name="z"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void AddEmptyFieldToList(int z, int x, int y)
        {
            listOfEmptyFieldsPosition.Add(EncodePositionOfField(z, x, y));
        }
        /// <summary>
        /// Method removes field on specified position (z, x, y) from list of empty fields
        /// </summary>
        /// <param name="z"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void RemoveEmptyFieldFromList(int z, int x, int y)
        {
            listOfEmptyFieldsPosition.Remove(EncodePositionOfField(z, x, y));
        }

        /// <summary>
        /// Method returns empty field from specified position
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public FieldPosition GetEmptyFieldFromListAt(int i)
        {
            try
            {
                return DecodePositionOfField(listOfEmptyFieldsPosition.ElementAt(i));
            }
            catch (System.ArgumentOutOfRangeException e)
            {
                throw new NoFreeFieldException("There isn't any free field", e);
            }
        }
        /// <summary>
        /// Method returns number of the empty fields
        /// </summary>
        /// <returns></returns>
        public int NumberOfEmptyFields()
        {
            return listOfEmptyFieldsPosition.Count;
        }
    }
    
}
