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
        private int breite;
        private int hoehe;
        // Konstruktor
        public Rechteck(int breite, int hoehe)
        {
            this.breite = breite;
            this.hoehe = hoehe;
        }
        // Methode zum Zeichnen des Rechtecks
        public void Zeichnen()
        {
            for (int j = 0; j < breite; j++)
            {
                Console.Write("*");
                
            }
            Console.WriteLine();
            for (int i = 0; i < hoehe - 2; i++)
            {
                Console.Write("*");
                for (int j = 0; j < breite - 2; j++)
                {
                    Console.Write(" ");
                }
                Console.WriteLine("*");
            }

            for (int j = 0; j < breite; j++)
            {
                Console.Write("*");

            }
        }
    }
}
