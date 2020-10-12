using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test2.Entities;

namespace Test2.Models
{
    public class LekcjaView
    {
        public string Nazwa { get; set; }
        public string Wykladowca { get; set; }
        public FormaLekcji Forma { get; set; }
        public string Od { get; set; }
        public string Do { get; set; }
        public string Sala { get; set; }
    }
}
