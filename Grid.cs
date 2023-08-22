using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace waveFunctionColapse1
{
    internal class Grid
    {
        int[] dimentions;
        int NumberOfCells;
        bool DrawFirstRow = true;

        public Cell[] cells;
        int cellsColapsed = 0;
        
        Random rng;
        int ErrorAdress = -1;

        public Grid(int[] dimentionsXY)
        {
            dimentions = dimentionsXY;
            NumberOfCells = dimentions[0] * dimentions[1];
            cells = new Cell[NumberOfCells];
            rng = new Random();
        }

        public void Init(List<Drawable> PossibleStates)
        {
            for (int i = 0; i < NumberOfCells; i++)
            {
                Drawable[] states = new Drawable[PossibleStates.Count];
                PossibleStates.CopyTo(states);

                cells[i] = new Cell(states.ToList<Drawable>());
            }
        }


        // Usado para fazer com que a GRID alinhe com uma grid desenhada anteriormente
        public void Init(List<Drawable> PossibleStates, Cell[] Initialcells)
        {
            DrawFirstRow = false;
            for (int i = 0; i < Initialcells.Length; i++)
            {
                cells[i] = Initialcells[i];
            }
            for (int i = Initialcells.Length; i < NumberOfCells; i++)
            {
                Drawable[] states = new Drawable[PossibleStates.Count];
                PossibleStates.CopyTo(states);

                cells[i] = new Cell(states.ToList<Drawable>());
            }
        }

        public bool Run()
        {
            bool done = false;
            ColapseRandomCell();
            while (!done)
            {
                //Console.Write($"Loading: {(((float) cellsColapsed / (float)NumberOfCells) * 100).ToString("0.00")} %    \r");
                Update();
                if (CheckDone() || ErrorAdress != -1)
                {
                    done = true;
                    break;
                };
                ColapseLowestPossibilityCell();
            }
            return Draw();
        }

        private void ForceColapse(int cellIndex)
        {
            int forcedState = rng.Next(cells[cellIndex].PossibleStates.Count);
            cells[cellIndex].PossibleStates[0] = cells[cellIndex].PossibleStates[forcedState];
            cells[cellIndex].colapsed = true;
        }

        private void ColapseRandomCell()
        {
            int r = rng.Next(NumberOfCells);
            ForceColapse(r);
        }

        private void ColapseLowestPossibilityCell()
        {
            int index = -1;
            for (int i = 0; i < cells.Length; i++)
            {
                Cell c = cells[i];

                if (c.colapsed)
                {
                    continue;
                }

                if (index == -1)
                {
                    index = i;
                }

                if (c.PossibleStates.Count < cells[index].PossibleStates.Count)
                {
                    index = i;
                }
            }

            ForceColapse(index);
        }

        private void Update()
        {
            for (int i = 0; i < NumberOfCells; i++)
            {
                int upperNeibour = i - dimentions[0];
                int bottomNeibour = i + dimentions[0];
                int rightNeibour = i + 1;
                int leftNeibour = i - 1;

                List<Cell> neibours = new List<Cell>();
                if (!(upperNeibour < 0))
                {
                    neibours.Add(cells[upperNeibour]);
                }

                if (!(bottomNeibour > NumberOfCells - 1))
                {
                    neibours.Add(cells[bottomNeibour]);
                }

                if (rightNeibour % NumberOfCells != 1 && rightNeibour < NumberOfCells)
                {
                    neibours.Add(cells[rightNeibour]);
                }

                if (!(leftNeibour % dimentions[0] < 1))
                {
                    neibours.Add(cells[leftNeibour]);
                }

                Cell[] r = neibours.ToArray();
                if (!cells[i].Update(r))
                {
                    ErrorAdress = i;
                    break;
                }
            }
        }

        private bool CheckDone()
        {
            int colapsed = 0;
            foreach (Cell cell in cells)
            {
                if (cell.colapsed)
                {
                    colapsed++;
                }
            }

            cellsColapsed = colapsed;
            return (NumberOfCells- cellsColapsed) == 0;
        }

        public bool Draw()
        {
            //Console.WriteLine();
            if (ErrorAdress > -1)
            {
                Console.WriteLine($"#ERRO Não foi possivel concluir o resultado da celula de indice{ErrorAdress}");
                return false;
            }

            int begginAt = DrawFirstRow ? 0 : dimentions[0]+1;
            for (int i = 0; i < NumberOfCells; i++)
            {
                if (i % dimentions[0] == 0)
                {
                    Console.WriteLine();
                }

                cells[i].PossibleStates[0].draw();
                cells[i].PossibleStates[0].draw();
            }

            return true;
        }
    }
}
