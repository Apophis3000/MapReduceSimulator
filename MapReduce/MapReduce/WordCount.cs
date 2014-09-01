using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapReduce
{
    public static class WordCount
    {
        public static List<KeyValuePair<string, int>> Map(KeyValuePair<string, string> record)
        {
            List<KeyValuePair<string, int>> intermediate = new List<KeyValuePair<string, int>>();

            string key = record.Key; //Buchtitel
            string val = record.Value.Trim(); //Alle Wörter im Buch

            foreach (string word in val.Split(' '))
            {
                intermediate.Add(new KeyValuePair<string, int>(word, 1));
            }

            return intermediate;
        }
        public static string Reduce(string key, List<int> values)
        {
            //Zu Beginn Wort 0 mal vorhanden
            int total = 0;

            foreach (var val in values)
            {
                total += val;
            }

            //z.B.: (Auto, 1), (Fahrrad, 3), (hat, 2), ...
            return string.Format("({0}, {1})", key, total);
        }
    }
}