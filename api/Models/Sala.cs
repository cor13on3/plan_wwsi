using System.Collections.Generic;

namespace Test2.Models
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
