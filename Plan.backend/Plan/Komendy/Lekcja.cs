using Plan.Core.Entities;

namespace Plan.API.Komendy
{
    public class KomendaDodajLekcje
    {
        public int IdPrzedmiotu { get; set; }
        public int IdWykladowcy { get; set; }
        public int IdSali { get; set; }
        public string GodzinaOd { get; set; }
        public string GodzinaDo { get; set; }
        public FormaLekcji Forma { get; set; }
    }

    public class KomendaPrzypiszGrupyLekcji
    {
        public int IdLekcji { get; set; }
        public string[] NrGrup { get; set; }
        public int NrZjazdu { get; set; }
        public int DzienTygodnia { get; set; }
        public bool CzyOdpracowanie { get; set; }
    }
}
