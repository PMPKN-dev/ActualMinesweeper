using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace ActualMinesweeper.Model
{
    internal class CoordHandler
    {
        public void HandlePosition(Coord position, Rectangle field)
        {
            int[] pos = position.getCoord();
            Canvas.SetTop(field,pos[0]);
            Canvas.SetLeft(field,pos[1]);
        }
    }
}
