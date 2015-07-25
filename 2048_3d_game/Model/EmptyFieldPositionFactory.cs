using System;
using _2048_3d_game.Exceptions;
namespace _2048_3d_game.Model
{
    static class EmptyFieldPositionFactory
    {
        private static Random generator = new Random();

        public static FieldPosition GetRandomEmptyFieldPosition()
        {
            
            EmptyFieldsPosition freeFields = EmptyFieldsPosition.Instance;

            int position = generator.Next(freeFields.NumberOfEmptyFields());
            FieldPosition pos;

            try
            {
                pos = freeFields.GetEmptyFieldFromListAt(position);
            }
            catch (NoFreeFieldException e)
            {
                throw e;
            }

            freeFields.RemoveEmptyFieldFromList(pos.z, pos.x, pos.y);
            return pos;
        }
    }
}
