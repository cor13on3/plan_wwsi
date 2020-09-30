using System.Collections.Generic;

namespace Test2.Models
{
    public class Grupa
    {
        public string NrGrupy { get; set; }
        public short Semestr { get; set; }
        public TrybStudiow Tryb { get; set; }
        public StopienStudiow Stopien { get; set; }

        public List<GrupaZjazd> GrupaZjazdList { get; set; }
        public List<LekcjaGrupa> LekcjaGrupaList { get; set; }
    }
}
