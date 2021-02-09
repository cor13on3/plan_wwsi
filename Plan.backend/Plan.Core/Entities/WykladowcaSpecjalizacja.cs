using System;
using System.Collections.Generic;
using System.Text;

namespace Plan.Core.Entities
{
    public class WykladowcaSpecjalizacja
    {
        public int IdWykladowcy { get; set; }
        public Wykladowca Wykladowca { get; set; }

        public int IdSpecjalizacji { get; set; }
        public Specjalizacja Specjalizacja { get; set; }
    }
}
