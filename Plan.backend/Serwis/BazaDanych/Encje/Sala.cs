using Plan.Interfejsy;
using System;
using System.Collections.Generic;
using System.Text;

namespace Plan.Serwis.BazaDanych.Encje
{
    public class Sala
    {
        public Sala()
        {
            LekcjaList = new List<Lekcja>();
        }

        public int IdSali { get; set; }
        public string Nazwa { get; set; }
        public RodzajSali Rodzaj { get; set; }

        public List<Lekcja> LekcjaList { get; set; }
    }
}
