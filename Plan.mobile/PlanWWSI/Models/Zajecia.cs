namespace App1.Models
{
    public class Zajecia : ModelBase
    {
        public string NumerGrupy { get; set; }
        public string Data { get; set; }
        public string Nazwa { get; set; }
        public string Godzina { get; set; }
    }

    public class ZajeciaSzczegoly : Zajecia
    {
        public string Wykladowca { get; set; }
        public string Sala { get; set; }
        public Zajecia[] Alternatywy { get; set; }
    }
}