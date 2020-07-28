using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SzallasManager
{
    class Panzio :EpitettSzallashely
    {
        bool vanreggeli;

        public Panzio(string azonosito, Cim cim, Szallasfajta szallasfajta, byte csillagokszama, int szobaar, bool vanreggeli) : base(azonosito, cim, szallasfajta, csillagokszama, szobaar)
        {
            Vanreggeli = vanreggeli;
        }

        public bool Vanreggeli { get => vanreggeli; set => vanreggeli = value; }



    }
}
