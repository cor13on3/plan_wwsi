namespace Test2.Entities
{
    public class LekcjaGrupa
    {
        public int NrZjazdu { get; set; }
        public int DzienTygodnia { get; set; }
        public bool CzyOdpracowanie { get; set; }

        public int IdLekcji { get; set; }
        public Lekcja Lekcja { get; set; }

        public string NrGrupy { get; set; }
        public Grupa Grupa { get; set; }
    }
}
