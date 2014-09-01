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
            threadFac.StartThreadMap(1, books[1].Split('/')[0], books[1].Split('/')[1]);
            threadFac.StartThreadMap(2, books[2].Split('/')[0], books[2].Split('/')[1]);

            Console.WriteLine("Finished.");
            Console.WriteLine();
            Console.WriteLine("Mapping words...");

            //Warteschleife bis alle Threads abgearbeitet sind
            while (true)
            {
                bool fin0 = threadFac.ThreadFinished(0);
                bool fin1 = threadFac.ThreadFinished(1);
                bool fin2 = threadFac.ThreadFinished(2);

                if (fin0 && fin1 && fin2)
                {
                    break;
                }
            }

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
            foreach (KeyValuePair<string, int> word in words1)
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
            foreach (KeyValuePair<string, int> word in words2)
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
                bool wordIsInProgress = false;

                while (true)
                {
                    if (!wordIsInProgress && threadFac.ThreadFinished(0))
                    {
                        string result = threadFac.GetThreadReduceResult(0);

                        if (!String.IsNullOrEmpty(result))
                        {
                            output.Add(result);
                        }

                        threadFac.StartThreadReduce(0, word, wordCounts[word]);
                        wordIsInProgress = true;
                    }
                    if (!wordIsInProgress && threadFac.ThreadFinished(1))
                    {
                        string result = threadFac.GetThreadReduceResult(1);

                        if (!String.IsNullOrEmpty(result))
                        {
                            output.Add(result);
                        }

                        threadFac.StartThreadReduce(1, word, wordCounts[word]);
                        wordIsInProgress = true;
                    }
                    if (!wordIsInProgress && threadFac.ThreadFinished(2))
                    {
                        string result = threadFac.GetThreadReduceResult(2);

                        if (!String.IsNullOrEmpty(result))
                        {
                            output.Add(result);
                        }

                        threadFac.StartThreadReduce(2, word, wordCounts[word]);
                        wordIsInProgress = true;
                    }

                    if (wordIsInProgress)
                    {
                        break;
                    }
                }
            }

            //Warteschleife bis alle Threads abgearbeitet sind
            while (true)
            {
                bool fin0 = threadFac.ThreadFinished(0);
                bool fin1 = threadFac.ThreadFinished(1);
                bool fin2 = threadFac.ThreadFinished(2);

                if (fin0 && fin1 && fin2)
                {
                    string result0 = threadFac.GetThreadReduceResult(0);
                    string result1 = threadFac.GetThreadReduceResult(1);
                    string result2 = threadFac.GetThreadReduceResult(2);

                    if (!String.IsNullOrEmpty(result0))
                    {
                        output.Add(result0);
                    }
                    if (!String.IsNullOrEmpty(result1))
                    {
                        output.Add(result1);
                    }
                    if (!String.IsNullOrEmpty(result2))
                    {
                        output.Add(result2);
                    }

                    break;
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
 * Präsentation
 *  - Threading
 *  - Verbindung zum Server von Client aus
*/