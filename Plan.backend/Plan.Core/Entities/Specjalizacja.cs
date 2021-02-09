using System;
using System.Collections.Generic;
using System.Text;

namespace Plan.Core.Entities
{
    public class Specjalizacja
    {
        public int IdSpecjalizacji { get; set; }
        public string Nazwa { get; set; }

        public List<WykladowcaSpecjalizacja> WyklSpecList { get; set; }
    }
}
