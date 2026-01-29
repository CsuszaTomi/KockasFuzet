using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KockasFuzet.Models;

namespace KockasFuzet.Models
{
    internal class Szolgaltatas
    {
        int azon = 0;
        string nev = "";

        public Szolgaltatas(int azon, string nev)
        {
            Azon = azon;
            Nev = nev;
        }

        public Szolgaltatas()
        {
        }
        public int Azon { get => azon; set => azon = value; }
        public string Nev { get => nev; set => nev = value; }

        public override string ToString()
        {
            return $"Azon: {Azon},Név: {Nev}";
        }
    }
}
