using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KockasFuzet.Models;
using MySql.Data.MySqlClient;
using KockasFuzet.Views;

namespace KockasFuzet.Controller
{
    internal class SzolgaltatasController
    {
        static public List<Szolgaltatas> GetSzolgaltatasList()
        {
            List<Szolgaltatas> szolgaltatasList = new List<Szolgaltatas>();
            MySqlConnection connection = new MySqlConnection();
            string connectionString = "SERVER = localhost;DATABASE=kockasfuzet;UID=root;PASSWORD=;";
            connection.ConnectionString = connectionString;
            connection.Open();
            string sql = "SELECT * FROM szolgaltatas;";
            MySqlCommand command = new MySqlCommand(sql, connection);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Szolgaltatas szolgaltatas = new Szolgaltatas()
                {
                    Azon = reader.GetInt32("Azon"),
                    Nev = reader.GetString("Nev"),
                };
                szolgaltatasList.Add(szolgaltatas);
            }
            connection.Close();
            return szolgaltatasList;
        }

        static public Szolgaltatas GetSzolgaltatasOBJ(int azon, List<Szolgaltatas> szolgaltatasok)
        {
            foreach (Szolgaltatas szolgaltatas in szolgaltatasok)
            {
                if (szolgaltatas.Azon == azon)
                {
                    return szolgaltatas;
                }
            }
            return null;
        }

        /// <summary>
        /// Ez a függvény elrendezi a szolgáltatások azonosítóját hogy ne legyen üres id(pl ne legyen 1,2,3,5,6 hanem 1,2,3,4,5) és visszaadja a rendezett listát
        /// </summary>
        /// <param name="szolgaltatasok">A szolgáltatásokat tartalmazó lista</param>
        /// <returns>Az rendezett szolgáltatások listáját</returns>
        public static List<Szolgaltatas> SzolgaltatasAzonNormalizer(List<Szolgaltatas> szolgaltatasok)
        {
            List<Szolgaltatas> rendezettAzonok = szolgaltatasok.OrderBy(szamla => szamla.Azon).ToList();
            for (int i = 0; i < rendezettAzonok.Count; i++)
            {
                rendezettAzonok[i].Azon = i + 1;
            }
            return rendezettAzonok;
        }

        /// <summary>
        /// A függvény frissíti a szolgáltatások azon-ját az adatbázisban a rendezett lista alapján, hogy ne legyen üres id(pl ne legyen 1,2,3,5,6 hanem 1,2,3,4,5) és visszaadja a rendezett listát
        /// </summary>
        /// <param name="szolgaltatasok">A szolgáltatásokat listája</param>
        /// <returns>A rendezett szolgáltatások listája</returns>
        public static List<Szolgaltatas> SzolgaltatasAzonUpdater(List<Szolgaltatas> szolgaltatasok)
        {
            MySqlConnection connection = new MySqlConnection();
            string connectionString = "SERVER = localhost;DATABASE=kockasfuzet;UID=root;PASSWORD=;";
            connection.ConnectionString = connectionString;
            connection.Open();
            List<Szolgaltatas> regiszolgaltatok = GetSzolgaltatasList();
            List<Szolgaltatas> rendezettszamla = SzolgaltatasAzonNormalizer(szolgaltatasok);
            for (int i = 0; i < regiszolgaltatok.Count; i++)
            {
                string sql = "UPDATE `szolgaltatas` SET `Azon`=@ujAzon WHERE Azon = @Azon";
                MySqlCommand command = new MySqlCommand(sql, connection);
                command.Parameters.AddWithValue("@ujAzon", rendezettszamla[i].Azon);
                command.Parameters.AddWithValue("@Azon", regiszolgaltatok[i].Azon);
                command.ExecuteNonQuery();
            }
            return rendezettszamla;
        }

        static public void AddSzolgaltatas(List<Szolgaltatas> szolgaltatasok)
        {
            Console.Clear();
            Text.WriteLine("Új szolgáltatás hozzáadása", ConsoleColor.Red);
            Text.WriteLine("=========================", ConsoleColor.DarkYellow);
            Console.Write("Azonosító: ");
            string azonosito = Console.ReadLine();
            if (azonosito == "")
                return;
            else
            {
                bool azonMegfelelo = false;

                while (!azonMegfelelo)
                {
                    if (!Checker.IntChecker(azonosito))
                    {
                        Text.WriteLine("Az ID csak szám lehet!", ConsoleColor.DarkRed);
                    }
                    else if (int.Parse(azonosito) > szolgaltatasok.Count + 1)
                    {
                        Text.WriteLine($"Az ID nem lehet nagyobb, mint {szolgaltatasok.Count + 1}!", ConsoleColor.DarkRed);
                    }
                    else if (szolgaltatasok.Any(azon => azon.Azon == int.Parse(azonosito)))
                    {
                        Text.WriteLine("Már van ilyen azonosítójú szolgáltatás!", ConsoleColor.DarkRed);
                    }
                    else
                    {
                        azonMegfelelo = true;
                        continue;
                    }

                    Console.Write("Azonosító: ");
                    azonosito = Console.ReadLine();
                    if (azonosito == "")
                        return;
                }
            }
            Console.Write("Név: ");
            string nev = Console.ReadLine();
            if (nev == "")
                return;
            MySqlConnection connection = new MySqlConnection();
            string connectionString = "SERVER = localhost;DATABASE=kockasfuzet;UID=root;PASSWORD=;";
            connection.ConnectionString = connectionString;
            connection.Open();
            string sql = "INSERT INTO szolgaltatas (Azon, Nev) VALUES (@azon, @nev);";
            MySqlCommand command = new MySqlCommand(sql, connection);
            command.Parameters.AddWithValue("@azon", azonosito);
            command.Parameters.AddWithValue("@nev", nev);
            int sorok = command.ExecuteNonQuery();
            connection.Close();
            string valasz = sorok > 0 ? "Sikeres hozzáadás!" : "Sikertelen hozzáadás!";
            Text.WriteLine(valasz, ConsoleColor.Cyan);
            Text.WriteLine("Enterrel vissza...", ConsoleColor.Yellow);
            Console.ReadLine();
        }

