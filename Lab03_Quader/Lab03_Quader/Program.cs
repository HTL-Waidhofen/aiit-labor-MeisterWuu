using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab03_Quader
{
    class Quader
    {
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
    }


    internal class Program
    {
        static void Main(string[] args)
        {
            Quader q1 = new Quader(3, 2, 1);
            Quader q2 = new Quader();
        }
    }
}
