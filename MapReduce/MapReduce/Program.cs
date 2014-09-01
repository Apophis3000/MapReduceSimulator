using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapReduce
{
    public class Program
    {
        static void Main(string[] args)
        {
            //Datei auslesen
            //Multithreading beginnen
        }
    }
}
/*
 * Multithreading einbauen
 *  - 3 Bücher, je eins in einen Thread auf Service aufrufen, warten bis alle fertig sind zum weiteren abarbeiten (MAP)
 *  - lokal zusammenfassen gleicher Worte (Werte addieren)
 *  - alle Wörter mit mehr als 1 in neuem Thread auf Service aufrufen, warten bis freie Ressource und nächsten Aufruf starten bis alle abgearbeitet (REDUCE)
 * Ausgabe auf Konsole
 * WCF-Dienst einbinden
 * 
 * Präsentation
 *  - Threading
 *  - Verbindung zum Server von Client aus
*/