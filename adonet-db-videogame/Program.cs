using System;
using adonet_db_videogame.Utility;
using System.IO;
using System.Collections.Generic;

namespace adonet_db_videogame
{
    class  Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Connessione stabilita, scegli cosa vuoi fare:\r\n1) 1 inserire un nuovo videogioco\r\n2) ricercare un videogioco per id\r\n3) ricercare tutti i videogiochi aventi il nome contenente una determinata stringa inserita in input\r\n4) cancellare un videogioco\r\n5) chiudere il programma");
            int number = int.Parse(Console.ReadLine());
            Console.WriteLine();

            while (number > 0 && number < 5)
            {
                switch (number)
                {
                    case 1:
                        Console.WriteLine("\nAggiungere un nome: ");
                        string videoGameName = Console.ReadLine();
                        Console.WriteLine("\nAggiungere una descrizione: ");
                        string videoGameOverview = Console.ReadLine();
                        DateTime videoGamenReleaseDate = DateTime.Now;

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
                        long videoGameId = int.Parse(Console.ReadLine());
                        Console.WriteLine(DBManager.GetGameId(videoGameId)?.ToString() ?? "NOT FOUND");

                        break;

                    case 3:
                        Console.WriteLine("\n Ricevi una lista di Videogame contenenti la parola.....");
                        string word = Console.ReadLine();
                        List<Videogame> videogames = DBManager.GetVideogames(word);
                        if (videogames.Count > 0)
                        {
                            foreach (var item in videogames)
                            {
                                Console.WriteLine();
                                Console.WriteLine(item?.ToString());
                                Console.WriteLine();
                            }
                            Console.WriteLine($"Trovati {videogames.Count} risultati");
                        }
                        break;

                    case 4:
                        Console.WriteLine("Cancella un videogioco per ID:");
                        long deleteId = long.Parse(Console.ReadLine());
                        if (DBManager.DeleteGame(deleteGame))
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
