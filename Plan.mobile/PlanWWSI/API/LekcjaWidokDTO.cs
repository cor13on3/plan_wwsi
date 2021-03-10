using PlanWWSI.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlanWWSI.API
{
    public class LekcjaWidokDTO
    {
        public int IdLekcji { get; set; }
        public string Nazwa { get; set; }
        public string Wykladowca { get; set; }
        public FormaLekcji Forma { get; set; }
        public string Od { get; set; }
        public string Do { get; set; }
        public string Sala { get; set; }
        public bool CzyOdpracowanie { get; set; }
        public int NrZjazdu { get; set; }
        public int DzienTygodnia { get; set; }
    }
}
