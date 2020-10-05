using System.Collections.Generic;

namespace Test2.Entities
{
    public class Wykladowca : Entity
    {
        public Wykladowca()
        {
            LekcjaList = new List<Lekcja>();
        }
        public int IdWykladowcy { get; set; }
        public string Tytul { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public string Email { get; set; }

        public List<Lekcja> LekcjaList { get; set; }
        public List<WyklSpec> WyklSpecList { get; set; }
    }
}
