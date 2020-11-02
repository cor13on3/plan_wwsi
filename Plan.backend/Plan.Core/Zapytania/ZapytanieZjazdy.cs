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
            base(x => (nrGrupy == null ||  x.NrGrupy == nrGrupy) &&
                      (data == null || (x.Zjazd.DataOd <= data && data <= x.Zjazd.DataDo)))
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
            DodajMapowanie(x => new Zjazd
            {
                IdZjazdu = x.IdZjazdu
            });
        }
    }

    public class ZapytanieZjady: ZapytanieBase<Zjazd, ZjazdWidokDTO>
    {
        public ZapytanieZjady() : base(x => true)
        {
            DodajMapowanie(x => new ZjazdWidokDTO
            {
                IdZjazdu = x.IdZjazdu,
                DataOd = x.DataOd,
                DataDo = x.DataDo,
            });
        }
    }
}
