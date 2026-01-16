using System;

namespace Lab04_Bruchrechnung
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
            string[] teile = str.Split('/');

            int z = int.Parse(teile[0]);
            int n = int.Parse(teile[1]);

            return new Bruch(z, n);
        }

        public void Kuerzen()
        {
            int kleinster = Math.Min(zaehler, nenner);

            for (int i = kleinster; i > 1; i--)
            {
                if (zaehler % i == 0 && nenner % i == 0)
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
        public void Sub(Bruch b)
        {
            int z = this.zaehler * b.getNenner() - b.getZaehler() * this.nenner;
            int n = this.nenner * b.getNenner();
            this.nenner = n;
            this.zaehler = z;
            Kuerzen();
        }

        public void Mul(Bruch b)
        {
            int z = this.zaehler * b.getZaehler();
            int n = this.nenner * b.getNenner();
            this.nenner = n;
            this.zaehler = z;
            Kuerzen();
        }

        public void Div(Bruch b)
        {
            int z = this.zaehler * b.getNenner();
            int n = this.nenner * b.getZaehler();
            this.nenner = n;
            this.zaehler = z;
            Kuerzen();
        }
    }
}

