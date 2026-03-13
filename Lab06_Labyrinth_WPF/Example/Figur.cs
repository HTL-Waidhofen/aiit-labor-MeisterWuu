using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Example
{
    internal class Figur
    {
        int hoehe = 16;
        int breite = 16;
        int x;
        int y;
        Ellipse geometrie;

        public Figur(int x, int y)
        {
            this.x = x;
            this.y = y;
            geometrie = new Ellipse();
            geometrie.Width = breite;
            geometrie.Height = hoehe;
            geometrie.Fill = Brushes.Firebrick;
            Canvas.SetLeft(geometrie, x);
            Canvas.SetTop(geometrie, y);
        }

        public void Bewegen(int dx, int dy)
        {
            x += dx;
            y += dy;
            Canvas.SetLeft(geometrie, x);
            Canvas.SetTop(geometrie, y);
        }

        public Ellipse GetEllipse()
        {
            return geometrie;
        }

        // indicate invulnerability visually (lower opacity)
        public void SetInvulnerable(bool inv)
        {
            geometrie.Opacity = inv ? 0.4 : 1.0;
        }

        // Expose current position so caller can check collisions
        public int X { get { return x; } }
        public int Y { get { return y; } }
    }
}
