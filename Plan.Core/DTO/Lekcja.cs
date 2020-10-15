using System;
using System.Collections.Generic;
using System.Text;

namespace Plan.Core.DTO
{
    public class LekcjaWidokDTO
    {
        public string Nazwa { get; set; }
        public string Wykladowca { get; set; }
        public int Forma { get; set; }
        public string Od { get; set; }
        public string Do { get; set; }
        public string Sala { get; set; }
    }
}
