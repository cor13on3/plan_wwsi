using System;
using System.Collections.Generic;
using System.Text;

namespace Plan.API.Komendy
{
    public class ZjazdKolejnyDTO
    {
        public int NrZjazdu { get; set; }
        public int IdZjazdu { get; set; }
        public bool CzyOdpracowanie { get; set; }
    }

    public class KomendaPrzypiszZjazdyGrupie
    {
        public string NrGrupy { get; set; }
        public ZjazdKolejnyDTO[] Zjazdy { get; set; }
    }
}
