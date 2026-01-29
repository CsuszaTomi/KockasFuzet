using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KockasFuzet.Models;
using MySql.Data.MySqlClient;

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
    }
}
