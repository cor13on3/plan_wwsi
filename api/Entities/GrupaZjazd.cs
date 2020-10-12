namespace Test2.Entities
{
    public class GrupaZjazd
    {
        public int NrZjazdu { get; set; }
        public bool CzyOdpracowanie { get; set; }

        public int IdZjazdu { get; set; }
        public Zjazd Zjazd { get; set; }

        public string NrGrupy { get; set; }
        public Grupa Grupa { get; set; }
    }
}
