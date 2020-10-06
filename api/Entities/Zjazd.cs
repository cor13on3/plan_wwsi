using System;
using System.Collections.Generic;

namespace Test2.Entities
{
    public class Zjazd
    {
        public int IdZjazdu { get; set; }
        public DateTime DataOd { get; set; }
        public DateTime DataDo { get; set; }
        public RodzajSemestru RodzajSemestru { get; set; }
        public DateTime? DzienOdpracowywany { get; set; }

        public List<GrupaZjazd> GrupaZjazdList { get; set; }
    }
}
