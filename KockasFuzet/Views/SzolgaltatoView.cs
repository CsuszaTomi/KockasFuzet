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
            //120x30 a méret
            Console.WriteLine("╔═════════╦════════════════════════════════════════╦══════════════════════════════════════════════════════════╗");
            Console.WriteLine("║ Rövinév ║                Név                     ║                   Ügyfélszolgálat                        ║");
            Console.WriteLine("║         ║                                        ║         Cím                 ║           Telefon          ║");
            Console.WriteLine("╠═════════╬════════════════════════════════════════╬══════════════════════════════════════════════════════════╣");
            foreach (Szolgaltato szolgaltato in szolgaltatoList)
            {
                Console.WriteLine($"║ {szolgaltato.RovidNev,-7} ║ {szolgaltato.Nev,-38} ║ {szolgaltato.UgyfelSzolgalat,-56} ║");
            }   
            Console.WriteLine("╚═════════╩════════════════════════════════════════╩═══════════════════════════════════════════════════════════╝");
        }
    }
}
