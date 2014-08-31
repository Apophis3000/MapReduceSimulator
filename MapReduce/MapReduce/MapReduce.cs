using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapReduce
{
    public class MapReduce<T>
    {
        private Dictionary<string, List<object>> intermediate;
        private List<object> result;

        public MapReduce()
        {
            intermediate = new Dictionary<string, List<object>>();
            result = new List<object>();
        }

        public void Emit_Intermediate(string key, object value)
        {
            if (!intermediate.ContainsKey(key))
            {
                intermediate.Add(key, new List<object>());
            }
            
            intermediate[key].Add(value);            
        }

        public void Emit(object value)
        {
            result.Add(value);
        }

        public void Execute(List<T> data, Action<T> mapper, Action<string, List<object>> reducer)
        {
            data.ForEach(mapper);
            
            foreach (string key in intermediate.Keys)
            {
                reducer(key, intermediate[key]); 
            }

            foreach (var item in result)
            {
                Console.WriteLine(item.ToString());
            }
        }
    }
}
