using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2048_3d_game.Model
{
    static class FieldValueFactory
    {
        private static Random generator = new Random();

        static public FieldValue GetRandomFieldValue()
        {
            int chance = 2;

            
            if ((generator.Next() % chance) == 0)
            {
                return FieldValue.first;
            }
            else
            {
                return FieldValue.first;
            }
        }
    }
}
