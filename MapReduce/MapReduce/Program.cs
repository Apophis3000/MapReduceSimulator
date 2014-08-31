using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapReduce
{
    public class Program
    {
        static MapReduce<KeyValuePair<string, string>> mr = new MapReduce<KeyValuePair<string, string>>();

        static void lc_map(KeyValuePair<string, string> record)
        {
            string key = record.Key; //Buchtitel
            string val = record.Value; //Alle Wörter im Buch / Wörter im Buchrücken o.ä.

            foreach (string word in val.Split(' '))
            {
                mr.Emit_Intermediate(word, 1); // 1 bzw. Integer entspricht dem Typen in der generischen Liste in lc_reduce
            }
        }

        static void lc_reduce(string key, List<object> values) // hier: object == integer
        {
            int total = 0; // Zu Beginn Wort 0 mal vorhanden

            foreach (var val in values)
            {
                total++;                
            }
            
            mr.Emit(string.Format("({0}, {1})", key, total)); //z.B.: (Auto, 1), (Fahrrad, 3), (hat, 2), ...
        }

        static void Main(string[] args)
        {
            List<KeyValuePair<string, string>> lc_in = new List<KeyValuePair<string, string>>();

            lc_in.Add(new KeyValuePair<string, string>("Game of Thrones", "Everyone dies in the end end end"));
            lc_in.Add(new KeyValuePair<string, string>("Metro2033", "Es ist das Jahr end 2033. Nach einem verheerenden Krieg liegen weite " +
             "Teile der Welt in Schutt und Asche...."));
            
            mr.Execute(lc_in, lc_map, lc_reduce);

            Console.ReadLine();
        }
    }
}
