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

        public void ShowSzamla(Szamla szamla)
        {
            Console.WriteLine($"ID: {szamla.ID}");
            Console.WriteLine($"Szolgáltatás azonosító: {szamla.SzolgaltatasAzon}");
            Console.WriteLine($"Szolgáltatás rövid neve: {szamla.SzolgaltatoRovidNev}");
            Console.WriteLine($"Tol: {szamla.Tol.ToShortDateString()}");
            Console.WriteLine($"Ig: {szamla.Ig.ToShortDateString()}");
            Console.WriteLine($"Összeg: {szamla.Osszeg} Ft");
            Console.WriteLine($"Határidő: {szamla.Hatarido.ToShortDateString()}");
            Console.WriteLine($"Befizetve: {(szamla.Befizetve == null ? "Nincs befizetve" : szamla.Befizetve)}");
            Console.WriteLine($"Megjegyzés: {szamla.Megjegyzes}");
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
