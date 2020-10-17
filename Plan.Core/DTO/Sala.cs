using Plan.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Plan.Core.DTO
{
    public class SalaWidokDTO
    {
        public int Id { get; set; }
        public string Nazwa { get; set; }
        public RodzajSali Rodzaj { get; set; }
    }
}
