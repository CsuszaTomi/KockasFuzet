using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KockasFuzet.Models;

namespace KockasFuzet.Views
{
    internal class SzamlaView
    {
        public SzamlaView()
        {
        }

        public void ShowSzamla(Szamla szamla,List<Szolgaltatas> szolgaltatasok)
        {
            Console.WriteLine();
            Text.Write("  #", ConsoleColor.DarkYellow);
            Text.Write(szamla.ID.ToString(), ConsoleColor.Yellow);
            Text.WriteLine(" SZÁMLA RÉSZLETEI", ConsoleColor.White);
            Text.WriteLine("-----------------------------------", ConsoleColor.DarkGray);
            Text.Write("  Kiadó:      ", ConsoleColor.Gray);
            Text.Write(szamla.SzolgaltatoRovidNev, ConsoleColor.Cyan);
            Console.WriteLine();
            Text.Write("  Szolgáltatás:  ", ConsoleColor.Gray);
            foreach (Szolgaltatas szolgaltatas in szolgaltatasok)
            {
                if(szolgaltatas.Azon == szamla.SzolgaltatasAzon)
                {
                    Text.Write(szolgaltatas.Nev, ConsoleColor.DarkCyan);
                    break;
                }
            };
            Console.WriteLine();

            Text.Write("  Időszak:    ", ConsoleColor.Gray);
            Text.Write(szamla.Tol.ToString("yyyy-MM-dd"), ConsoleColor.White);
            Text.Write(" >>> ", ConsoleColor.DarkGray);
            Text.WriteLine(szamla.Ig.ToString("yyyy-MM-dd"), ConsoleColor.White);

            Text.Write("  Összeg:     ", ConsoleColor.Gray);
            Text.WriteLine($"{szamla.Osszeg} Ft", ConsoleColor.Green);

            Text.Write("  Határidő:   ", ConsoleColor.Gray);
            Text.WriteLine(szamla.Hatarido.ToString("yyyy-MM-dd"), ConsoleColor.Red);

            Text.Write("  Állapot:   ", ConsoleColor.Gray);
            if (szamla.Befizetve == "Nincs befizetve" || string.IsNullOrEmpty(szamla.Befizetve))
            {
                Text.WriteLine(" [FÜGGŐBEN]", ConsoleColor.Magenta);
            }
            else
            {
                Text.Write(" [KIFIZETVE] ", ConsoleColor.Green);
                Text.WriteLine($"({szamla.Befizetve})", ConsoleColor.DarkGreen);
            }
            Text.WriteLine("-----------------------------------", ConsoleColor.DarkGray);
            Text.Write("  Megjegyzés: ", ConsoleColor.DarkYellow);
            Console.WriteLine(szamla.Megjegyzes);
            Text.WriteLine("-----------------------------------", ConsoleColor.DarkGray);
        }

        static public void ShowSzamlaList(List<Szamla> szamlaList)
        {
            Text.WriteLine("Számlák", ConsoleColor.Red);
            Text.WriteLine("===================", ConsoleColor.DarkYellow);
            Console.WriteLine("╔════╦═════════════╦══════════╦═══════════════╦═══════════════╦══════════╦══════════════╦═════════════════╦════════════╗");
            Console.WriteLine("║ ID ║ Szolg. Azon ║   Rövid  ║      Tól      ║       Ig      ║  Összeg  ║   Határidő   ║    Befizetve    ║ Megjegyzés ║");
            Console.WriteLine("╠════╬═════════════╬══════════╬═══════════════╬═══════════════╬══════════╬══════════════╬═════════════════╬════════════╣");
            foreach (Szamla szamla in szamlaList)
            {
                string id = szamla.ID.ToString().PadRight(3).Substring(0, 3);
                string azon = szamla.SzolgaltatasAzon.ToString().PadRight(12).Substring(0, 12);
                string rovid = szamla.SzolgaltatoRovidNev.PadRight(9).Substring(0, 9);
                string tol = szamla.Tol.ToShortDateString().PadRight(14).Substring(0, 14);
                string ig = szamla.Ig.ToShortDateString().PadRight(14).Substring(0, 14);
                string osszeg = (szamla.Osszeg + " Ft").PadRight(9).Substring(0, 9);
                string hatarido = szamla.Hatarido.ToShortDateString().PadRight(13).Substring(0, 13);
                string befizetve = (szamla.Befizetve == null ? "Nincs" : szamla.Befizetve).PadRight(16).Substring(0, 16);
                string megj = (szamla.Megjegyzes ?? "").PadRight(11).Substring(0, 11);
                Console.WriteLine($"║ {id}║ {azon}║ {rovid}║ {tol}║ {ig}║ {osszeg}║ {hatarido}║ {befizetve}║ {megj}║");
            }
            Console.WriteLine("╚════╩═════════════╩══════════╩═══════════════╩═══════════════╩══════════╩══════════════╩═════════════════╩════════════╝");
            Text.WriteLine("Enterrel vissza....", ConsoleColor.Yellow);
        }

    }
}
