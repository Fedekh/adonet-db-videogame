using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace adonet_db_videogame
{
    class Program
    {
        static void Main(string[] args)
        {
            string server = "DESKTOP-8HJQROF\\SQLEXPRESS";
            string database = "Videogame";
            string connectionString = $"Server={server};Database={database};Integrated Security=True;";


            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                try
                {
                    sqlConnection.Open();
                    Console.WriteLine("Connessione stabilita, scegli cosa vuoi fare:\r\n1) 1 inserire un nuovo videogioco\r\n2) ricercare un videogioco per id\r\n3) ricercare tutti i videogiochi aventi il nome contenente una determinata stringa inserita in input\r\n4) cancellare un videogioco\r\n5) chiudere il programma");

                    string query = "SELECT * from videogames";
                    using (SqlCommand cmd = new SqlCommand(query, sqlConnection)) 

                    using (SqlDataReader reader = cmd.ExecuteReader()) 
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine();

                            Console.WriteLine($"Titolo Videogame: {reader.GetString(1)}");
                            Console.WriteLine();

                            Console.WriteLine($"Descrizion: {reader.GetString(2)}");
                            Console.WriteLine();

                            Console.WriteLine($"Data di rilascio: {reader.GetDateTime(3)}");
                            Console.WriteLine();

                        }
                    }
                    Console.ReadLine();
                }

                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

        }
    }
}
