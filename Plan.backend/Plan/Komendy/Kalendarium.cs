using Plan.Core.DTO;
using Plan.Core.Entities;
using Plan.Core.IServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace Plan.API.Komendy
{
    public class KomendaDodajZjazd
    {
        public DateTime DataOd { get; set; }
        public DateTime DataDo { get; set; }
        public RodzajSemestru RodzajSemestru { get; set; }
    }

    public class KomendaPrzypiszZjazdyGrupie
    {
        public string NrGrupy { get; set; }
        public ZjazdKolejny[] Zjazdy { get; set; }
    }

    public class KomendaPrzypiszGrupyDoZjazdu
    {
        public string[] Grupy { get; set; }
        public ZjazdKolejny Zjazd { get; set; }
    }

    public class KomendaUsunGrupyZZjazdu
    {
        public string[] Grupy { get; set; }
        public int NrKolejny { get; set; }
    }

}
