using System;

namespace Plan.Core.DTO
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
