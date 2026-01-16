using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab04_Bruchrechnung
{
    internal class Program
    {
        class Bruch
        {
            private int zaehler;
            private int nenner;
            public Bruch(int zaehler, int nenner)
            {
                this.zaehler = zaehler;
                this.nenner = nenner;
            }

            public int getZaehler()
            {
                return zaehler;
            }

            public void setZaehler(int zaehler)
            {
                this.zaehler = zaehler;
                
            }

            public int getNenner()
            {
                return nenner;
            }
            public void setNenner(int nenner)
            {
                if (nenner != 0)
                { 
                    this.nenner = nenner;
                }
                else Console.WriteLine("Nenner darf nicht 0 sein!");
            }
            public override string ToString()
            {
                return $"{zaehler}/{nenner}";
            }

            public static Bruch Parse(string str)
            { 
                string [] teile = str.Split('/');

                int z = int.Parse(teile[0]);
                int n = int.Parse(teile[1]);

                return new Bruch(z, n);
            }

            public void Kuerzen()
            {
                int kleinster = Math.Min(zaehler, nenner);

                for(int i = kleinster; i > 1; i--)
                {
                    if(zaehler % i == 0 && nenner % i == 0)
                    {
                        nenner /= i;
                        zaehler /= i;
                    }
                }
            }

            public void Add(Bruch b)
            {
                int z = this.zaehler * b.getNenner() + b.getZaehler() * this.nenner;
                int n = this.nenner * b.getNenner();
                
                this.nenner = n;
                this.zaehler = z;

                Kuerzen();


            }
        }
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
