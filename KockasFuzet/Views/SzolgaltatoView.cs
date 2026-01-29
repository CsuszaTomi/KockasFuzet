using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KockasFuzet.Models;

namespace KockasFuzet.Views
{
    internal class SzolgaltatoView
    {
        public SzolgaltatoView()
        {
        }

        public void ShowSzolgaltato(Szolgaltato szolgaltato)
        {
            Console.WriteLine($"Rövidnév: {szolgaltato.RovidNev}");
            Console.WriteLine($"Név: {szolgaltato.Nev}");
            Console.WriteLine($"Ügyfélszolgálat");
            Console.WriteLine($"Cím: {szolgaltato.UgyfelSzolgalat}");
            Console.WriteLine($"Telefon: Nincs");
        }

        static public void ShowSzolgaltatoList(List<Szolgaltato> szolgaltatoList)
        {
            Console.WriteLine("╔═════════╦════════════════════════════════════════════╦══════════════════════════════════════════════════════════╗");
            Console.WriteLine("║ Rövinév ║                    Név                     ║                   Ügyfélszolgálat                        ║");
            Console.WriteLine("║         ║                                            ║                        Cím                               ║");
            Console.WriteLine("╠═════════╬════════════════════════════════════════════╬══════════════════════════════════════════════════════════╣");
            foreach (Szolgaltato szolgaltato in szolgaltatoList)
            {
                string rovid = szolgaltato.RovidNev.PadRight(7).Substring(0, 7);
                string nev = szolgaltato.Nev.PadRight(42).Substring(0, 42);
                string ugyfel = szolgaltato.UgyfelSzolgalat.PadRight(56).Substring(0, 56);
                Console.WriteLine($"║ {rovid} ║ {nev} ║ {ugyfel} ║");
            }
            Console.WriteLine("╚═════════╩════════════════════════════════════════════╩══════════════════════════════════════════════════════════╝");
        }
    }
}
