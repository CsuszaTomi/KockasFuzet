using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KockasFuzet.Controller;
using KockasFuzet.Models;
using KockasFuzet.Views;

namespace KockasFuzet
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] menupoints = { "Szolgáltató kezelés", "Szolgáltatás kezelés", "Kilépés" };

            while (true)
            {
                List<Szolgaltato> szolgaltatok = SzolgaltatoController.GetSzolgaltatoList();
                List<Szolgaltatas> szolgaltatasok = SzolgaltatasController.GetSzolgaltatasList();
                int fomenuchoice = Text.ArrowMenu("Főmenü", menupoints);
                switch (fomenuchoice)
                {
                    case 0:
                        Console.Clear();
                        int szolgaltatochoice = Text.ArrowMenu("Szolgáltató kezelés", new string[] { "Szolgáltatók listázása","Szolgáltató kiirása", "Szolgáltató hozzáadása", "Szolgáltató módosítása", "Szolgáltató törlése", "Vissza" });
                        switch(szolgaltatochoice)
                        {
                            case 0:
                                Console.Clear();
                                SzolgaltatoView.ShowSzolgaltatoList(szolgaltatok);
                                Console.ReadLine();
                                break;
                            case 1:
                                Console.Clear();
                                Text.WriteLine("Szolgáltató keresés", ConsoleColor.Red);
                                Text.WriteLine("=====================",ConsoleColor.DarkYellow);
                                Console.WriteLine("Add meg a szolgáltató rövidnevét:");
                                Szolgaltato kivalasztott = SzolgaltatoController.GetSzolgaltatoOBJ(Console.ReadLine(),szolgaltatok);
                                if (kivalasztott != null)
                                {
                                    new SzolgaltatoView().ShowSzolgaltato(kivalasztott);
                                }
                                else
                                {
                                    Text.WriteLine("Nincs ilyen szolgáltató!", ConsoleColor.Yellow);
                                }
                                Console.ReadLine();
                                break;
                            case 2:
                                break;
                            case 3:
                                break;
                            case 4:
                                break;
                            case 5:
                                break;
                        }
                        break;
                    case 1:
                        Console.Clear();
                        int szolgaltataschoice = Text.ArrowMenu("Szolgáltatás kezelés", new string[] { "Szolgáltatások listázása", "Szolgáltatás kiirása", "Szolgáltatás hozzáadása", "Szolgáltatás módosítása", "Szolgáltatás törlése", "Vissza" });
                        switch (szolgaltataschoice)
                        {
                            case 0:
                                Console.Clear();
                                SzolgaltatasView.ShowSzolgaltatasList(szolgaltatasok);
                                Console.ReadLine();
                                break;
                            case 1:
                                break;
                            case 2:
                                break;
                            case 3:
                                break;
                            case 4:
                                break;
                            case 5:
                                break;
                        }
                        break;
                    case 2:
                        Environment.Exit(0);
                        break;
                }
            }
        }
    }
}
