using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KockasFuzet.Models;
using MySql.Data.MySqlClient;
using KockasFuzet.Views;
using Org.BouncyCastle.Crypto;

namespace KockasFuzet.Controller
{
    internal class SzolgaltatoController
    {
        static public List<Szolgaltato> GetSzolgaltatoList()
        {
            List<Szolgaltato> szolgaltatoList = new List<Szolgaltato>();
            MySqlConnection connection= new MySqlConnection();
            string connectionString = "SERVER = localhost;DATABASE=kockasfuzet;UID=root;PASSWORD=;";
            connection.ConnectionString = connectionString;
            connection.Open();
            string sql = "SELECT * FROM szolgaltato;";
            MySqlCommand command = new MySqlCommand(sql, connection);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Szolgaltato szolgaltato = new Szolgaltato()
                {
                    RovidNev = reader.GetString("rovidnev"),
                    Nev = reader.GetString("nev"),
                    UgyfelSzolgalat = reader.GetString("ugyfelszolgalat")
                };
                szolgaltatoList.Add(szolgaltato);
            }
            connection.Close();
            return szolgaltatoList;
        }

        static public Szolgaltato GetSzolgaltatoOBJ(string rovidnev,List<Szolgaltato> szolgaltatok)
        {
            foreach (Szolgaltato szolgaltato in szolgaltatok)
            {
                if (szolgaltato.RovidNev == rovidnev)
                {
                    return szolgaltato;
                }
            }
            return null;
        }

        static public void AddSzolgaltato()
        {
            Console.Clear();
            Text.WriteLine("Új szolgáltató hozzáadása", ConsoleColor.Red);
            Text.WriteLine("=========================", ConsoleColor.DarkYellow);
            Console.Write("Rövid név: ");
            string rovidnev = Console.ReadLine();
            if (rovidnev == "")
                return;
            Console.Write("Név: ");
            string nev = Console.ReadLine();
            if (nev == "")
                return;
            Console.Write("Ügyfélszolgálat címe: ");
            string ugyfelszolgalat = Console.ReadLine();
            if (ugyfelszolgalat == "")
                return;
            MySqlConnection connection = new MySqlConnection();
            string connectionString = "SERVER = localhost;DATABASE=kockasfuzet;UID=root;PASSWORD=;";
            connection.ConnectionString = connectionString;
            connection.Open();
            string sql = "INSERT INTO szolgaltato (rovidnev, nev, ugyfelszolgalat) VALUES (@rovidnev, @nev, @ugyfelszolgalat);";
            MySqlCommand command = new MySqlCommand(sql, connection);
            command.Parameters.AddWithValue("@rovidnev", rovidnev);
            command.Parameters.AddWithValue("@nev", nev);
            command.Parameters.AddWithValue("@ugyfelszolgalat", ugyfelszolgalat);
            int sorok = command.ExecuteNonQuery();
            connection.Close();
            string valasz = sorok > 0 ? "Sikeres hozzáadás!" : "Sikertelen hozzáadás!";
            Text.WriteLine(valasz, ConsoleColor.Cyan);
            Text.WriteLine("Enterrel vissza...", ConsoleColor.Yellow);
            Console.ReadLine();
        }

