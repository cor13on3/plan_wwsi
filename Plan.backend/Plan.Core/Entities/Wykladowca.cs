using System;
using System.Collections.Generic;
using System.Text;

namespace Plan.Core.Entities
{
    public class Wykladowca
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
        public List<WykladowcaSpecjalizacja> WyklSpecList { get; set; }
    }
}
