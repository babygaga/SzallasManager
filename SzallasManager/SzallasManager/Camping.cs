using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SzallasManager
{
    class Camping : Szallashely
    {
        bool vizparti;

        public Camping(string azonosito, Cim cim, Szallasfajta szallasfajta, bool vizparti) : base(azonosito, cim, szallasfajta)
        {
            Vizparti = vizparti;
        }

        public bool Vizparti { get => vizparti; set => vizparti = value; }

        public override int Arszamito()
        {
           return (vizparti) ? 5000 :  3000;
        }
    }
}
