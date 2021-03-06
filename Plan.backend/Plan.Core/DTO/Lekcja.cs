using Plan.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Plan.Core.DTO
{
    public class LekcjaWidokDTO
    {
        public int IdLekcji { get; set; }
        public string Nazwa { get; set; }
        public int IdWykladowcy { get; set; }
        public string Wykladowca { get; set; }
        public FormaLekcji Forma { get; set; }
        public string Od { get; set; }
        public string Do { get; set; }
        public string Sala { get; set; }
        public bool CzyOdpracowanie { get; set; }
        public int NrZjazdu { get; set; }
        public int DzienTygodnia { get; set; }
    }

    public class LekcjaDTO
    {
        public int IdLekcji { get; set; }
        public string GodzinaOd { get; set; }
        public string GodzinaDo { get; set; }
        public FormaLekcji Forma { get; set; }
        public int IdPrzedmiotu { get; set; }
        public string Przedmiot { get; set; }
        public int IdSali { get; set; }
        public string Sala { get; set; }
        public int IdWykladowcy { get; set; }
        public string Wykladowca { get; set; }
    }

    public class LekcjaWZjazdach
    {
        public int[] Zjazdy { get; set; }
        public LekcjaWidokDTO Lekcja { get; set; }
    }

    public class PlanDnia
    {
        public int DzienTygodnia { get; set; }
        public LekcjaWZjazdach[] Lekcje { get; set; }
    }
}
