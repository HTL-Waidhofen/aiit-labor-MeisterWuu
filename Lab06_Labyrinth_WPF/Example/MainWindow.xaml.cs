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

            StreamReader reader = new StreamReader("maze_6x6.txt");
            string inhalt = reader.ReadToEnd();
            string[] zeilen = inhalt.Split('\n');

            this.Spielfeld.Background = Brushes.Aquamarine;

            for (int a = 0; a < zeilen.Length; a++)
            {
                for (int i = 0; i < zeilen[a].Length; i++)
                {
                    if (zeilen[a][i] == '#')
                    {
                        Canvas c = new Canvas();
                        c.Background = Brushes.Red;
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
    }
}
