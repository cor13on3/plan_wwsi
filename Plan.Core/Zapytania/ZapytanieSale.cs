using Plan.Core.DTO;
using Plan.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Plan.Core.Zapytania
{
    public class ZapytanieSale : ZapytanieBase<Sala, SalaWidokDTO>
    {
        public ZapytanieSale() :
            base(x => true)
        {
            DodajMapowanie(x => new SalaWidokDTO
            {
                Id = x.IdSali,
                Nazwa = x.Nazwa,
                Rodzaj = x.Rodzaj
            });
        }
    }
}
