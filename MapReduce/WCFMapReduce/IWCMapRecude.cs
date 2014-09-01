using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WCFMapReduce
{
    // Word-Count: 
        // T = KeyValuePair<string, string> ("Game of Thrones", "Hallo Hallo welt")
        // K = KeyValuePair<string, int> ("Hallo", 1)
        // L = List<int> // {1, 1, 1}
    [ServiceContract(SessionMode = SessionMode.Required)]
    public interface IWCMapReduce           
    {
        [OperationContract]
        List<KeyValuePair<string, int>> Map(KeyValuePair<string, string> record);

        [OperationContract]
        string Reduce(string key, List<int> values);
    }
}
