using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KockasFuzet.Models;
using MySql.Data.MySqlClient;

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
    }
}
