using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab04_Bruchrechnung
{
    internal partial class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Bruchrechnung in C#");

            //Eingabe: 67 / 69
            Console.WriteLine("Bitte 1. Bruch eingeben (Zähler / Nenner): ");
            string eingabe1 = Console.ReadLine();
            Console.WriteLine("Bitte 2. Bruch eingeben (Zähler / Nenner): ");
            string eingabe2 = Console.ReadLine();

            Bruch b1 = Bruch.Parse(eingabe1);
            Bruch b2 = Bruch.Parse(eingabe2);

            b1.Add(b2);

            Console.WriteLine(b1);

            ;

            Console.ReadKey();
        }
    }
}
