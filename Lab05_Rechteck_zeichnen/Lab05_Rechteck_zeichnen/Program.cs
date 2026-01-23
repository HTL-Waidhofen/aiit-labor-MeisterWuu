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
            Console.Write("Bitte breite eingeben: \n");
            int breite = int.Parse(Console.ReadLine());

            Console.Write("Bitte hoehe eingeben: \n");
            int hoehe = int.Parse(Console.ReadLine());


            // Rechteck-Objekt erstellen
            Rechteck meinRechteck = new Rechteck(breite, hoehe);

            // Rechteck zeichnen
            meinRechteck.Zeichnen();


            Console.ReadKey();
        }
    }
}
