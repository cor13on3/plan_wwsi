using System;
using System.Collections.Generic;
using System.Text;

namespace Plan.Serwis.BazaDanych.Encje
{
    public class WykladowcaSpecjalizacja
    {
        public int IdWykladowcy { get; set; }
        public Wykladowca Wykladowca { get; set; }

        public int IdSpecjalnosci { get; set; }
        public Specjalnosc Specjalnosc { get; set; }
    }
}
