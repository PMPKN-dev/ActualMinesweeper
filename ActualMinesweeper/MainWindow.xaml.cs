using ActualMinesweeper.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;

namespace ActualMinesweeper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Field[,] fields;
        int MinesMax = 40;
        int MinesCurrent = 0;


        public MainWindow()
        {
            InitializeComponent();
            LoadFields();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            (sender as Button).ContextMenu.IsEnabled = true;
            (sender as Button).ContextMenu.PlacementTarget = (sender as Button);
            (sender as Button).ContextMenu.Placement = System.Windows.Controls.Primitives.PlacementMode.Bottom ;
            (sender as Button).ContextMenu.IsOpen = true;


        }


        private void LoadFields()
        {
            fields = new Field[21,21];
            for (int i = 0;i < 21; i++)
            {
                for(int j = 0;j < 21; j++)
                {
                    Field temp = new Field(new Coord(i * 16, j * 16), false, Color.FromRgb(169, 169, 169));

                    DrawingBoard.Children.Add(temp.UIField);

                    Random random = new Random();
                    int chance = random.Next(10);

                    if(chance >= 9 && MinesCurrent < MinesMax) //decided to make it so that the chance for it to be a mine is higher than the percentual amount for max mines
                    {
                        temp.IsMine = true;
                        temp.UIField.Stroke = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                    }

                    temp.UIField.MouseDown += new MouseButtonEventHandler(UIField_MouseDown);

                    //todo add event handler or something for revealing the fields
                    fields[j,i] = temp;
                }
            }

        }

        private void UIField_MouseDown(object sender, RoutedEventArgs e)
        {
            Reveal(GetField((Rectangle) sender));
        }

        private void Cascade(Rectangle Field)
        {
            List<Field> temp = GetNeighbors(Field);
            foreach(Field field in temp) //calls reveal on all neighbors
            {
                Reveal(GetField(Field));
            }
        }

        private void Reveal(Field field)
        {
            if (field.IsOpened || field.IsFlagged)
            {
                //ignore
            }
            else if (NeighborHasMine(field) >= 0)
            {
                AddBombNumber(field, NeighborHasMine(field));
                OpenField(field);
            }else if (!field.IsMine && !field.IsOpened)
            {
                OpenField(field);

            }
            else if (field.IsMine)
            {
                GameOver();
            }

        }

        //This does not work :) fixed?
        private List<Field> GetNeighbors(Rectangle rectangle)
        {
            int[] temp = GetField(rectangle).coord.getCoord();

            List<Field> result = new List<Field>();

            int y = (temp[0] / 16)-1;
            int x = (temp[1] / 16)-1;

            for(int i = x-1;i<x+1; i++)
            {
                for(int j = y - 1; j < y + 1; j++)
                {
                    if (i == x && j == y)
                    {

                    }
                    result.Add(fields[j, i]);

                }
            }
            return result;
            
        }

        private Field GetField(Rectangle rectangle)
        {

            int x = (int) (Canvas.GetLeft(rectangle) / 16);
            int y = (int) (Canvas.GetTop(rectangle) / 16);

            return fields[x, y];



        }

        private void OpenField(Field field) 
        {
            field.IsOpened = true;
            field.UIField.Fill = new SolidColorBrush(Color.FromRgb(0, 255, 0));
            field.UIField.Stroke = null;

        }


        private int NeighborHasMine(Field field)
        {
            int counter = 0;
            List<Field> temp = GetNeighbors(field.UIField);
            foreach (Field Field in temp) 
            {
                if (Field.IsMine)
                {
                    counter++;
                }
            }

            return counter;

        }

        private void AddBombNumber(Field field, int amount)
        {

            int[] pos =  field.coord.getCoord();

            TextBox textBlock = new TextBox();
            textBlock.Text = $"{amount}";
            textBlock.Height = 16;
            textBlock.Width = 16;
            Canvas.SetLeft(textBlock,pos[0]);
            Canvas.SetTop(textBlock,pos[1]);

            DrawingBoard.Children.Add(textBlock);
        }

        private void GameOver()
        {
            TextBox textBox = new TextBox();
            textBox.Text = "That was a mine, buddy.\n The Game is over.";
            textBox.Foreground = new SolidColorBrush(Color.FromRgb(255, 0, 0));
            Board.Children.Add(textBox);
        }


        //todo: This will need two methods, a cascade and a reveal
        /*
         * Cascade is to be used when the field in question has no mines neighbouring meanwhile reveal simply reveals the current gield
         * Cascade calls reveal on all non-revealed neighbors
         * Reveal can be called through cascade or through clicking on a field
         * If a manually revealed Field has no neighbouring bombs, it launches cascade as well
         */
    }
}
