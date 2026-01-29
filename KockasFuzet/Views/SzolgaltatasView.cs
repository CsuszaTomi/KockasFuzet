using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KockasFuzet.Models;

namespace KockasFuzet.Views
{
    internal class SzolgaltatasView
    {
        public SzolgaltatasView()
        {
        }

        public void ShowSzolgaltatas(Szolgaltatas szolgaltatas)
        {
            Console.WriteLine($"Azonosító: {szolgaltatas.Azon}");
            Console.WriteLine($"Név: {szolgaltatas.Nev}");
        }

        static public void ShowSzolgaltatasList(List<Szolgaltatas> szolgaltatasList)
        {
            Text.WriteLine("Szolgáltatások",ConsoleColor.Red);
            Text.WriteLine("===================", ConsoleColor.DarkYellow);
            Console.WriteLine("╔════╦══════════════════════╗");
            Console.WriteLine("║Azon║          Név         ║");
            Console.WriteLine("╠════╬══════════════════════╣");
            foreach (Szolgaltatas szolgaltatas in szolgaltatasList)
            {
                string id = szolgaltatas.Azon.ToString().PadRight(2).Substring(0, 2);
                string nev = szolgaltatas.Nev.PadRight(20).Substring(0, 20);
                Console.WriteLine($"║ {id} ║ {nev} ║");
            }
            Console.WriteLine("╚════╩══════════════════════╝");
            Text.WriteLine("Enterrel vissza....", ConsoleColor.Yellow);
        }
    }
}
