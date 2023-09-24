using System;
using adonet_db_videogame.Utility;
using System.IO;

namespace adonet_db_videogame
{
    class  Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Connessione stabilita, scegli cosa vuoi fare:\r\n1) 1 inserire un nuovo videogioco\r\n2) ricercare un videogioco per id\r\n3) ricercare tutti i videogiochi aventi il nome contenente una determinata stringa inserita in input\r\n4) cancellare un videogioco\r\n5) chiudere il programma");
            int number = int.Parse(Console.ReadLine());

            string videoGameName = "";
            string videoGameOverview = "";
            long videoGameId;
            DateTime videoGamenReleaseDate;

            while (number > 0 && number < 5)
            {
                switch (number)
                {
                    case 1:
                        Console.WriteLine("\nAggiungere un nome: ");
                        videoGameName = Console.ReadLine();
                        Console.WriteLine("\nAggiungere una descrizione: ");
                        videoGameOverview = Console.ReadLine();
                        videoGamenReleaseDate = DateTime.Now;

                        Videogame videogame = new Videogame(videoGameName, videoGameOverview, videoGamenReleaseDate);
                        bool isCreated = DBManager.CreateGame(videogame);

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
                        videoGameId = int.Parse(Console.ReadLine());
                        Console.WriteLine(DBManager.GetGameId(videoGameId)?.ToString() ?? "NOT FOUND");

                        break;

                }
            }

            Console.WriteLine("Sei uscito");
            Console.ReadLine();
                       
        }
             
    }
}
