using System;
using System.Collections.Generic;
using System.Text;

namespace Plan.Core.Entities
{
    public class Grupa
    {
        public string NrGrupy { get; set; }
        public int Semestr { get; set; }
        public TrybStudiow TrybStudiow { get; set; }
        public StopienStudiow StopienStudiow { get; set; }

        public List<GrupaZjazd> GrupaZjazdList { get; set; }
        public List<LekcjaGrupa> LekcjaGrupaList { get; set; }
    }
}
