﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Plan.Core.Entities
{
    public class Specjalnosc
    {
        public int IdSpecjalnosci { get; set; }
        public string Nazwa { get; set; }

        public List<WykladowcaSpecjalizacja> WyklSpecList { get; set; }
    }
}