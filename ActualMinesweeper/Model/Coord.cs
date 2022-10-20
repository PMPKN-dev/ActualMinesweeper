using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActualMinesweeper.Model
{
    class Coord
    {
        int x;
        int y;

        public Coord(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public int[] getCoord()
        {
            int[] a = { x, y };
            return a;
        }
    }
}
