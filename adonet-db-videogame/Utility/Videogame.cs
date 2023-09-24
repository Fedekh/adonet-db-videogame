using System;

namespace adonet_db_videogame.Utility
{
    public class Videogame
    {
        public long Id { get; private set; }
        public string Name { get; private set; }
        public string Overview { get; private set; }
        public DateTime ReleaseDate { get; private set; }
        public long SoftwareHouseId { get; private set; } = new Random().Next(1, 7);

        public Videogame(string name, string overview, DateTime releaseDate)
        {
            Name = name;
            Overview = overview;
            ReleaseDate = releaseDate;
        }
        public Videogame(long id, string name, string overview, DateTime releaseDate, long softwereId)
        {
            Id = id;
            Name = name;
            Overview = overview;
            ReleaseDate = releaseDate;
            SoftwareHouseId = softwereId;
        }

        public override string ToString()
        {
            return $"ID: {this.Id} -Titolo: {this.Name}{Environment.NewLine} {Environment.NewLine}-Descrizione: {this.Overview} {Environment.NewLine}{Environment.NewLine}-Rilasciato: {this.ReleaseDate}{Environment.NewLine}";
        }
    }
}
