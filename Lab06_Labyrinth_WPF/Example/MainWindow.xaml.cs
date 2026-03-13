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
using System.Media;

namespace Example
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Figur figur = null;
        private List<Punkt> punkte = new List<Punkt>();
        private List<Gegner> gegner = new List<Gegner>();
        private int leben = 3;
        private System.Windows.Threading.DispatcherTimer gegnerTimer;
        // invulnerability after hit
        private bool isInvulnerable = false;
        private System.Windows.Threading.DispatcherTimer invTimer;

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
                        // ensure walls are base layer
                        Canvas.SetZIndex(c, 0);
                        Spielfeld.Children.Add(c);
                    }
                    else if (zeilen[a][i] == 'X')
                    {
                        figur = new Figur(i * 20, a * 20);
                        var fe = figur.GetEllipse();
                        Spielfeld.Children.Add(fe);
                        // player above walls/points
                        Canvas.SetZIndex(fe, 100);
                    }
                    else if (zeilen[a][i] == 'o')
                    {
                        // collectible point that gives an extra life
                        Punkt p = new Punkt(i * 20 + 4, a * 20 + 4);
                        punkte.Add(p);
                        var pe = p.GetEllipse();
                        Spielfeld.Children.Add(pe);
                        // show points above walls
                        Canvas.SetZIndex(pe, 50);
                    }
                }
            }

            // if no collectibles present in the map, place a few on empty tiles
            if (punkte.Count == 0)
            {
                var free = new List<Tuple<int,int>>();
                for (int ay = 0; ay < zeilen.Length; ay++)
                {
                    for (int ix = 0; ix < zeilen[ay].Length; ix++)
                    {
                        if (zeilen[ay][ix] == ' ')
                        {
                            if (figur != null && figur.X == ix * 20 && figur.Y == ay * 20) continue;
                            free.Add(Tuple.Create(ix, ay));
                        }
                    }
                }

                var rnd = new Random();
                int toPlace = Math.Min(6, free.Count);
                for (int k = 0; k < toPlace; k++)
                {
                    int idx = rnd.Next(free.Count);
                    var pos = free[idx];
                    free.RemoveAt(idx);
                    Punkt p = new Punkt(pos.Item1 * 20 + 4, pos.Item2 * 20 + 4);
                    punkte.Add(p);
                    var pe = p.GetEllipse();
                    Spielfeld.Children.Add(pe);
                    Canvas.SetZIndex(pe, 50);
                }
            }

            // initial leben display
            UpdateLebenAnzeige();

            // create 2 enemies at random free positions
            var freeForEnemies = new List<Tuple<int,int>>();
            for (int ay = 0; ay < zeilen.Length; ay++)
            {
                for (int ix = 0; ix < zeilen[ay].Length; ix++)
                {
                    if (zeilen[ay][ix] == ' ')
                    {
                        if (figur != null && figur.X == ix * 20 && figur.Y == ay * 20) continue;
                        freeForEnemies.Add(Tuple.Create(ix, ay));
                    }
                }
            }

            var rnd = new Random();
            int enemyCount = Math.Min(2, freeForEnemies.Count);
            for (int e = 0; e < enemyCount; e++)
            {
                int idx = rnd.Next(freeForEnemies.Count);
                var pos = freeForEnemies[idx];
                freeForEnemies.RemoveAt(idx);
                Gegner g = new Gegner(pos.Item1 * 20, pos.Item2 * 20);
                gegner.Add(g);
                var ge = g.GetEllipse();
                Spielfeld.Children.Add(ge);
                Canvas.SetZIndex(ge, 75);
            }

            // start timer to update enemies
            gegnerTimer = new System.Windows.Threading.DispatcherTimer();
            gegnerTimer.Interval = TimeSpan.FromMilliseconds(400);
            gegnerTimer.Tick += GegnerTimer_Tick;
            gegnerTimer.Start();

            // invulnerability timer setup
            invTimer = new System.Windows.Threading.DispatcherTimer();
            invTimer.Interval = TimeSpan.FromMilliseconds(1500); // 1.5s invuln
            invTimer.Tick += (s,ev) => {
                invTimer.Stop();
                isInvulnerable = false;
                if (figur != null) figur.SetInvulnerable(false);
            };
        }

        private void GegnerTimer_Tick(object sender, EventArgs e)
        {
            // enemies decide to move or follow
            foreach(var g in gegner)
            {
                // if within follow distance (60 px), move towards player
                int dist = Math.Abs(g.X - figur.X) + Math.Abs(g.Y - figur.Y);
                if (dist <= 60)
                {
                    g.StepTowards(figur.X, figur.Y, IsBlocked);
                }
                else
                {
                    // random walk
                    int r = new Random().Next(4);
                    int step = 20;
                    int dx = 0, dy = 0;
                    switch(r)
                    {
                        case 0: dx = step; break;
                        case 1: dx = -step; break;
                        case 2: dy = step; break;
                        case 3: dy = -step; break;
                    }
                    int nx = g.X + dx;
                    int ny = g.Y + dy;
                    if (!IsBlocked(nx, ny)) g.Move(dx, dy);
                }

                // check collision with player
                if (Math.Abs(g.X - figur.X) < 10 && Math.Abs(g.Y - figur.Y) < 10)
                {
                    if (!isInvulnerable)
                    {
                        // reduce life and move enemy away a bit
                        leben = Math.Max(0, leben - 1);
                        UpdateLebenAnzeige();
                        // play hurt sound (system beep as fallback)
                        try {
                            SystemSounds.Hand.Play();
                        } catch { }
                        // start invulnerability
                        isInvulnerable = true;
                        invTimer.Stop();
                        invTimer.Start();
                        if (figur != null) figur.SetInvulnerable(true);
                        // push enemy away
                        int pushX = (g.X <= figur.X) ? -20 : 20;
                        int pushY = (g.Y <= figur.Y) ? -20 : 20;
                        int nx = g.X + pushX;
                        int ny = g.Y + pushY;
                        if (!IsBlocked(nx, ny)) g.Move(pushX, pushY);
                    }
                }
            }
        }

        private bool IsBlocked(int targetX, int targetY)
        {
            foreach(UIElement element in Spielfeld.Children)
            {
                if (element is Canvas)
                {
                    Canvas c = (Canvas)element;
                    if (Canvas.GetLeft(c) == targetX && Canvas.GetTop(c) == targetY)
                    {
                        return true;
                    }
                }
            }
            return false;
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

                /*
                bool blocked = Spielfeld.Children.OfType<Canvas>()
                    .Any(c => (c.Tag as string) == "wall" &&
                              Canvas.GetLeft(c) == targetX &&
                              Canvas.GetTop(c) == targetY);
                */

                // Alternative implementierung
                bool blocked = false;
                foreach(UIElement element in Spielfeld.Children)
                {
                    if (element is Canvas)
                    {
                        Canvas c = (Canvas)element;
                        if (Canvas.GetLeft(c) == targetX && Canvas.GetTop(c) == targetY)
                        {
                            blocked = true;
                        }
                    }
                }

                if (!blocked)
                {
                    figur.Bewegen(dx * step, dy * step);
                    CheckCollectibles();
                    UpdateLebenAnzeige();
                }
            }
        }

        private void CheckCollectibles()
        {
            // check if figur overlaps any collectible
            Punkt found = null;
            foreach (var p in punkte)
            {
                if (p == null) continue;
                if (p.IsCollected) continue;

                if (Math.Abs(p.X - figur.X) < 10 && Math.Abs(p.Y - figur.Y) < 10)
                {
                    found = p;
                    break;
                }
            }

            if (found != null)
            {
                found.Collect();
                leben += 1; // grant extra life
            }
        }

        private void UpdateLebenAnzeige()
        {
            if (LebenText != null)
            {
                LebenText.Text = $"Leben: {leben}";
                // make sure it's visible above everything
                Canvas.SetZIndex(LebenText, 1000);
                LebenText.Background = Brushes.Black;
                LebenText.Opacity = 0.85;
            }
        }
    }
}
