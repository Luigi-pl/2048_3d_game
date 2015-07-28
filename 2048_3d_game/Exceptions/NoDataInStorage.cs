using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2048_3d_game.Exceptions
{
    /// <summary>
    /// This exception is used  to inform, that in storage thera aren't any data
    /// </summary>
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
