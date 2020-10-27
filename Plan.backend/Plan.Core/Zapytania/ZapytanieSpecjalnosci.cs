﻿using Plan.Core.DTO;
using Plan.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Plan.Core.Zapytania
{
    public class ZapytanieSpecjalnosci : ZapytanieBase<Specjalnosc, SpecjalnoscWidokDTO>
    {
        public ZapytanieSpecjalnosci() :
            base(x => true)
        {
            DodajMapowanie(x => new SpecjalnoscWidokDTO
            {
                Id = x.IdSpecjalnosci,
                Nazwa = x.Nazwa,
            });
        }
    }
}
