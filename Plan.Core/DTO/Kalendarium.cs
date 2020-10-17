using Plan.Core.Entities;
using System;

namespace Plan.Core.DTO
{
    public class PropozycjaZjazduWidokDTO
    {
        public DateTime DataOd { get; set; }
        public DateTime DataDo { get; set; }
    }

    public class ZjazdWidokDTO
    {
        public int Nr { get; set; }
        public DateTime DataOd { get; set; }
        public DateTime DataDo { get; set; }
        public bool CzyOdpracowanie { get; set; }
    }

    public class ZjazdDTO
    {
        public DateTime DataOd { get; set; }
        public DateTime DataDo { get; set; }
        public RodzajSemestru RodzajSemestru { get; set; }
    }



    public class ZjazdKolejnyDTO
    {
        public int NrZjazdu { get; set; }
        public int IdZjazdu { get; set; }
        public bool CzyOdpracowanie { get; set; }
    }
}
