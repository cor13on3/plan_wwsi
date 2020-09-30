using System;
using System.Collections.Generic;

namespace Test2.Models
{
    public class Zjazd
    {
        public int IdZjazdu { get; set; }
        public DateTime DataOd { get; set; }
        public DateTime DataDo { get; set; }
        public RodzajSemestru Semestr { get; set; }

        public List<GrupaZjazd> GrupaZjazdList { get; set; }
    }
}
