using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SzallasManager
{
    class Szalloda :EpitettSzallashely
    { 
        bool vanwellness;

      

        public bool Vanwellness { get => vanwellness; set => vanwellness = value; }

        public Szalloda(string azonosito, Cim cim, Szallasfajta szallasfajta, byte csillagokszama, int szobaar, bool vanwellness) : base(azonosito, cim, szallasfajta, csillagokszama, szobaar)
        {
            Vanwellness = vanwellness;
        }


    }
}
