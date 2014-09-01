using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WCFMapReduce
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall,
        ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class WCMapReduce : IWCMapReduce
    {
        public List<KeyValuePair<string, int>> Map(KeyValuePair<string, string> record)
        {
            List<KeyValuePair<string, int>> result = new List<KeyValuePair<string, int>>();

            foreach (string word in record.Value.Split(' '))
            {
                result.Add(new KeyValuePair<string, int>(word, 1));
            }

            return result;
        }

        public string Reduce(string key, List<int> values)
        {
            int total = 0;

            foreach (var val in values)
            {
                total++;
            }

            return string.Format("({0}, {1})", key, total);
        }
    }
}
