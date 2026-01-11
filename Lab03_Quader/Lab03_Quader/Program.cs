using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab03_Quader
{
    class Quader
    {
        //Maße in mm speichern
        private double hoehe;
        private double breite;
        private double laenge;

        public Quader() : this(1, 1, 1)
        {
        }

        public Quader(double hoehe, double breite, double laenge)
        {
            this.hoehe = hoehe;
            this.breite = breite;
            this.laenge = laenge;
        }

        public static double ParseValue(string text)
        {
            double value = 0;
            if (text.EndsWith("cm"))
            {
                string valueStr = text.Replace("cm", "");   // "2cm" -> "2"
                value = Double.Parse(valueStr) * 10;
            }
            else if (text.EndsWith("mm"))
            {
                string valueStr = text.Replace("mm", "");   // "20mm" -> "20"
                value = Double.Parse(valueStr);
            }
            return value;
        }
        public static Quader Parse(string text)
        {
            double hoehe = 0;
            double breite = 0;
            double laenge = 0;


            text = text.Replace(" ", "");           //Leerzeichen entfernen --> "2cm; 3cm;5mm" -> "2cm;3cm;5mm"
            string[] parts = text.Split(';');       //Teilen bei Semikolon --> ["2cm", "3cm", "5mm"]

            hoehe = ParseValue(parts[0]);                   //Aufruf der Klassenmethode ParseValue
            breite = ParseValue(parts[1]);
            laenge = ParseValue(parts[2]);
            return new Quader(hoehe, breite, laenge);
        }

        public double GetVolume()
        {
            double volume = hoehe * breite * laenge;
            return volume;
        }

        public void DrawFootprint()
        {
            for(int i = 0; i < laenge; i++)
            {
                Console.Write("*");
            }

            for (int i = 0; i < breite - 2; i++)
            {
                Console.WriteLine();
                Console.Write("*");
                for (int j = 0; j < laenge - 2; j++)
                {
                    Console.Write(" ");
                }
                Console.Write("*");
            }
            Console.WriteLine();
            for (int a = 0; a < laenge; a++)
            {
                Console.Write("*");
            }
            Console.WriteLine();
        }
    }


    internal class Program
    {
        static void Main(string[] args)
        {
            
            Console.WriteLine("Beispiel: 2cm; 3cm; 5mm");
            Console.Write("Bitte geben sie den Quader ein: ");
            string eingabe = Console.ReadLine();
            
            Quader q = Quader.Parse(eingabe);       //Klassenmethode
            //Quader q1 = new Quader();
            //Console.WriteLine(q1.GetHeight());    //Instanzmethode

            //string intStr = "12";
            // int x = int.Parse(intStr);

            q.DrawFootprint();

            Console.WriteLine($"Der Quader hat das Volumen: {q.GetVolume()}mm³");
            

            Random random = new Random();
            List <Quader> quaderListe = new List<Quader>();

            for(int i = 0; i < 10; i++)
            {
                double h = random.Next(10, 21); //10mm bis 20mm
                double b = random.Next(10, 21);
                double l = random.Next(10, 21);
                Quader quader = new Quader(h, b, l);
                quaderListe.Add(quader);

                quaderListe[i].DrawFootprint();
            }

            


            Console.ReadKey();
        }
    }
}
