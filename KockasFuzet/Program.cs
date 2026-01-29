using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KockasFuzet.Models;
using KockasFuzet.Views;

namespace KockasFuzet
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] menupoints = { "Szolgáltatók kiirása", "Szolgáltató kiirása", "Kilépés" };
            List<Szolgaltato> szolgaltatok = new List<Szolgaltato>()
            {
                new Szolgaltato("T-Home", "Magyar Telekom Nyrt.", "1117 Budapest, Magyar Tudósok körútja 9-11."),
                new Szolgaltato("Vodafone", "Vodafone Magyarország Zrt.", "1095 Budapest, Lechner Ödön fasor 6."),
                new Szolgaltato("Telenor", "Telenor Magyarország Zrt.", "2045 Törökbálint, Törökbálinti út 1-3.")
            };
            bool valasztas = false;
            int mostanipont = 0;
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Főmenü");
                Console.WriteLine("=========");

                for(int i = 0; i < menupoints.Length; i++)
                {
                    if (i == mostanipont)
                    {
                        Console.WriteLine($"> {menupoints[i]}");
                    }
                    else
                    {
                        Console.WriteLine($"{menupoints[i]}");
                    }
                }
                switch(Console.ReadKey(true).Key)
                {
                    case ConsoleKey.E:
                        valasztas = true;
                        break;
                    case ConsoleKey.W:
                        if (mostanipont > 0) mostanipont--;
                        break;
                    case ConsoleKey.S:  
                        if (mostanipont < menupoints.Length - 1) mostanipont++;
                        break;
                    default:
                        Console.Beep();
                        break;
                }
                if (valasztas)
                {
                    switch(mostanipont)
                    {
                        case 0:
                            Console.Clear();
                            SzolgaltatoView.ShowSzolgaltatoList(szolgaltatok);
                            Console.ReadLine();
                            valasztas = false;
                            break;
                        case 1:
                            Console.Clear();
                            new SzolgaltatoView().ShowSzolgaltato(new Szolgaltato("T-Home", "Magyar Telekom Nyrt.", "1117 Budapest, Magyar Tudósok körútja 9-11."));
                            Console.ReadLine();
                            valasztas = false;
                            break;
                        case 2:
                            Environment.Exit(0);
                            break;
                    }
                }
            }
            SzolgaltatoView.ShowSzolgaltatoList(new List<Szolgaltato>());
            new SzolgaltatoView().ShowSzolgaltato(new Szolgaltato("T-Home", "Magyar Telekom Nyrt.", "1117 Budapest, Magyar Tudósok körútja 9-11."));
        }
    }
}
