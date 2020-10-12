using Plan.Interfejsy;
using System;
using System.Collections.Generic;
using System.Text;

namespace Plan.Serwis.BazaDanych.Encje
{
    public class Zjazd
    {
        public int IdZjazdu { get; set; }
        public DateTime DataOd { get; set; }
        public DateTime DataDo { get; set; }
        public RodzajSemestru RodzajSemestru { get; set; }

        public List<GrupaZjazd> GrupaZjazdList { get; set; }
    }
}
