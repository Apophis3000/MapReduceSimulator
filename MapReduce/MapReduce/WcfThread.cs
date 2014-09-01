using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MapReduce
{
    public class WcfThread
    {
        private Thread innerThread;
        private List<KeyValuePair<string, int>> mapResults;
        private string reduceResult;

        public WcfThread()
        {
            mapResults = new List<KeyValuePair<string, int>>();
        }

        public void StartMap(string title, string content)
        {
            innerThread = new Thread(InitMap);

            innerThread.Start();
        }
        public List<KeyValuePair<string, int>> GetMapResults()
        {
            return mapResults;
        }
        public void StartReduce()
        {
            innerThread = new Thread(InitReduce);

            innerThread.Start();
        }
        public string GetReduceResult()
        {
            return reduceResult;
        }
        public bool Finished()
        {
            return !innerThread.IsAlive;
        }

        private void InitMap()
        {
            //Wcf-Dienst
            //Am Ende Thread stoppen
        }
        private void InitReduce()
        {
            //Wcf-Dienst
            //Am Ende Thread stoppen
        }
    }
}