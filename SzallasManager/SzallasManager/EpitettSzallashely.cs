using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SzallasManager
{
  abstract class EpitettSzallashely : Szallashely
    {
        byte csillagokszama;
        int szobaar;

        

        public byte Csillagokszama 
        {
            get => csillagokszama;
            set
            {
                if (value >= 1 && value <= 5)
                {
                    csillagokszama = value;
                }
                else
                {
                    throw new FormatException("Az csillagok száma 1 és 5 között lehet!");
                }
            }
        }
        public int Szobaar
        { 
            get => szobaar;
            set
            {
                if (value > 0)
                {
                    szobaar = value;
                }
                else
                {
                    throw new FormatException("A szobaár nem lehet 0!");
                }
            }
        }

        public EpitettSzallashely(string azonosito, Cim cim, Szallasfajta szallasfajta, byte csillagokszama, int szobaar) : base(azonosito, cim, szallasfajta)
        {
            Csillagokszama = csillagokszama;
            Szobaar = szobaar;
        }

        public override int Arszamito()
        {
            return szobaar + (szobaar / 10 * csillagokszama);
        }
    }
}
