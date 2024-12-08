using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Texagochi
{
    internal class Player
    {
        public string id;
        public bool registered;
        public int currency;

        public Player(string id) 
        {
            this.id = id;
        }
    }
}
