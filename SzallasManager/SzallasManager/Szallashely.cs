using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SzallasManager
{
    enum Szallasfajta
    {
        Üzleti,
        Gyógy,
        Sport,
        Egyéb
    }
    abstract class Szallashely
    {
        Cim cim;
        string azonosito;
        Szallasfajta szallasfajta;

        public string Azonosito
        {
            get => azonosito;
            set
            {
                if (value.Length == 8)
                {
                    azonosito = value;
                }
                else
                {
                    throw new FormatException("Az azonosító 8 karakter hossszú!");
                }
            }
        }

        internal Cim Cim
        {
            get => cim;
            set => cim = value;
        }
        internal Szallasfajta Szallasfajta { get => szallasfajta; /*set => szallasfajta = value; */}



        public override string ToString()
        {
            return $"{azonosito}";
        }




    }


}
