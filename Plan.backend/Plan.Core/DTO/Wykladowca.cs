using System;
using System.Collections.Generic;
using System.Text;

namespace Plan.Core.DTO
{
    public class SpecjalizacjaDTO
    {
        public int Id { get; set; }
        public string Nazwa { get; set; }
    }

    public class WykladowcaWidokDTO
    {
        public int Id { get; set; }
        public string Nazwa { get; set; }
        public string Email { get; set; }
        public string Nazwisko { get; set; }
    }

    public class WykladowcaDTO
    {
        public int Id { get; set; }
        public string Tytul { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public string Email { get; set; }
        public SpecjalizacjaDTO[] Specjalizacje { get; set; }
    }
}
