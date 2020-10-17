using Plan.Core.Entities;

namespace Plan.API.Komendy
{
    public class KomendaDodajGrupe
    {
        public string NrGrupy { get; set; }
        public int Semestr { get; set; }
        public TrybStudiow TrybStudiow { get; set; }
        public StopienStudiow StopienStudiow { get; set; }
    }
}
