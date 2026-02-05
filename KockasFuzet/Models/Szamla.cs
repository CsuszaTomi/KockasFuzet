using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KockasFuzet.Models;

namespace KockasFuzet.Models
{
    internal class Szamla
    {
        int id = 0;
        int szolgaltatasAzon = 0;
        string szolgaltatoRovidNev = "";
        DateTime tol = DateTime.MinValue;
        DateTime ig = DateTime.MinValue;
        int osszeg = 0;
        DateTime hatarido = DateTime.MinValue;
        string befizetve = "";
        string megjegyzes = "";

        public Szamla(int iD, int szolgaltatasAzon, string szolgaltatoRovidNev, DateTime tol, DateTime ig, int osszeg, DateTime hatarido, string befizetve, string megjegyzes)
        {
            ID = iD;
            SzolgaltatasAzon = szolgaltatasAzon;
            SzolgaltatoRovidNev = szolgaltatoRovidNev;
            Tol = tol;
            Ig = ig;
            Osszeg = osszeg;
            Hatarido = hatarido;
            Befizetve = befizetve;
            Megjegyzes = megjegyzes;
        }

        public Szamla()
        {
        }

        public int ID { get => id; set => id = value; }
        public int SzolgaltatasAzon { get => szolgaltatasAzon; set => szolgaltatasAzon = value; }
        public string SzolgaltatoRovidNev { get => szolgaltatoRovidNev; set => szolgaltatoRovidNev = value; }
        public DateTime Tol { get => tol; set => tol = value; }
        public DateTime Ig { get => ig; set => ig = value; }
        public int Osszeg { get => osszeg; set => osszeg = value; }
        public DateTime Hatarido { get => hatarido; set => hatarido = value; }
        public string Befizetve { get => befizetve; set => befizetve = value; }
        public string Megjegyzes { get => megjegyzes; set => megjegyzes = value; }
    }
}
