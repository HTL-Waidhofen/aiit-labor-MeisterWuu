using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Example
{
    internal class Gegner
    {
        int groesse = 16;
        int x;
        int y;
        Ellipse geometrie;
        private static Random rnd = new Random();

        public Gegner(int x, int y)
        {
            this.x = x;
            this.y = y;
            geometrie = new Ellipse();
            geometrie.Width = groesse;
            geometrie.Height = groesse;
            geometrie.Fill = Brushes.DarkRed;
            geometrie.Stroke = Brushes.Black;
            geometrie.StrokeThickness = 1;
            Canvas.SetLeft(geometrie, x);
            Canvas.SetTop(geometrie, y);
        }

        public void Move(int dx, int dy)
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

        public int X { get { return x; } }
        public int Y { get { return y; } }

        // step towards target by one grid cell (20 px) if not blocked
        // isBlocked returns true if the target location is blocked
        public void StepTowards(int targetX, int targetY, Func<int,int,bool> isBlocked)
        {
            int dx = targetX - x;
            int dy = targetY - y;
            int step = 20;

            if (Math.Abs(dx) <= 0 && Math.Abs(dy) <= 0) return;

            // if within follow range, try to move towards player
            int ax = Math.Abs(dx);
            int ay = Math.Abs(dy);

            // prefer axis with larger distance
            if (ax >= ay)
            {
                int sx = (dx == 0) ? 0 : (dx > 0 ? 1 : -1);
                if (sx != 0)
                {
                    int nx = x + sx * step;
                    int ny = y;
                    if (!isBlocked(nx, ny))
                    {
                        Move(sx * step, 0);
                        return;
                    }
                }

                int sy = (dy == 0) ? 0 : (dy > 0 ? 1 : -1);
                if (sy != 0)
                {
                    int nx = x;
                    int ny = y + sy * step;
                    if (!isBlocked(nx, ny))
                    {
                        Move(0, sy * step);
                        return;
                    }
                }
            }
            else
            {
                int sy = (dy == 0) ? 0 : (dy > 0 ? 1 : -1);
                if (sy != 0)
                {
                    int nx = x;
                    int ny = y + sy * step;
                    if (!isBlocked(nx, ny))
                    {
                        Move(0, sy * step);
                        return;
                    }
                }

                int sx = (dx == 0) ? 0 : (dx > 0 ? 1 : -1);
                if (sx != 0)
                {
                    int nx = x + sx * step;
                    int ny = y;
                    if (!isBlocked(nx, ny))
                    {
                        Move(sx * step, 0);
                        return;
                    }
                }
            }

            // if both preferred moves are blocked, try a random neighbor
            var dirs = new int[,] { {1,0},{-1,0},{0,1},{0,-1} };
            int order = rnd.Next(4);
            for (int i=0;i<4;i++)
            {
                int idx = (order + i) % 4;
                int nx = x + dirs[idx,0] * step;
                int ny = y + dirs[idx,1] * step;
                if (!isBlocked(nx, ny))
                {
                    Move(dirs[idx,0]*step, dirs[idx,1]*step);
                    return;
                }
            }

            // otherwise stay
        }
    }
}
