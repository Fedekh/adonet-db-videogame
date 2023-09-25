using System;
using adonet_db_videogame.Utility;
using System.IO;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace adonet_db_videogame
{
    class  Program
    {
        public static void Main(string[] args)
        {

            string server = ConfigurationManager.AppSettings["SQLServer"];
            string db = ConfigurationManager.AppSettings["SQLDB"];
            string userId = ConfigurationManager.AppSettings["userId"];
            string password = ConfigurationManager.AppSettings["password"];

            DBManager dBManager = new DBManager(server, db,userId, password);

            int number = 1;




            while (number > 0 && number < 5)
            {
            Console.WriteLine("\r\n\r\nConnessione stabilita, scegli cosa vuoi fare:\r\n1) 1 inserire un nuovo videogioco\r\n2) ricercare un videogioco per id\r\n3) ricercare tutti i videogiochi aventi il nome contenente una determinata stringa inserita in input\r\n4) cancellare un videogioco\r\n5) chiudere il programma\r\n");
            Console.WriteLine("Scegli un azione\r\n");
            number = int.Parse(Console.ReadLine());
            Console.WriteLine();
                switch (number)
                {
                    case 1:
                        Console.WriteLine("\nAggiungere un nome: ");
                        string videoGameName = Console.ReadLine();
                        Console.WriteLine("\nAggiungere una descrizione: ");
                        string videoGameOverview = Console.ReadLine();
                        DateTime videoGamenReleaseDate = DateTime.Now;

                        Videogame videogame = new Videogame(videoGameName, videoGameOverview, videoGamenReleaseDate);
                        bool isCreated = dBManager.CreateGame(videogame);

                        if (isCreated)
                        {
                            Console.WriteLine("\nVideogame creato con successo");
                        }
                        else
                        {
                        Console.WriteLine("\nC'è stato un problema nella creazione del videogame");
                        }

                          break; 

                    case 2:
                        Console.WriteLine("\n Ricerca un gioco per ID: ");
                        long videoGameId = int.Parse(Console.ReadLine());
                        Console.WriteLine(dBManager.GetGameId(videoGameId)?.ToString() ?? "NOT FOUND");

                        break;

                    case 3:
                        Console.WriteLine("\nScegliere il nome del gioco da cercare: ");
                        videoGameName = Console.ReadLine();
                        List<Videogame> videogames = dBManager.SearchVideogamesByName(videoGameName);

                        if (videogames.Count() > 0)
                        {
                            foreach (var item in videogames)
                            {
                                Console.WriteLine("--------------------------------------------");
                                Console.WriteLine(item?.ToString() ?? "404 NOT FOUND :(");
                                Console.WriteLine("--------------------------------------------");
                            }
                            Console.WriteLine($"{Environment.NewLine}{videogames.Count()} risultati trovati{Environment.NewLine}");
                        }
                        else
                        {
                            Console.WriteLine("Nessuna Corrispondenza");
                        }

                        break;
                    case 4:
                        Console.WriteLine("Cancella un videogioco per ID:");
                        long deleteId = long.Parse(Console.ReadLine());
                        if (dBManager.DeleteGame(deleteId))
                        {
                            Console.WriteLine("Eliminato con successo");
                        }
                        else
                        {
                            Console.WriteLine("Videogioco non trovato");
                        }
                        break;
                }
            }

            Console.WriteLine("Sei uscito");
            Console.ReadLine();
                       
        }
             
    }
}
