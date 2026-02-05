using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KockasFuzet.Models;
using MySql.Data.MySqlClient;
using KockasFuzet.Views;
using Org.BouncyCastle.Crypto.Engines;

namespace KockasFuzet.Controller
{
    internal class SzamlaController
    {
        static public List<Szamla> GetSzamlaList()
        {
            List<Szamla> szamlaList = new List<Szamla>();
            MySqlConnection connection = new MySqlConnection();
            string connectionString = "SERVER=localhost;DATABASE=kockasfuzet;UID=root;PASSWORD=;Convert Zero Datetime=True;";
            connection.ConnectionString = connectionString;
            connection.Open();
            string sql = "SELECT * FROM számla;";
            MySqlCommand command = new MySqlCommand(sql, connection);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Szamla szamla = new Szamla();
                szamla.ID = reader.GetInt32("ID");
                szamla.SzolgaltatasAzon = reader.GetInt32("SzolgaltatasAzon");
                szamla.SzolgaltatoRovidNev = reader.GetString("SzolgaltatoRovid");
                szamla.Tol = reader.GetDateTime("Tol");
                szamla.Ig = reader.GetDateTime("Ig");
                szamla.Osszeg = reader.GetInt32("Osszeg");
                szamla.Hatarido = reader.GetDateTime("Hatarido");
                int befIndex = reader.GetOrdinal("Befizetve");
                if (!reader.IsDBNull(befIndex))
                {
                    DateTime befDatum = reader.GetDateTime(befIndex);
                    if (befDatum == DateTime.MinValue)
                    {
                        szamla.Befizetve = "Nincs befizetve";
                    }
                    else
                    {
                        szamla.Befizetve = befDatum.ToString("yyyy-MM-dd");
                    }
                }
                else
                {
                    szamla.Befizetve = "Nincs befizetve";
                }
                szamla.Megjegyzes = reader.IsDBNull(reader.GetOrdinal("Megjegyzes")) ? "" : reader.GetString("Megjegyzes");
                szamlaList.Add(szamla);
            }
            connection.Close();
            return szamlaList;
        }

        /// <summary>
        /// Ez a függvény elrendezi a számlák idjét hogy ne legyen üres id(pl ne legyen 1,2,3,5,6 hanem 1,2,3,4,5) és visszaadja a rendezett listát
        /// </summary>
        /// <param name="szamlak">A számlákat tartalmazó lista</param>
        /// <returns>Az rendezett számlák listáját</returns>
        public static List<Szamla> SzamlaIDNormalizer(List<Szamla> szamlak)
        {
            List<Szamla> rendezettSzamlak = szamlak.OrderBy(szamla => szamla.ID).ToList();
            for (int i = 0; i < rendezettSzamlak.Count; i++)
            {
                rendezettSzamlak[i].ID = i + 1;
            }
            return rendezettSzamlak;
        }

        /// <summary>
        /// A függvény frissíti a számlák id-jét az adatbázisban a rendezett lista alapján, hogy ne legyen üres id(pl ne legyen 1,2,3,5,6 hanem 1,2,3,4,5) és visszaadja a rendezett listát
        /// </summary>
        /// <param name="szamlak">A számlák listája</param>
        /// <returns>A rendezett számlák listája</returns>
        public static List<Szamla> SzamlaIdUpdater(List<Szamla> szamlak)
        {
            MySqlConnection connection = new MySqlConnection();
            string connectionString = "SERVER = localhost;DATABASE=kockasfuzet;UID=root;PASSWORD=;";
            connection.ConnectionString = connectionString;
            connection.Open();
            List<Szamla> regiszamlak = GetSzamlaList();
            List<Szamla> rendezettszamla = SzamlaIDNormalizer(szamlak);
            for (int i = 0; i < regiszamlak.Count; i++)
            {
                string sql = "UPDATE `számla` SET `ID`=@ujID WHERE ID = @ID";
                MySqlCommand command = new MySqlCommand(sql, connection);
                command.Parameters.AddWithValue("@ujID", rendezettszamla[i].ID);
                command.Parameters.AddWithValue("@ID", regiszamlak[i].ID);
                command.ExecuteNonQuery();
            }
            return rendezettszamla;
        }

        static public Szamla GetSzamlaOBJ(int ID, List<Szamla> szamlak)
        {
            foreach (Szamla szamla in szamlak)
            {
                if (szamla.ID == ID)
                {
                    return szamla;
                }
            }
            return null;
        }

