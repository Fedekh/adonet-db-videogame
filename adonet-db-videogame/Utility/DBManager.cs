using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Xml.Linq;

namespace adonet_db_videogame.Utility
{
    public class DBManager
    {
        private string _server;
        private string _db;
        private string _userId;
        private string _passWord;
        private string _connectionString;

        public DBManager(string server, string db, string userId, string password)
        {
            _server = server;
            _db = db;
            _userId = userId;
            _passWord = password;
            _connectionString = $"Server={server};Database={db};User ID={userId};Password={password};";
        }

        public bool CreateGame(Videogame newVideogame)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
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

        public List<Videogame> SearchVideogamesByName(string name)
        {
            List<Videogame> videogames = new List<Videogame>();
            Videogame videogameFounded = null;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                try
                {

                    connection.Open();


                    string query = $"SELECT v.id, v.name, v.overview, v.release_date, softerH.id FROM videogames v JOIN software_houses softerH ON softerH.id = v.software_house_id WHERE v.name LIKE(@Name);";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.Add(new SqlParameter("@Name", $"%{name}%"));
                        using (SqlDataReader data = cmd.ExecuteReader())
                        while (data.Read())
                        {
                            videogameFounded = new Videogame(data.GetInt64(0), data.GetString(1), data.GetString(2), data.GetDateTime(3), data.GetInt64(4));
                            videogames.Add(videogameFounded);
                        }
                    }
                }
                catch (Exception ex) { Console.WriteLine(ex.Message); }
            }
            return videogames;
        }



        public Videogame GetGameId(long id)
        {
            Videogame videogame = null;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                try
                {
                    {
                        connection.Open();
                        string query = "SELECT id, name, overview, release_date, software_house_id FROM videogames WHERE id = @id";

                        using (SqlCommand cmd = new SqlCommand(query, connection))
                        {
                            cmd.Parameters.AddWithValue("@id", id);

                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    long gameId = reader.GetInt64(0);
                                    string name = reader.GetString(1);
                                    string overview = reader.GetString(2);
                                    DateTime releaseDate = reader.GetDateTime(3);
                                    long softwareHouseId = reader.GetInt64(4);

                                    return new Videogame(gameId, name, overview, releaseDate, softwareHouseId);
                                }
                                throw new Exception($"Errore durante la ricerca del videogioco per ID:");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Errore durante la ricerca del videogioco per ID: {ex.Message}");
                }
            }
            return videogame;
        }

            public bool DeleteGame(long id)
            {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "DELETE from videogames where id=@Id";
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.Add(new SqlParameter("@id", id));
                    int affectedRows = cmd.ExecuteNonQuery();
                    if (affectedRows > 0)
                    {
                        return true;
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                return false;
            }
        }
    }

}
