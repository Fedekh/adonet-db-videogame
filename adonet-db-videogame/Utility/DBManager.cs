using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace adonet_db_videogame.Utility
{
    public static class DBManager
    {
        private const string server = "DESKTOP-8HJQROF\\SQLEXPRESS";
        private const string database = "Videogame";
        private static string connectionString = $"Data Source={server};Initial Catalog={database};Integrated Security=True;";

        public static bool CreateGame(Videogame newVideogame)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = $"INSERT INTO videogames ( name , overview, release_date, software_house_id) VALUES (@Name, @Overview, @ReleaseDate, @SoftwareHouseId)";
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.Add(new SqlParameter("@name", newVideogame.Name));
                        cmd.Parameters.Add(new SqlParameter("@Overview", newVideogame.Overview));
                        cmd.Parameters.Add(new SqlParameter("@ReleaseDate", newVideogame.ReleaseDate));
                        cmd.Parameters.Add(new SqlParameter("@SoftwareHouseId", newVideogame.SoftwareHouseId));

                        bool rowsAffected = cmd.ExecuteNonQuery() > 0;
                        if (!rowsAffected)
                        {

                            return false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                return true;
            }

        }

        public static List<Videogame> GetVideogames(string name)
        {
            List<Videogame> videogames = new List<Videogame>();
            Videogame videogame = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = $"SELECT name, overview, release_date, software_house_id from Videogame where name like '%@name'";
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.Add(new SqlParameter("@name", name));
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                videogame = new Videogame(reader.GetInt64(0), reader.GetString(1), reader.GetString(2), reader.GetDateTime(3), reader.GetInt64(4));
                                videogames.Add(videogame);
                            }
                        }
                    }
                }
                catch (Exception ex) { Console.WriteLine(ex.Message); }
            }
            return videogames;

        }

        public static Videogame GetGameId(long id)
        {
            Videogame videogame = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = $"SELECT name, overview, release_date, software_house_id from Videogame where id = @id";
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.Add(new SqlParameter("@id", id));
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                videogame = new Videogame(reader.GetInt64(0), reader.GetString(1), reader.GetString(2), reader.GetDateTime(3), reader.GetInt64(4));
                                return videogame;

                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine();
                }
            }
            return videogame;
        }

            public static void DeleteGame(string name)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();
                        string query = "DELETE from videogames where name = @name";
                        SqlCommand cmd = new SqlCommand(query, connection);
                        cmd.Parameters.Add(new SqlParameter("@name", name));
                        int affectedRows = cmd.ExecuteNonQuery();
                        if (affectedRows > 0)
                        {
                            Console.WriteLine($"Il videogioco {name} è stato eliminato correttamente");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine();
                    }
                }
        }
    }

}
