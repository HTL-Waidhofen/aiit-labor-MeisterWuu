using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab05_Rechteck_zeichnen
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int consoleWidth = Console.BufferWidth;
            int consoleHeight = Console.BufferHeight;

            int anzahl = ReadInt("Wie viele Rechtecke sollen gezeichnet werden? ", min: 1);

            var rechtecke = new List<Rechteck>();

            for (int i = 0; i < anzahl; i++)
            {
                Console.WriteLine();
                Console.WriteLine($"Rechteck {i + 1}:");

                int breite = ReadInt("  Breite: ", min: 1);
                int hoehe = ReadInt("  Höhe: ", min: 1);
                int x = ReadInt("  X (linke obere Ecke): ", min: 0);
                int y = ReadInt("  Y (linke obere Ecke): ", min: 0);

                // Clamp so that das Rechteck vollständig in die Konsole passt
                breite = Math.Max(1, Math.Min(breite, consoleWidth));
                hoehe = Math.Max(1, Math.Min(hoehe, consoleHeight));
                x = Math.Max(0, Math.Min(x, Math.Max(0, consoleWidth - breite)));
                y = Math.Max(0, Math.Min(y, Math.Max(0, consoleHeight - hoehe)));

                rechtecke.Add(new Rechteck(breite, hoehe, x, y));
            }

            // Anfangszeichnung
            DrawAll(rechtecke);

            Console.WriteLine();
            Console.WriteLine("Letztes Rechteck mit WASD oder Pfeiltasten verschieben. ESC zum Beenden.");

            // Interaktive Verschiebung des letzten Rechtecks
            if (rechtecke.Count > 0)
            {
                var last = rechtecke[rechtecke.Count - 1];
                while (true)
                {
                    var keyInfo = Console.ReadKey(true);

                    int dx = 0, dy = 0;
                    bool handled = true;

                    switch (keyInfo.Key)
                    {
                        case ConsoleKey.LeftArrow:
                        case ConsoleKey.A:
                            dx = -1;
                            break;
                        case ConsoleKey.RightArrow:
                        case ConsoleKey.D:
                            dx = 1;
                            break;
                        case ConsoleKey.UpArrow:
                        case ConsoleKey.W:
                            dy = -1;
                            break;
                        case ConsoleKey.DownArrow:
                        case ConsoleKey.S:
                            dy = 1;
                            break;
                        case ConsoleKey.Escape:
                            return;
                        default:
                            handled = false;
                            break;
                    }

                    if (handled && (dx != 0 || dy != 0))
                    {
                        // Verschieben und neu zeichnen
                        last.Move(dx, dy, consoleWidth, consoleHeight);
                        Console.Clear();
                        DrawAll(rechtecke);
                        Console.SetCursorPosition(0, Math.Min(consoleHeight - 1, Console.CursorTop + 1));
                       
                    }
                }
            }
            else
            {
                Console.WriteLine("Keine Rechtecke vorhanden. Ende.");
            }
        }

        static void DrawAll(List<Rechteck> liste)
        {
            Console.Clear();
            foreach (var r in liste)
                r.Zeichnen();
        }

        static int ReadInt(string prompt, int min = int.MinValue, int max = int.MaxValue)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();
                if (int.TryParse(input, out int value))
                {
                    if (value < min)
                    {
                        Console.WriteLine($"  Wert muss >= {min} sein.");
                        continue;
                    }
                    if (value > max)
                    {
                        Console.WriteLine($"  Wert muss <= {max} sein.");
                        continue;
                    }
                    return value;
                }
                Console.WriteLine("  Ungültige Eingabe, bitte ganze Zahl eingeben.");
            }
        }
    }
    }

