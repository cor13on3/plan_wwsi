using System;
using System.Collections.Generic;
using System.Text;

namespace Plan.Core.Entities
{
    public class LekcjaGrupa
    {
        public int NrZjazdu { get; set; }
        public bool CzyOdpracowanie { get; set; }

        public int IdLekcji { get; set; }
        public Lekcja Lekcja { get; set; }

        public string NrGrupy { get; set; }
        public Grupa Grupa { get; set; }
    }
}
