namespace Test2.Models
{
    public class GrupaZjazd
    {
        public short NrZjazdu { get; set; }

        public int IdZjazdu { get; set; }
        public Zjazd Zjazd { get; set; }

        public string NrGrupy { get; set; }
        public Grupa Grupa { get; set; }
    }
}
