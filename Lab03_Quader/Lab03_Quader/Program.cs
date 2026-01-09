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

            if (parts[0].EndsWith("cm"))            //hoehe
            {
                string hoeheStr = parts[0].Replace("cm", "");   // "2cm" -> "2"
                hoehe =  Double.Parse(hoeheStr) * 10;
            }
            else if (parts[0].EndsWith("mm"))
            {
                string hoeheStr = parts[0].Replace("mm", "");   // "20mm" -> "20"
                hoehe = Double.Parse(hoeheStr);
            }

            ParseValue(parts[0]);                   //Aufruf der Klassenmethode ParseValue
            return new Quader(hoehe, breite, laenge);
        }

        public double GetVolume()
        {
            return hoehe * breite * laenge;
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

            Console.WriteLine($"Der Quader hat das Volumen: {q.GetVolume()}mm³");

            //Quader q1 = new Quader();
            //Console.WriteLine(q1.GetHeight());    //Instanzmethode

            //string intStr = "12";
            // int x = int.Parse(intStr);

            Console.ReadKey();
        }
    }
}
