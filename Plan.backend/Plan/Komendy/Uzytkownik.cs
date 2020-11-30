using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Plan.Komendy
{
    public class KomendaZaloguj
    {
        public string Email { get; set; }
        public string Haslo { get; set; }
    }

    public class KomendaDodajUzytkownika
    {
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public string Email { get; set; }
        public string Haslo { get; set; }
    }
}
