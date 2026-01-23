using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab05_Rechteck_zeichnen
{
    internal class Rechteck
    {
        // Attribute
        public int Breite { get; private set; }
        public int Hoehe { get; private set; }
        public int X { get; private set; }
        public int Y { get; private set; }

        // Konstruktor
        public Rechteck(int breite, int hoehe, int x, int y)
        {
            Breite = Math.Max(1, breite);
            Hoehe = Math.Max(1, hoehe);
            X = x;
            Y = y;
        }

        // Position verschieben (delta)
        public void Move(int dx, int dy, int maxWidth, int maxHeight)
        {
            int newX = X + dx;
            int newY = Y + dy;

            // Clamp innerhalb des sichtbaren Bereichs so, dass das Rechteck komplett sichtbar bleibt
            newX = Math.Max(0, Math.Min(newX, Math.Max(0, maxWidth - Breite)));
            newY = Math.Max(0, Math.Min(newY, Math.Max(0, maxHeight - Hoehe)));

            X = newX;
            Y = newY;
        }

        // Methode zum Zeichnen des Rechtecks (nur Rand)
        public void Zeichnen()
        {
            // Schutz: Breite/ Höhe minimal 1
            if (Breite <= 0 || Hoehe <= 0)
                return;

            // Falls Breite oder Höhe 1 -> Sonderfall: eine Linie zeichnen
            try
            {
                if (Hoehe == 1)
                {
                    Console.SetCursorPosition(X, Y);
                    for (int j = 0; j < Breite; j++)
                        Console.Write('*');
                    return;
                }

                if (Breite == 1)
                {
                    for (int i = 0; i < Hoehe; i++)
                    {
                        Console.SetCursorPosition(X, Y + i);
                        Console.Write('*');
                    }
                    return;
                }

                // Obere Kante
                Console.SetCursorPosition(X, Y);
                for (int j = 0; j < Breite; j++)
                    Console.Write('*');

                // Mittlere Zeilen
                for (int i = 1; i < Hoehe - 1; i++)
                {
                    Console.SetCursorPosition(X, Y + i);
                    Console.Write('*');
                    for (int j = 0; j < Breite - 2; j++)
                        Console.Write(' ');
                    Console.Write('*');
                }

                // Untere Kante
                Console.SetCursorPosition(X, Y + Hoehe - 1);
                for (int j = 0; j < Breite; j++)
                    Console.Write('*');
            }
            catch (ArgumentOutOfRangeException)
            {
                // Wenn SetCursorPosition außerhalb des Buffers liegt: still fail (Positionen
                // sollten durch das aufrufende Programm geclamped werden).
            }
        }
    }
}

