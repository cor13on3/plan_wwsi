using System;
using System.Collections.Generic;
using System.Text;

namespace Plan.API.Komendy
{
    public class KomendaDodajWykladowce
    {
        public string Tytul { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public string Email { get; set; }
        public int[] Specjalizacje { get; set; }
    }

    public class KomendaEdytujWykladowce
    {
        public string Tytul { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public string Email { get; set; }
        public int[] Specjalizacje { get; set; }
    }
}
