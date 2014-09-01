using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapReduce
{
    public class ThreadFactory
    {
        public WcfThread[] AllThreads
        {
            get;
            private set;
        }

        public ThreadFactory(int countThreads)
        {
            AllThreads = new WcfThread[countThreads];

            for (int index = 0; index < countThreads; index++)
            {
                AllThreads[index] = new WcfThread();
            }
        }

        public void StartThreadMap(int threadIndex, string bookTitle, string bookContent)
        {
            AllThreads[threadIndex].StartMap(bookTitle, bookContent);
        }
        public List<KeyValuePair<string, int>> GetThreadMapResults(int threadIndex)
        {
            return AllThreads[threadIndex].GetMapResults();
        }
        public void StartThreadReduce(int threadIndex)
        {
            AllThreads[threadIndex].StartReduce();
        }
        public string GetReduceResult(int threadIndex)
        {
            return AllThreads[threadIndex].GetReduceResult();
        }
        public bool ThreadFinished(int threadIndex)
        {
            return AllThreads[threadIndex].Finished();
        }
    }
}