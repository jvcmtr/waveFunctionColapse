using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace waveFunctionColapse1
{
    internal class Cell
    {
        public bool colapsed = false;
        public List<Drawable> PossibleStates;

        public bool Update(Cell[] neibours)
        {
            if (colapsed)
            {
                return true;
            }

            foreach (Cell neibour in neibours)
            {
                if (!neibour.colapsed)
                {
                    continue;
                }

                string Allowed = neibour.PossibleStates[0].allowedNabours;
                Drawable[] Temp = PossibleStates.ToArray();
                foreach (Drawable d in Temp)
                {
                    if (!Allowed.Contains(d.symbol))
                    {
                        PossibleStates.Remove(d);
                    }
                }
            }

            if (PossibleStates.Count == 0)
            {
                /*
                PossibleStates.Add(new(" ", "░▒▓"));
                colapsed= true;
                return true;
                */
                return false;
            }

            if (PossibleStates.Count == 1)
            {
                colapsed = true;
            }


            return true;
        }

        public Cell(List<Drawable> possibleStates)
        {
            PossibleStates = possibleStates;
        }
    }
}
