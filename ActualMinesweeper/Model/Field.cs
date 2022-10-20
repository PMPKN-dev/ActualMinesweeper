using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using ActualMinesweeper.Model;


namespace ActualMinesweeper.Model
{
    internal class Field
    {
        public bool IsMine { get; set; }
        public bool IsOpened { get; set; }
        public bool IsFlagged { get; set; }
        public Coord coord { get; set; }
        public Rectangle UIField { get; set; }
        


        public Field(Coord position, bool IsMine, Color color)
        {
            this.IsMine = IsMine;

            this.UIField = new Rectangle();
            this.UIField.Height = 16;
            this.UIField.Width = 16;
            this.UIField.Fill = new SolidColorBrush(color);
            this.UIField.Stroke = new SolidColorBrush(Color.FromRgb(0,0,0));
            this.coord = position;

            CoordHandler ch = new CoordHandler();
            ch.HandlePosition(coord, UIField);

            this.IsFlagged = false;
            this.IsOpened = false;

            
        }

    }
}
