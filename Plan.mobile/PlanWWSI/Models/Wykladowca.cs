namespace App1.Models
{
    public class Wykladowca : ModelBase
    {
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public string NazwaPelna { get; set; }
    }

    public class WykladowcaSzczegoly : Wykladowca
    {
        public string Specjalizacja { get; set; }
        public Zajecia[] Zajecia { get; set; }
    }
}