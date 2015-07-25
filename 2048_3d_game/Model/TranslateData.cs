using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2048_3d_game.Model
{
    class TranslateData
    {
        public static char TrasnlateNumberToChar(int value)
        {
            return (char)(65 + value);
        }

        public static int TranslateCharToNumber(char value)
        {
            return ((int)value - 65);
        }
    }
}
