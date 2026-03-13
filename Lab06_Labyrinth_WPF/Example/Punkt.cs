using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Example
{
    internal class Punkt
    {
        int groesse = 12;
        int x;
        int y;
        Ellipse geometrie;
        public bool IsCollected { get; private set; }

        public Punkt(int x, int y)
        {
            this.x = x;
            this.y = y;
            geometrie = new Ellipse();
            geometrie.Width = groesse;
            geometrie.Height = groesse;
            geometrie.Fill = Brushes.Gold;
            geometrie.Stroke = Brushes.Black;
            geometrie.StrokeThickness = 1;
            Canvas.SetLeft(geometrie, x);
            Canvas.SetTop(geometrie, y);
            // make sure points are visible above walls
            Canvas.SetZIndex(geometrie, 50);
        }

        public void Collect()
        {
            IsCollected = true;
            // hide visually
            geometrie.Visibility = System.Windows.Visibility.Hidden;
        }

        public Ellipse GetEllipse()
        {
            return geometrie;
        }

        public int X { get { return x; } }
        public int Y { get { return y; } }
    }
}
