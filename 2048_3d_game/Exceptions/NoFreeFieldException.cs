using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2048_3d_game.Exceptions
{
    class NoFreeFieldException : Exception
    {
        public NoFreeFieldException()
        {
        }

        public NoFreeFieldException(string message)
            : base(message)
        {
        }

        public NoFreeFieldException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
