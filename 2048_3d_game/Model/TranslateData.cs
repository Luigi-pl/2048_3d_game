using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2048_3d_game.Model
{
    class TranslateData
    {
        /// <summary>
        /// Method converts number to char. 
        /// </summary>
        /// <param name="value">Number</param>
        /// <returns>Char</returns>
        public static char TrasnlateNumberToChar(int value)
        {
            return (char)(65 + value);
        }
        /// <summary>
        /// Method converts char to number
        /// </summary>
        /// <param name="value">Char</param>
        /// <returns>Number</returns>
        public static int TranslateCharToNumber(char value)
        {
            return ((int)value - 65);
        }
    }
}
