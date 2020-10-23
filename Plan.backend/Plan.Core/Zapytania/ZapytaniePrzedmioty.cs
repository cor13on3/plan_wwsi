using Plan.Core.DTO;
using Plan.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Plan.Core.Zapytania
{
    public class ZapytaniePrzedmioty : ZapytanieBase<Przedmiot, PrzedmiotWidokDTO>
    {
        public ZapytaniePrzedmioty() :
            base(x => true)
        {
            DodajMapowanie(x => new PrzedmiotWidokDTO
            {
                Id = x.IdPrzedmiotu,
                Nazwa = x.Nazwa,
            });
        }
    }
}
