using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab04_Bruchrechnung
{
    internal class Bruchrechnung
    {
        Bruch b1;
        Bruch b2;
        public Bruchrechnung(Bruch b1, Bruch b2) 
        { 
            this.b1 = b1;
            this.b2 = b2;
        }

        public static Bruchrechnung Parse(string str)
        {
            //Eingabe: 67 / 69 + 23 / 45
            string[] brueche = str.Split('+', '-', '*', ':');
            Bruch b1 = Bruch.Parse(brueche[0]);
            string op = brueche[1];
            Bruch b2 = Bruch.Parse(brueche[2]);
            return new Bruchrechnung(b1, b2);
        }
    }
}