        static public void RemoveSzolgaltato(List<Szolgaltato> szolgaltatok)
        {
            Console.Clear();
            Text.WriteLine("Szolgáltató törlés", ConsoleColor.Red);
            Text.WriteLine("=========================", ConsoleColor.DarkYellow);
            Console.Write("Add meg a törlendő szolgáltató rövidnevét: ");
            string rovidnev = Console.ReadLine();
            if (rovidnev == "")
                return;
            int szamlalo = 0;
            foreach (Szolgaltato szolgaltato in szolgaltatok)
            {
                if (szolgaltato.RovidNev == rovidnev)
                {
                    MySqlConnection connection = new MySqlConnection();
                    string connectionString = "SERVER = localhost;DATABASE=kockasfuzet;UID=root;PASSWORD=;";
                    connection.ConnectionString = connectionString;
                    connection.Open();  
                    string deletesql = $"DELETE FROM `szolgaltato` WHERE RovidNev = @rovidNev";
                    MySqlCommand deletecmd = new MySqlCommand(deletesql, connection);
                    deletecmd.Parameters.AddWithValue("@rovidNev", rovidnev);
                    int sorok = deletecmd.ExecuteNonQuery();
                    connection.Close();
                    string valasz = sorok > 0 ? "Sikeres törlés!" : "Sikertelen törlés!";
                    Text.WriteLine(valasz, ConsoleColor.Cyan);
                }
                else
                {
                    szamlalo++;
                    if (szamlalo == szolgaltatok.Count)
                    {
                        Text.WriteLine("Nincs ilyen szolgáltató!", ConsoleColor.DarkRed);
                    }
                }
            }
            Text.WriteLine("Enterrel vissza...", ConsoleColor.Yellow);
            Console.ReadLine();
        }

        static public void ModifySzolgaltato(List<Szolgaltato> szolgaltatok)
        {
            Console.Clear();
            Text.WriteLine("Szolgáltató módosítás", ConsoleColor.Red);
            Text.WriteLine("=========================", ConsoleColor.DarkYellow);
            Console.Write("Add meg a módosítandó szolgáltató rövidnevét: ");
            string rovidnev = Console.ReadLine();
            if (rovidnev == "")
                return;
            int szamlalo = 0;
            bool modosit = false;
            Szolgaltato modositando = new Szolgaltato();
            foreach (Szolgaltato szolgaltato in szolgaltatok)
            {
                if (szolgaltato.RovidNev == rovidnev)
                {
                    modosit = true;
                    modositando = szolgaltato;
                    break;
                }
                else
                {
                    szamlalo++;
                    if (szamlalo == szolgaltatok.Count)
                    {
                        Text.WriteLine("Nincs ilyen szolgáltató!", ConsoleColor.DarkRed);
                    }
                }
            }
            if (modosit)
            {
                Text.WriteLine("Ha valamit nem akar módosítani nyomjon entert!", ConsoleColor.Yellow);
                Console.WriteLine($"Rövid név({modositando.RovidNev}): ");
                string ujrovidnev = Console.ReadLine();
                Console.Write($"Név ({modositando.Nev}): ");
                string ujnev = Console.ReadLine();
                Console.Write($"Ügyfélszolgálat címe ({modositando.UgyfelSzolgalat}): ");
                string ujugyfelszolgalat = Console.ReadLine();
                MySqlConnection connection = new MySqlConnection();
                string connectionString = "SERVER = localhost;DATABASE=kockasfuzet;UID=root;PASSWORD=;";
                connection.ConnectionString = connectionString;
                connection.Open();
                string deletesql = $"UPDATE `szolgaltato` SET `RovidNev`=@rovidNev,`Nev`=@nev,`UgyfelSzolgalat`=@ugyfelszolgalat WHERE RovidNev = @originalRovidNev";
                MySqlCommand deletecmd = new MySqlCommand(deletesql, connection);
                deletecmd.Parameters.AddWithValue("@originalRovidNev", modositando.RovidNev);
                if (ujrovidnev != "")
                    deletecmd.Parameters.AddWithValue("@rovidNev", ujrovidnev);
                else
                    deletecmd.Parameters.AddWithValue("@rovidNev", modositando.RovidNev);
                if (ujnev != "")
                    deletecmd.Parameters.AddWithValue("@nev", ujnev);
                else
                    deletecmd.Parameters.AddWithValue("@nev", modositando.Nev);
                if (ujugyfelszolgalat != "")
                    deletecmd.Parameters.AddWithValue("@ugyfelszolgalat", ujugyfelszolgalat);
                else
                    deletecmd.Parameters.AddWithValue("@ugyfelszolgalat", modositando.UgyfelSzolgalat);
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
