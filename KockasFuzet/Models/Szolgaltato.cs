using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using KockasFuzet.Models;

namespace KockasFuzet.Models
{
    internal class Szolgaltato
    {
        string rovidNev = "";
        string nev = "";
        string ugyfelSzolgalat = "";

        public Szolgaltato(string rovidNev, string nev, string ugyfelSzolgalat)
        {
            RovidNev = rovidNev;
            Nev = nev;
            UgyfelSzolgalat = ugyfelSzolgalat;
        }

        public Szolgaltato()
        {
        }

        public string RovidNev { get => rovidNev; set => rovidNev = value; }
        public string Nev { get => nev; set => nev = value; }
        public string UgyfelSzolgalat { get => ugyfelSzolgalat; set => ugyfelSzolgalat = value; }

        public override string ToString()
        {
            return $"RövidNév: {RovidNev},Név: {Nev},Ügyfélszolgálat: {UgyfelSzolgalat}";
        }
    }
}
