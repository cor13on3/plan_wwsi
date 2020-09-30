using System;
using System.Collections.Generic;

namespace Test2.Models
{
    public class Lekcja
    {
        public int IdLekcji { get; set; }
        public TimeSpan CzasOd { get; set; }
        public TimeSpan CzasDo { get; set; }
        public bool CzyOdpracowanie { get; set; }

        public short IdPrzedmiotu { get; set; }
        public Przedmiot Przedmiot { get; set; }

        public int IdSali { get; set; }
        public Sala Sala { get; set; }

        public int IdWykladowcy { get; set; }
        public Wykladowca Wykladowca { get; set; }

        public List<LekcjaGrupa> LekcjaGrupaList { get; set; }
    }
}
