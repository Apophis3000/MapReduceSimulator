using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MapReduce
{
    public class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Console.WriteLine("Initialize books...");

            string[] books;

            //Datei auslesen
            Reader reader = new Reader(AppDomain.CurrentDomain.BaseDirectory, "books.txt");
            books = reader.GetContent();

            //Multithreading initialisieren
            ThreadFactory threadFac = new ThreadFactory(3);
            threadFac.StartThreadMap(0, books[0].Split('/')[0], books[0].Split('/')[1]);
            threadFac.StartThreadMap(1, books[1].Split('/')[0], books[0].Split('/')[1]);
            threadFac.StartThreadMap(2, books[2].Split('/')[0], books[0].Split('/')[1]);

            Console.WriteLine("Finished.");
            Console.WriteLine();
            Console.WriteLine("Mapping words...");

            while (!threadFac.ThreadFinished(0) && !threadFac.ThreadFinished(1) && !threadFac.ThreadFinished(2))
            {
                //Warteschleife
                Thread.Sleep(1000);
            }

            List<KeyValuePair<string, int>> words0 = threadFac.GetThreadMapResults(0);
            List<KeyValuePair<string, int>> words1 = threadFac.GetThreadMapResults(1);
            List<KeyValuePair<string, int>> words2 = threadFac.GetThreadMapResults(2);

            Dictionary<string, List<int>> wordCounts = new Dictionary<string, List<int>>();

            foreach (KeyValuePair<string, int> word in words0)
            {
                if (wordCounts.ContainsKey(word.Key))
                {

                }
            }
            foreach (KeyValuePair<string, int> word in words0)
            {

            }
            foreach (KeyValuePair<string, int> word in words0)
            {

            }

            Console.WriteLine("Finished.");
            Console.WriteLine();
            Console.WriteLine("");
        }
    }
}
/*
 * Multithreading einbauen
 *  - 3 Bücher, je eins in einen Thread auf Service aufrufen, warten bis alle fertig sind zum weiteren abarbeiten (MAP)
 *  - lokal zusammenfassen gleicher Worte (Werte addieren)
 *  - alle Wörter mit mehr als 1 in neuem Thread auf Service aufrufen, warten bis freie Ressource und nächsten Aufruf starten bis alle abgearbeitet (REDUCE)
 * Ausgabe auf Konsole
 * WCF-Dienst einbinden
 * 
 * Präsentation
 *  - Threading
 *  - Verbindung zum Server von Client aus
*/