using System.Collections.Generic;

namespace Test2.Models
{
    public class Przedmiot
    {
        public Przedmiot()
        {
            LekcjaList = new List<Lekcja>();
        }

        public short IdPrzedmiotu { get; set; }
        public string Nazwa { get; set; }
        public FormaPrzedmiotu Forma { get; set; }

        public List<Lekcja> LekcjaList { get; set; }
    }
}
