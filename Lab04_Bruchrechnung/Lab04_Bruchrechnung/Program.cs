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

            //Eingabe: 67 / 69 + 23 / 45

            Console.Write("Bitte geben sie eine Bruchrechnung ein (z.B. 67/69 + 23/45): ");
            string eingabe = Console.ReadLine();

            Bruchrechnung br =Bruchrechnung.Parse(eingabe);


            Console.WriteLine(br.getResult);
            Console.ReadKey();
        }
    }
}
