using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KockasFuzet.Models;
using MySql.Data.MySqlClient;

namespace KockasFuzet.Controller
{
    internal class SzamlaController
    {
        static public List<Szamla> GetSzamlaList()
        {
            List<Szamla> szamlaList = new List<Szamla>();
            MySqlConnection connection = new MySqlConnection();
            string connectionString = "SERVER = localhost;DATABASE=kockasfuzet;UID=root;PASSWORD=;";
            connection.ConnectionString = connectionString;
            connection.Open();
            string sql = "SELECT * FROM számla;";
            MySqlCommand command = new MySqlCommand(sql, connection);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Szamla szamla = new Szamla()
                {
                    ID = reader.GetInt32("ID"),
                    SzolgaltatasAzon = reader.GetInt32("SzolgaltatasAzon"),
                    SzolgaltatoRovidNev = reader.GetString("SzolgaltatoRovid"),
                    Tol = reader.GetDateTime("Tol"),
                    Ig = reader.GetDateTime("Ig"),
                    Osszeg = reader.GetInt32("Osszeg"),
                    Hatarido = reader.GetDateTime("Hatarido"),
                    Befizetve = reader.GetDateTime("Befizetve"),
                    Megjegyzes = reader.GetString("Megjegyzes")
                };
                szamlaList.Add(szamla);
            }
            connection.Close();
            return szamlaList;
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
    }
}
