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
            Text.Write("  #", ConsoleColor.DarkYellow);
            Text.Write(szolgaltato.RovidNev.ToString(), ConsoleColor.Yellow);
            Text.WriteLine(" SZOLGÁLTATÓ RÉSZLETEI", ConsoleColor.White);
            Text.WriteLine("-----------------------------------", ConsoleColor.DarkGray);
            Text.Write("  Név:      ", ConsoleColor.Gray);
            Text.Write(szolgaltato.Nev, ConsoleColor.Cyan);
            Console.WriteLine();

            Text.WriteLine("  Ügyfélszolgálat:    ", ConsoleColor.Gray);
            Text.Write("    Cím:    ", ConsoleColor.Gray);
            Text.WriteLine(szolgaltato.UgyfelSzolgalat, ConsoleColor.White);
            Text.Write("    Telefon:   Nincs", ConsoleColor.Gray);
            Console.WriteLine();
            Text.WriteLine("-----------------------------------", ConsoleColor.DarkGray);
        }

        static public void ShowSzolgaltatoList(List<Szolgaltato> szolgaltatoList)
        {
            Text.WriteLine("Szolgáltatók", ConsoleColor.Red);
            Text.WriteLine("===================", ConsoleColor.DarkYellow);
            Console.WriteLine("╔══════════╦════════════════════════════════════════════╦══════════════════════════════════════════════════════════╗");
            Console.WriteLine("║ Rövidnév ║                    Név                     ║                   Ügyfélszolgálat                        ║");
            Console.WriteLine("║          ║                                            ║                        Cím                               ║");
            Console.WriteLine("╠══════════╬════════════════════════════════════════════╬══════════════════════════════════════════════════════════╣");
            foreach (Szolgaltato szolgaltato in szolgaltatoList)
            {
                string rovid = szolgaltato.RovidNev.PadRight(8).Substring(0, 8);
                string nev = szolgaltato.Nev.PadRight(42).Substring(0, 42);
                string ugyfel = szolgaltato.UgyfelSzolgalat.PadRight(56).Substring(0, 56);
                Console.WriteLine($"║ {rovid} ║ {nev} ║ {ugyfel} ║");
            }
            Console.WriteLine("╚══════════╩════════════════════════════════════════════╩══════════════════════════════════════════════════════════╝");
            Text.WriteLine("Enterrel vissza....", ConsoleColor.Yellow);
        }
    }
}
