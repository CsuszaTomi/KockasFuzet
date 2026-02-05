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
            string[] menupoints = { "Szolgáltató kezelés", "Szolgáltatás kezelés","Számla kezelés", "Kilépés" };

            while (true)
            {
                List<Szolgaltato> szolgaltatok = SzolgaltatoController.GetSzolgaltatoList();
                List<Szolgaltatas> szolgaltatasok = SzolgaltatasController.GetSzolgaltatasList();
                List<Szamla> szamlak = SzamlaController.GetSzamlaList();
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
                                Console.Clear();
                                SzolgaltatoController.AddSzolgaltato();
                                break;
                            case 3:
                                Console.Clear();
                                SzolgaltatoController.ModifySzolgaltato(szolgaltatok);
                                break;
                            case 4:
                                Console.Clear();
                                SzolgaltatoController.RemoveSzolgaltato(szolgaltatok);
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
                                Console.Clear();
                                Text.WriteLine("Szolgáltatás keresés", ConsoleColor.Red);
                                Text.WriteLine("=====================", ConsoleColor.DarkYellow);
                                Console.Write("Add meg a szolgáltatás azonosítóját:");
                                Szolgaltatas kivalasztott = SzolgaltatasController.GetSzolgaltatasOBJ(Convert.ToInt32(Console.ReadLine()), szolgaltatasok);
                                if (kivalasztott != null)
                                {
                                    new SzolgaltatasView().ShowSzolgaltatas(kivalasztott);
                                }
                                else
                                {
                                    Text.WriteLine("Nincs ilyen szolgáltatás!", ConsoleColor.Yellow);
                                }
                                Console.ReadLine();
                                break;
                            case 2:
                                Console.Clear();
                                SzolgaltatasController.AddSzolgaltatas();
                                break;
                            case 3:
                                Console.Clear();
                                SzolgaltatasController.ModifySzolgaltatas(szolgaltatasok);
                                break;
                            case 4:
                                Console.Clear();
                                SzolgaltatasController.RemoveSzolgaltatas(szolgaltatasok);
                                break;
                            case 5:
                                break;
                        }
                        break;
                    case 2:
                        Console.Clear();
                        int szamlachoice = Text.ArrowMenu("Számla kezelés", new string[] { "Számlák listázása", "Számla kiirása", "Számla hozzáadása", "Számla módosítása", "Számla törlése", "Vissza" });
                        switch (szamlachoice)
                        {
                            case 0:
                                Console.Clear();
                                SzamlaView.ShowSzamlaList(szamlak);
                                Console.ReadLine();
                                break;
                            case 1:
                                Console.Clear();
                                Text.WriteLine("Számla keresés", ConsoleColor.Red);
                                Text.WriteLine("=====================", ConsoleColor.DarkYellow);
                                Console.Write("Add meg a számla azonosítóját:");
                                Szamla kivalasztott = SzamlaController.GetSzamlaOBJ(Convert.ToInt32(Console.ReadLine()), szamlak);
                                if (kivalasztott != null)
                                {
                                    new SzamlaView().ShowSzamla(kivalasztott);
                                }
                                else
                                {
                                    Text.WriteLine("Nincs ilyen számla!", ConsoleColor.Yellow);
                                }
                                Console.ReadLine();
                                break;
                            case 2:
                                Console.Clear();
                                SzamlaController.AddSzamla();
                                break;
                            case 3:
                                Console.Clear();
                                SzamlaController.ModifySzamla(szamlak);
                                break;
                            case 4:
                                Console.Clear();
                                SzamlaController.RemoveSzamla(szamlak);
                                break;
                            case 5:
                                break;
                        }
                        break;
                    case 3:
                        Environment.Exit(0);
                        break;
                }
            }
        }
    }
}
