using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using MapReduce.ServiceReference1;

namespace MapReduce
{
    public class WcfThread
    {
        public int ThreadIndex
        {
            get;
            set;
        }

        private Thread innerThread;
        private volatile List<KeyValuePair<string, int>> mapResults;
        private volatile string reduceResult;
        private ChannelFactory<IWCMapReduce> remoteFactory;
        private IWCMapReduce remoteProxy;

        public WcfThread(int threadIndex)
        {
            ThreadIndex = threadIndex;

            mapResults = new List<KeyValuePair<string, int>>();

            StartUpService();
        }

        public void StartMap(string title, string content)
        {
            innerThread = new Thread(() => InitMap(title, content));

            innerThread.Start();
        }
        public List<KeyValuePair<string, int>> GetMapResults()
        {
            return mapResults;
        }
        public void StartReduce(string word, List<int> counts)
        {
            innerThread = new Thread(() => InitReduce(word, counts));

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

        private void StartUpService()
        {
            //Auslesen der Adressen zur Laufzeit
            EndpointIdentity endId = EndpointIdentity.CreateSpnIdentity("mapReduce");

            Uri uri = new Uri(ConfigurationManager.AppSettings["vm" + ThreadIndex]);

            var address = new EndpointAddress(uri, endId);

            //Einbinden der Dienste
            remoteFactory = new ChannelFactory<IWCMapReduce>("WSHttpBinding_IWCMapReduce", address);
            remoteProxy = remoteFactory.CreateChannel();
        }
        private void InitMap(string title, string content)
        {
            KeyValuePair<string, string> temp = new KeyValuePair<string, string>(title, content);

            //Wcf-Dienst
            mapResults = remoteProxy.Map(temp).ToList();

            //Thread beenden
            innerThread.Join();
        }
        private void InitReduce(string word, List<int> counts)
        {
            //Wcf-Dienst
            remoteProxy.Reduce(word, counts.ToArray());
        }
    }
}