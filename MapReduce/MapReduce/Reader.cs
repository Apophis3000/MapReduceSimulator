using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapReduce
{
    public class Reader
    {
        public string FilePath
        {
            get;
            set;
        }
        public string Dataname
        {
            get;
            set;
        }

        public Reader(string filePath, string dataname)
        {
            if (File.Exists(Path.Combine(filePath, dataname)))
            {
                FilePath = filePath;
                Dataname = dataname;
            }
            else
            {
                FilePath = string.Empty;
                Dataname = string.Empty;
            }
        }

        public bool ChangePath(string filePath, string dataname)
        {
            if (!File.Exists(Path.Combine(filePath, dataname)))
            {
                return false;
            }

            FilePath = filePath;
            Dataname = dataname;

            return true;
        }
        public string[] GetContent()
        {
            try
            {
                string[] lines = File.ReadAllLines(Path.Combine(FilePath, Dataname));

                return lines;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);

                return null;
            }
        }
    }
}