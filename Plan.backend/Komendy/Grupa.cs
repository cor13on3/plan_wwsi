using System;
using System.Collections.Generic;
using System.Text;

namespace Plan.API.Komendy
{
    // TODO: Skonfigurować działanie enum w komendach, żeby przekazywany był string
    public class KomendaDodajGrupe
    {
        public string NrGrupy { get; set; }
        public int Semestr { get; set; }
        public int TrybStudiow { get; set; }
        public int StopienStudiow { get; set; }
    }
}
