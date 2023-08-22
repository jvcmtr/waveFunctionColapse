using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace waveFunctionColapse1
{
    internal class Drawable
    {
        // █░▒▓ 
        public string symbol = " ";
        public string allowedNabours = "";

        public Drawable(string type, string allowed)
        {
            symbol = type;
            allowedNabours = allowed;
        }

        public void draw()
        {
            Console.Write(symbol);
        }
    }
}
