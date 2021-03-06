using System;
using System.Collections.Generic;
using System.Text;

namespace Plan.Core.Entities
{
    public class Przedmiot
    {
        public Przedmiot()
        {
            LekcjaList = new List<Lekcja>();
        }

        public int IdPrzedmiotu { get; set; }
        public string Nazwa { get; set; }

        public List<Lekcja> LekcjaList { get; set; }
    }
}
