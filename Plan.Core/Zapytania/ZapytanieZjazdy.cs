using Plan.Core.DTO;
using Plan.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Plan.Core.Zapytania
{
    public class ZapytanieZjadyGrupy : ZapytanieBase<GrupaZjazd, ZjazdWidokDTO>
    {
        public ZapytanieZjadyGrupy(string nrGrupy, DateTime? data = null) :
            base(x => x.NrGrupy == nrGrupy &&
                      (data == null || x.Zjazd.DataOd <= data && data <= x.Zjazd.DataDo))
        {
            DodajSkladowa("Zjazd");
            DodajMapowanie(x => new ZjazdWidokDTO
            {
                Nr = x.NrZjazdu,
                DataOd = x.Zjazd.DataOd,
                DataDo = x.Zjazd.DataDo,
                CzyOdpracowanie = x.CzyOdpracowanie,
                IdZjazdu = x.IdZjazdu
            });
        }
    }

    public class ZapytanieZjadOTerminie : ZapytanieBase<Zjazd, Zjazd>
    {
        public ZapytanieZjadOTerminie(DateTime poczatek, DateTime koniec) :
            base(x => x.DataOd == poczatek && x.DataDo == koniec)
        {
        }
    }
}
