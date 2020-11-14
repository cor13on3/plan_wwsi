using System;
using System.Collections.Generic;
using System.Text;

namespace PlanWWSI.Models
{
    public class ZjazdWidokDTO
    {
        public int Nr { get; set; }
        public DateTime DataOd { get; set; }
        public DateTime DataDo { get; set; }
        public bool CzyOdpracowanie { get; set; }
        public int IdZjazdu { get; set; }
    }
}