        static public void RemoveSzolgaltatas(List<Szolgaltatas> szolgaltatasok)
        {
            Console.Clear();
            Text.WriteLine("Szolgáltatás törlés", ConsoleColor.Red);
            Text.WriteLine("=========================", ConsoleColor.DarkYellow);
            Console.Write("Add meg a törlendő szolgáltatás azonosítóját: ");
            string azon = Console.ReadLine();
            if (azon == "")
                return;
            int szamlalo = 0;
            foreach (Szolgaltatas szolgaltatas in szolgaltatasok)
            {
                if (szolgaltatas.Azon == int.Parse(azon))
                {
                    MySqlConnection connection = new MySqlConnection();
                    string connectionString = "SERVER = localhost;DATABASE=kockasfuzet;UID=root;PASSWORD=;";
                    connection.ConnectionString = connectionString;
                    connection.Open();
                    string deletesql = $"DELETE FROM `szolgaltatas` WHERE Azon = @azon";
                    MySqlCommand deletecmd = new MySqlCommand(deletesql, connection);
                    deletecmd.Parameters.AddWithValue("@azon", azon);
                    int sorok = deletecmd.ExecuteNonQuery();
                    connection.Close();
                    string valasz = sorok > 0 ? "Sikeres törlés!" : "Sikertelen törlés!";
                    Text.WriteLine(valasz, ConsoleColor.Cyan);
                }
                else
                {
                    szamlalo++;
                    if (szamlalo == szolgaltatasok.Count)
                    {
                        Text.WriteLine("Nincs ilyen szolgáltató!", ConsoleColor.DarkRed);
                    }
                }
            }
            Text.WriteLine("Enterrel vissza...", ConsoleColor.Yellow);
            Console.ReadLine();
        }

        static public void ModifySzolgaltatas(List<Szolgaltatas> szolgaltatasok)
        {
            Console.Clear();
            Text.WriteLine("Szolgáltatás módosítás", ConsoleColor.Red);
            Text.WriteLine("=========================", ConsoleColor.DarkYellow);
            Console.Write("Add meg a módosítandó szolgáltatás azonosítóját: ");
            string azon = Console.ReadLine();
            if (azon == "")
                return;
            int szamlalo = 0;
            bool modosit = false;
            Szolgaltatas modositando = new Szolgaltatas();
            foreach (Szolgaltatas szolgaltatas in szolgaltatasok)
            {
                if (szolgaltatas.Azon == int.Parse(azon))
                {
                    modosit = true;
                    modositando = szolgaltatas;
                    break;
                }
                else
                {
                    szamlalo++;
                    if (szamlalo == szolgaltatasok.Count)
                    {
                        Text.WriteLine("Nincs ilyen szolgáltató!", ConsoleColor.DarkRed);
                    }
                }
            }
            if (modosit)
            {
                Text.WriteLine("Ha valamit nem akar módosítani nyomjon entert!", ConsoleColor.Yellow);
                Text.WriteWithInfo("Azonosító", modositando.Azon.ToString(), ": ", ConsoleColor.White, ConsoleColor.Cyan);
                string ujazon = Console.ReadLine();
                if(ujazon != "")
                {
                    while (Checker.IntChecker(ujazon))
                    {
                        Console.WriteLine("Az azonosító csak szám lehet!");
                        Console.Write("Azonosító: ");
                        ujazon = Console.ReadLine();
                        if (ujazon == "")
                            return;
                    }
                    while (Checker.LenghtChecker(ujazon, 0, 11))
                    {
                        Console.WriteLine("Az azonosító maximum 11 karakter lehet!");
                        Console.Write("Azonosító: ");
                        ujazon = Console.ReadLine();
                        if (ujazon == "")
                            return;
                    }
                }
                Text.WriteWithInfo("Név", modositando.Nev, ": ", ConsoleColor.White, ConsoleColor.Cyan);
                string ujnev = Console.ReadLine();
                MySqlConnection connection = new MySqlConnection();
                string connectionString = "SERVER = localhost;DATABASE=kockasfuzet;UID=root;PASSWORD=;";
                connection.ConnectionString = connectionString;
                connection.Open();
                string deletesql = $"UPDATE `szolgaltatas` SET `Azon`=@azon,`Nev`=@nev WHERE Azon = @originalAzon";
                MySqlCommand deletecmd = new MySqlCommand(deletesql, connection);
                deletecmd.Parameters.AddWithValue("@originalAzon", modositando.Azon);
                if (ujazon != "")
                    deletecmd.Parameters.AddWithValue("@azon", ujazon);
                else
                    deletecmd.Parameters.AddWithValue("@azon", modositando.Azon);
                if (ujnev != "")
                    deletecmd.Parameters.AddWithValue("@nev", ujnev);
                else
                    deletecmd.Parameters.AddWithValue("@nev", modositando.Nev);
                int sorok = deletecmd.ExecuteNonQuery();
                connection.Close();
                string valasz = sorok > 0 ? "Sikeres módosítás!":"Sikertelen módosítás!";
                Text.WriteLine(valasz, ConsoleColor.Cyan);
            }
            Text.WriteLine("Enterrel vissza...", ConsoleColor.Yellow);
            Console.ReadLine();
        }
    }
}
