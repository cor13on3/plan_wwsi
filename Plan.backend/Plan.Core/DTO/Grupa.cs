using Plan.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Plan.Core.DTO
{
    public class GrupaDTO
    {
        public string Numer { get; set; }
        public int Semestr { get; set; }
        public TrybStudiow TrybStudiow { get; set; }
        public StopienStudiow StopienStudiow { get; set; }
    }
}
