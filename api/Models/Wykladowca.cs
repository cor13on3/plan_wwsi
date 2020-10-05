using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Test2.Models
{
    public class WykladowcaWidokDTO
    {
        public int Id { get; set; }
        public string Nazwa { get; set; }
        public string Email { get; set; }
    }

    public class WykladowcaDTO
    {
        public int Id { get; set; }
        public string Tytul { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public string Email { get; set; }
        public SpecjalnoscDTO[] Specjalnosci { get; set; }
    }
}
