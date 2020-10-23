using System;
using System.Collections.Generic;
using System.Text;

namespace Plan.Core.Entities
{
    public class Lekcja
    {
        public int IdLekcji { get; set; }
        public string GodzinaOd { get; set; }
        public string GodzinaDo { get; set; }
        public FormaLekcji Forma { get; set; }

        public int IdPrzedmiotu { get; set; }
        public Przedmiot Przedmiot { get; set; }

        public int IdSali { get; set; }
        public Sala Sala { get; set; }

        public int IdWykladowcy { get; set; }
        public Wykladowca Wykladowca { get; set; }

        public List<LekcjaGrupa> LekcjaGrupaList { get; set; }
    }
}
