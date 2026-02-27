using System;
using System.Collections.Generic;
using System.IO;
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

namespace Example
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Figur figur = null;

        public MainWindow()
        {
            InitializeComponent();

            StreamReader reader = new StreamReader("maze_10x10.txt");
            string inhalt = reader.ReadToEnd();
            string[] zeilen = inhalt.Split('\n');

            this.Spielfeld.Background = Brushes.Black;

            for (int a = 0; a < zeilen.Length; a++)
            {
                for (int i = 0; i < zeilen[a].Length; i++)
                {
                    if (zeilen[a][i] == '#')
                    {
                        Canvas c = new Canvas();
                        c.Background = Brushes.DarkOliveGreen;
                        c.Tag = "wall";
                        c.Width = 20;
                        c.Height = 20;
                        Canvas.SetTop(c, a * 20);
                        Canvas.SetLeft(c, i * 20);
                        Spielfeld.Children.Add(c);
                    }
                    else if (zeilen[a][i] == 'X')
                    {
                        figur = new Figur(i * 20, a * 20);
                        Spielfeld.Children.Add(figur.GetEllipse());
                    }
                }
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            int dx = 0;
            int dy = 0;
            if (e.Key == Key.Right || e.Key == Key.D) dx = 1;
            if (e.Key == Key.Up || e.Key == Key.W) dy = -1;
            if (e.Key == Key.Left || e.Key == Key.A) dx = -1;
            if (e.Key == Key.Down || e.Key == Key.S) dy = 1;

            if (dx != 0 || dy != 0)
            {
                int step = 20; // move by one grid cell (20 pixels)
                int targetX = figur.X + dx * step;
                int targetY = figur.Y + dy * step;

                bool blocked = Spielfeld.Children.OfType<Canvas>()
                    .Any(c => (c.Tag as string) == "wall" &&
                              Canvas.GetLeft(c) == targetX &&
                              Canvas.GetTop(c) == targetY);

                if (!blocked)
                {
                    figur.Bewegen(dx * step, dy * step);
                }
            }
        }
    }
}
