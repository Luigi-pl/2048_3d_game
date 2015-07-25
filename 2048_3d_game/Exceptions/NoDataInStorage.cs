using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2048_3d_game.Exceptions
{
    class NoDataInStorage : Exception
    {
        public NoDataInStorage()
        {
        }

        public NoDataInStorage(string message)
            : base(message)
        {
        }

        public NoDataInStorage(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