        static public void AddSzamla(List<Szamla> szamlak)
        {
            Console.Clear();
            Text.WriteLine("Új számla hozzáadása", ConsoleColor.Red);
            Text.WriteLine("=========================", ConsoleColor.DarkYellow);
            MySqlConnection connection = new MySqlConnection();
            string connectionString = "SERVER = localhost;DATABASE=kockasfuzet;UID=root;PASSWORD=;";
            connection.ConnectionString = connectionString;
            connection.Open();
            Console.Write("ID: ");
            string id = Console.ReadLine();
            if (id == "")
                return;
            else
            {
                bool idMegfelelo = false;

                while (!idMegfelelo)
                {
                    if (!Checker.IntChecker(id))
                    {
                        Text.WriteLine("Az ID csak szám lehet!", ConsoleColor.DarkRed);
                    }
                    else if (int.Parse(id) > szamlak.Count + 1)
                    {
                        Text.WriteLine($"Az ID nem lehet nagyobb, mint {szamlak.Count + 1}!", ConsoleColor.DarkRed);
                    }
                    else if (szamlak.Any(szamla => szamla.ID == int.Parse(id)))
                    {
                        Text.WriteLine("Már van ilyen azonosítójú számla!", ConsoleColor.DarkRed);
                    }
                    else
                    {
                        idMegfelelo = true;
                        continue;
                    }

                    Console.Write("ID: ");
                    id = Console.ReadLine();
                    if (id == "") 
                        return;
                }

            }
            Text.WriteLine("Választható szolgáltatások: ", ConsoleColor.Cyan);
            string tipusSql = "SELECT Azon, Nev FROM szolgaltatas";
            List<string> szolgaltatasazonosito = new List<string>();
            using (MySqlCommand cmd2 = new MySqlCommand(tipusSql, connection))
            {
                using (MySqlDataReader reader = cmd2.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        szolgaltatasazonosito.Add(reader["Azon"].ToString());
                        Console.WriteLine($" - {reader["Azon"]} ({reader["Nev"]})");
                    }
                }
            }
            Console.WriteLine("Szolgáltatás azonosítója: ");
            string szolgazon = Console.ReadLine();
            if (szolgazon != "")
            {
                while (!szolgaltatasazonosito.Contains(szolgazon))
                {
                    Text.WriteLine("Nincs ilyen szolgáltatás!", ConsoleColor.DarkRed);
                    Console.Write("Szolgáltatás azonosítója: ");
                    szolgazon = Console.ReadLine();
                }
            }
            else
            {
                return;
            }
            Console.WriteLine("--------------");
            Text.WriteLine("Választható szolgáltatók:", Console.ForegroundColor = ConsoleColor.Cyan);
            string listSql = "SELECT `RovidNev`, Nev FROM szolgaltato";
            List<string> szolgaltatorovidnevek = new List<string>();
            using (MySqlCommand listCmd = new MySqlCommand(listSql, connection))
            {
                using (MySqlDataReader reader = listCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        szolgaltatorovidnevek.Add(reader["RovidNev"].ToString());
                        Console.WriteLine($"- {reader["RovidNev"]} ({reader["Nev"]})");
                    }
                }
            }
            Console.WriteLine("--------------");
            Console.Write("Szolgáltató rövid neve: ");
            string szolgrovnev = Console.ReadLine();
            if (szolgrovnev != "")
            {
                while (!szolgaltatorovidnevek.Contains(szolgrovnev))
                {
                    Text.WriteLine("Nincs ilyen szolgáltató! Válassz a listából!", ConsoleColor.DarkRed);
                    Console.Write("Szolgáltató rövid neve: ");
                    szolgrovnev = Console.ReadLine();
                }
            }
            else
            {
                return;
            }
            Console.WriteLine("--------------");
            Console.Write("Tól: ");
            string tol = Console.ReadLine();
            if (tol == "")
                return;
            else
            {
                while (!Checker.DateTimeChecker(tol))
                {
                    Text.WriteLine("Nem megfelelő formátum! (ÉÉÉÉ-HH-NN)", ConsoleColor.DarkRed);
                    Console.WriteLine("Tól: ");
                    tol = Console.ReadLine();
                    if (tol == "")
                        return;
                }
            }
            Console.Write("Ig: ");
            string ig = Console.ReadLine();
            if (ig == "")
                return;
            else
            {
                while (!Checker.DateTimeChecker(ig))
                {
                    Text.WriteLine("Nem megfelelő formátum! (ÉÉÉÉ-HH-NN)", ConsoleColor.DarkRed);
                    Console.WriteLine("Ig: ");
                    ig = Console.ReadLine();
                    if (ig == "")
                        return;
                }
            }
            Console.Write("Összeg: ");
            string osszeg = Console.ReadLine();
            if (osszeg == "")
                return;
            else
            {
                while (!Checker.IntChecker(osszeg))
                {
                    Text.WriteLine("Nem megfelelő formátum! Csak számokat írj!", ConsoleColor.DarkRed);
                    Console.Write("Összeg: ");
                    osszeg = Console.ReadLine();
                    if (osszeg == "")
                        return;
                }
            }
            Console.Write("Határidő: ");
            string hatarido = Console.ReadLine();
            if (hatarido == "")
                return;
            else
            {
                while (!Checker.DateTimeChecker(hatarido))
                {
                    Text.WriteLine("Nem megfelelő formátum! (ÉÉÉÉ-HH-NN)", ConsoleColor.DarkRed);
                    Console.WriteLine("Határidő: ");
                    hatarido = Console.ReadLine();
                    if (hatarido == "")
                        return;
                }
            }
            Console.Write("Befizetve: ");
            string befizetve = Console.ReadLine();
            Console.Write("Megjegyzés: ");
            string megjegyzes = Console.ReadLine();
            string sql = "INSERT INTO `számla`(`ID`, `SzolgaltatasAzon`, `SzolgaltatoRovid`, `Tol`, `Ig`, `Osszeg`, `Hatarido`, `Befizetve`, `Megjegyzes`) VALUES (@ID,@SzolgaltatasAzon,@SzolgaltatoRovid,@Tol,@Ig,@Osszeg,@Hatarido,@Befizetve,@Megjegyzes);";
            MySqlCommand command = new MySqlCommand(sql, connection);
            command.Parameters.AddWithValue("@ID", id);
            command.Parameters.AddWithValue("@SzolgaltatasAzon", szolgazon);
            command.Parameters.AddWithValue("@SzolgaltatoRovid", szolgrovnev);
            command.Parameters.AddWithValue("@Tol", tol);
            command.Parameters.AddWithValue("@Ig", ig);
            command.Parameters.AddWithValue("@Osszeg", osszeg);
            command.Parameters.AddWithValue("@Hatarido", hatarido);
            command.Parameters.AddWithValue("@Befizetve", befizetve);
            command.Parameters.AddWithValue("@Megjegyzes", megjegyzes);
            int sorok = command.ExecuteNonQuery();
            connection.Close();
            string valasz = sorok > 0 ? "Sikeres hozzáadás!" : "Sikertelen hozzáadás!";
            Text.WriteLine(valasz, ConsoleColor.Cyan);
            Text.WriteLine("Enterrel vissza...", ConsoleColor.Yellow);
            Console.ReadLine();
        }

        static public void RemoveSzamla(List<Szamla> szamlak)
        {
            Console.Clear();
            Text.WriteLine("Számla törlés", ConsoleColor.Red);
            Text.WriteLine("=========================", ConsoleColor.DarkYellow);
            Console.Write("Add meg a törlendő számla id-jét: ");
            string id = Console.ReadLine();
            if (id == "")
                return;
            int szamlalo = 0;
            foreach (Szamla szamla in szamlak)
            {
                if (szamla.ID == int.Parse(id))
                {
                    MySqlConnection connection = new MySqlConnection();
                    string connectionString = "SERVER = localhost;DATABASE=kockasfuzet;UID=root;PASSWORD=;";
                    connection.ConnectionString = connectionString;
                    connection.Open();
                    string deletesql = $"DELETE FROM `számla` WHERE ID = @id";
                    MySqlCommand deletecmd = new MySqlCommand(deletesql, connection);
                    deletecmd.Parameters.AddWithValue("@id", id);
                    int sorok = deletecmd.ExecuteNonQuery();
                    connection.Close();
                    string valasz = sorok > 0 ? "Sikeres törlés!" : "Sikertelen törlés!";
                    Text.WriteLine(valasz, ConsoleColor.Cyan);
                }
                else
                {
                    szamlalo++;
                    if (szamlalo == szamlak.Count)
                    {
                        Text.WriteLine("Nincs ilyen számla!", ConsoleColor.DarkRed);
                    }
                }
            }
            Text.WriteLine("Enterrel vissza...", ConsoleColor.Yellow);
            Console.ReadLine();
        }

        static public void ModifySzamla(List<Szamla> szamlak)
        {
            Console.Clear();
            Text.WriteLine("Számla módosítás", ConsoleColor.Red);
            Text.WriteLine("=========================", ConsoleColor.DarkYellow);
            Console.Write("Add meg a módosítandó számla id-jét: ");
            string id = Console.ReadLine();
            if (id == "")
                return;
            int szamlalo = 0;
            bool modosit = false;
            Szamla modositando = new Szamla();
            foreach (Szamla szamla in szamlak)
            {
                if (szamla.ID == int.Parse(id))
                {
                    modosit = true;
                    modositando = szamla;
                    break;
                }
                else
                {
                    szamlalo++;
                    if (szamlalo == szamlak.Count)
                    {
                        Text.WriteLine("Nincs ilyen számla!", ConsoleColor.DarkRed);
                    }
                }
            }

            if (modosit)
            {
                MySqlConnection connection = new MySqlConnection();
                string connectionString = "SERVER = localhost;DATABASE=kockasfuzet;UID=root;PASSWORD=;";
                connection.ConnectionString = connectionString;
                connection.Open();
                Text.WriteLine("Ha valamit nem akar módosítani nyomjon entert!", ConsoleColor.Yellow);
                Text.WriteLine("Választható szolgáltatások: ", ConsoleColor.Cyan);
                string tipusSql = "SELECT Azon, Nev FROM szolgaltatas";
                List<string> szolgaltatasazonosito = new List<string>();
                using (MySqlCommand cmd2 = new MySqlCommand(tipusSql, connection))
                {
                    using (MySqlDataReader reader = cmd2.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            szolgaltatasazonosito.Add(reader["Azon"].ToString());
                            Console.WriteLine($" - {reader["Azon"]} ({reader["Nev"]})");
                        }
                    }
                }
                Console.WriteLine("--------------");
                Console.Write($"Mostani szolgáltatás azonosítója: ");
                Text.WriteLine($"{modositando.SzolgaltatasAzon}", ConsoleColor.Cyan);
                Console.Write("Szolgáltatás azonosítója: ");
                string szolgazon = Console.ReadLine();
                if(szolgazon != "")
                {
                    while (!szolgaltatasazonosito.Contains(szolgazon))
                    {
                        Text.WriteLine("Nincs ilyen szolgáltatás!", ConsoleColor.DarkRed);
                        Console.Write("Szolgáltatás azonosítója: ");
                        szolgazon = Console.ReadLine();
                    }
                }
                Console.WriteLine("--------------");
                Text.WriteLine("Választható szolgáltatók:", Console.ForegroundColor = ConsoleColor.Cyan);
                string listSql = "SELECT `RovidNev`, Nev FROM szolgaltato";
                List<string> szolgaltatorovidnevek = new List<string>();
                using (MySqlCommand listCmd = new MySqlCommand(listSql, connection))
                {
                    using (MySqlDataReader reader = listCmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            szolgaltatorovidnevek.Add(reader["RovidNev"].ToString());
                            Console.WriteLine($"- {reader["RovidNev"]} ({reader["Nev"]})");
                        }
                    }
                }
                Console.WriteLine("--------------");
                Console.Write($"Mostani szolgáltató rövid neve: ");
                Text.WriteLine($"{modositando.SzolgaltatoRovidNev}",ConsoleColor.Cyan);
                Console.Write("Szolgáltató rövid neve: ");
                string szolgrovnev = Console.ReadLine();
                if(szolgrovnev != "")
                {
                    while (!szolgaltatorovidnevek.Contains(szolgrovnev))
                    {
                        Text.WriteLine("Nincs ilyen szolgáltató! Válassz a listából!", ConsoleColor.DarkRed);
                        Console.Write("Szolgáltató rövid neve: ");
                        szolgrovnev = Console.ReadLine();
                    }
                }
                Console.WriteLine("--------------");
                Text.WriteWithInfo($"Tól", $"{modositando.Tol.ToShortDateString()}", ":", ConsoleColor.White, ConsoleColor.Cyan);
                string tol = Console.ReadLine();
                if (tol != "")
                {
                    while (!Checker.DateTimeChecker(tol))
                    {
                        Text.WriteLine("Nem megfelelő formátum! (ÉÉÉÉ-HH-NN)", ConsoleColor.DarkRed);
                        Console.WriteLine("Tól: ");
                        tol = Console.ReadLine();
                        if (tol == "")
                            break;
                    }
                }
                Text.WriteWithInfo($"Ig",$"{modositando.Ig.ToShortDateString()}", ":", ConsoleColor.White, ConsoleColor.Cyan);
                string ig = Console.ReadLine();
                if (ig != "")
                {
                    while (!Checker.DateTimeChecker(ig))
                    {
                        Text.WriteLine("Nem megfelelő formátum! (ÉÉÉÉ-HH-NN)", ConsoleColor.DarkRed);
                        Console.WriteLine("Ig: ");
                        ig = Console.ReadLine();
                        if (ig == "")
                            break;
                    }
                }
                Text.WriteWithInfo("Összeg", $"{modositando.Osszeg} Ft", ":", ConsoleColor.White, ConsoleColor.Cyan);
                string osszeg = Console.ReadLine();
                if (osszeg != "")
                {
                    while (!Checker.IntChecker(osszeg))
                    {
                        Text.WriteLine("Nem megfelelő formátum! Csak számokat írj!", ConsoleColor.DarkRed);
                        Console.Write("Összeg: ");
                        osszeg = Console.ReadLine();
                        if (osszeg == "")
                            break;
                    }
                }
                Text.WriteWithInfo("Határidő", $"{modositando.Hatarido.ToShortDateString()}", ":", ConsoleColor.White, ConsoleColor.Cyan);
                string hatarido = Console.ReadLine();
                if (hatarido != "")
                {
                    while (!Checker.DateTimeChecker(hatarido))
                    {
                        Text.WriteLine("Nem megfelelő formátum! (ÉÉÉÉ-HH-NN)", ConsoleColor.DarkRed);
                        Console.WriteLine("Határidő: ");
                        hatarido = Console.ReadLine();
                        if (hatarido == "")
                            break;
                    }
                }
                Text.WriteWithInfo("Befizetve", $"{modositando.Befizetve}", ":", ConsoleColor.White, ConsoleColor.Cyan);
                string befizetve = Console.ReadLine();
                if(befizetve != "")
                {
                    while (!Checker.DateTimeChecker(befizetve))
                    {
                        Text.WriteLine("Nem megfelelő formátum! (ÉÉÉÉ-HH-NN)", ConsoleColor.DarkRed);
                        Console.WriteLine("Befizetve: ");
                        befizetve = Console.ReadLine();
                        if (befizetve == "")
                            break;
                    }
                }
                Text.WriteWithInfo("Megjegyzés", $"{modositando.Megjegyzes}",":", ConsoleColor.White, ConsoleColor.Cyan);
                string megjegyzes = Console.ReadLine();
                string deletesql = $"UPDATE `számla` SET `ID`=@ID,`SzolgaltatasAzon`=@szazon,`SzolgaltatoRovid`=@szolgrovnev,`Tol`=@tol,`Ig`=@ig,`Osszeg`=@osszeg,`Hatarido`=@hatarido,`Befizetve`=@befizetve,`Megjegyzes`=@megjegyzes WHERE ID = @ID";
                MySqlCommand deletecmd = new MySqlCommand(deletesql, connection);
                deletecmd.Parameters.AddWithValue("@ID", modositando.ID);
                if (szolgazon != "")
                    deletecmd.Parameters.AddWithValue("@szazon", szolgazon);
                else
                    deletecmd.Parameters.AddWithValue("@szazon", modositando.SzolgaltatasAzon);
                if (szolgrovnev != "")
                    deletecmd.Parameters.AddWithValue("@szolgrovnev", szolgrovnev);
                else
                    deletecmd.Parameters.AddWithValue("@szolgrovnev", modositando.SzolgaltatoRovidNev);
                if (tol != "")
                    deletecmd.Parameters.AddWithValue("@tol", tol);
                else
                    deletecmd.Parameters.AddWithValue("@tol", modositando.Tol);
                if (ig != "")
                    deletecmd.Parameters.AddWithValue("@ig", ig);
                else
                    deletecmd.Parameters.AddWithValue("@ig", modositando.Ig);
                if (osszeg != "")
                    deletecmd.Parameters.AddWithValue("@osszeg", osszeg);
                else
                    deletecmd.Parameters.AddWithValue("@osszeg", modositando.Osszeg);
                if (hatarido != "")
                    deletecmd.Parameters.AddWithValue("@hatarido", hatarido);
                else
                    deletecmd.Parameters.AddWithValue("@hatarido", modositando.Hatarido);
                if (befizetve != "")
                    deletecmd.Parameters.AddWithValue("@befizetve", befizetve);
                else
                    deletecmd.Parameters.AddWithValue("@befizetve", modositando.Befizetve);
                if (megjegyzes != "")
                    deletecmd.Parameters.AddWithValue("@megjegyzes", megjegyzes);
                else
                    deletecmd.Parameters.AddWithValue("@megjegyzes", modositando.Megjegyzes);
                int sorok = deletecmd.ExecuteNonQuery();
                connection.Close();
                string valasz = sorok > 0 ? "Sikeres módosítás!" : "Sikertelen módosítás!";
                Text.WriteLine(valasz, ConsoleColor.Cyan);
            }
            Text.WriteLine("Enterrel vissza...", ConsoleColor.Yellow);
            Console.ReadLine();
        }
    }
}
