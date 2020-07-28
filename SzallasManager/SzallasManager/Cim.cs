using System;

namespace SzallasManager
{
    struct Cim
    {
        short irsz;
        string varos;
        string utca;
        short hsz;

        public short Irsz 
        {
            get => irsz;
            set
            {
                if (value >= 1000 && value <= 9999)
                {
                    irsz = value;
                }
                else
                {
                    throw new FormatException("Azirsz 100 és 9999 között lehet!");
                }
            }
        }
        public string Varos
        { 
            get => varos;
            set
            {
                if (value.Length != 0)
                {
                    varos = value;
                }
                else
                {
                    throw new FormatException("A város nem lehet üres!");
                }
            }
        }
        public string Utca 
        { 
            get => utca;
            set
            {
                if (value.Length == 8)
                {
                    utca = value;
                }
                else
                {
                    throw new FormatException("Az utca nem lehet üres!");
                }
            }
        }
        public short Hsz 
        {
            get => hsz;
            set
            {
                if (value > 0)
                {
                    hsz = value;
                }
                else
                {
                    throw new FormatException("Az utca nem lehet üres!");
                }
            }
        }





    }
}