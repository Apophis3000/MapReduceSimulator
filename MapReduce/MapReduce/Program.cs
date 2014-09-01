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

            while (!threadFac.ThreadFinished(0) && !threadFac.ThreadFinished(1) && !threadFac.ThreadFinished(2));

            List<KeyValuePair<string, int>> words0 = threadFac.GetThreadMapResults(0);
            List<KeyValuePair<string, int>> words1 = threadFac.GetThreadMapResults(1);
            List<KeyValuePair<string, int>> words2 = threadFac.GetThreadMapResults(2);

            //Daten für Reduce-Vorgang
            Dictionary<string, List<int>> wordCounts = new Dictionary<string, List<int>>();

            foreach (KeyValuePair<string, int> word in words0)
            {
                if (wordCounts.ContainsKey(word.Key))
                {
                    wordCounts[word.Key].Add(word.Value);
                }
                else
                {
                    wordCounts.Add(word.Key, new List<int> { word.Value } );
                }
            }
            foreach (KeyValuePair<string, int> word in words0)
            {
                if (wordCounts.ContainsKey(word.Key))
                {
                    wordCounts[word.Key].Add(word.Value);
                }
                else
                {
                    wordCounts.Add(word.Key, new List<int> { word.Value });
                }
            }
            foreach (KeyValuePair<string, int> word in words0)
            {
                if (wordCounts.ContainsKey(word.Key))
                {
                    wordCounts[word.Key].Add(word.Value);
                }
                else
                {
                    wordCounts.Add(word.Key, new List<int> { word.Value });
                }
            }

            Console.WriteLine("Finished.");
            Console.WriteLine();
            Console.WriteLine("Reduce words...");

            //Ausgabeergebnisse
            List<string> output = new List<string>();

            //Alle Wörter einzeln an den nächsten freien Thread übergeben
            foreach (string word in wordCounts.Keys)
            {
                bool isInProgress = false;

                while (!isInProgress && (!threadFac.ThreadFinished(0) || !threadFac.ThreadFinished(1) || !threadFac.ThreadFinished(2)))
                {
                    if (!isInProgress && threadFac.ThreadFinished(0))
                    {
                        threadFac.StartThreadReduce(0, word, wordCounts[word]);
                        output.Add(threadFac.GetThreadReduceResult(0));
                        isInProgress = true;
                    }
                    if (!isInProgress && threadFac.ThreadFinished(1))
                    {
                        threadFac.StartThreadReduce(1, word, wordCounts[word]);
                        output.Add(threadFac.GetThreadReduceResult(1));
                        isInProgress = true;
                    }
                    if (!isInProgress && threadFac.ThreadFinished(2))
                    {
                        threadFac.StartThreadReduce(2, word, wordCounts[word]);
                        output.Add(threadFac.GetThreadReduceResult(2));
                        isInProgress = true;
                    }
                }
            }

            Console.WriteLine("Finished.");
            Console.WriteLine();
            Console.WriteLine("Output:");
            Console.WriteLine();

            foreach (string result in output)
            {
                Console.WriteLine(result);
            }

            Console.ReadLine();
        }
    }
}
/*
 * WCF-Dienst einbinden
 * 
 * Präsentation
 *  - Threading
 *  - Verbindung zum Server von Client aus
*/