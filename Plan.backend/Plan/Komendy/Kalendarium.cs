using Plan.Core.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Plan.API.Komendy
{
    public class KomendaPrzypiszZjazdyGrupie
    {
        public string NrGrupy { get; set; }
        public ZjazdKolejnyDTO[] Zjazdy { get; set; }
    }
}
