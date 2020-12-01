using Plan.Core.DTO;
using Plan.Core.Entities;
using System;

namespace Plan.Core.Zapytania
{
    public class ZapytanieZjadyGrupy : ZapytanieBase<GrupaZjazd, ZjazdWidokDTO>
    {
        public string NumerGrupy { get; set; }
        public DateTime? Data { get; set; }

        public ZapytanieZjadyGrupy()
        {
            DolaczEncje("Zjazd");
            UstawKryteria(x => (NumerGrupy == null || x.NrGrupy == NumerGrupy) &&
                               (Data == null || (x.Zjazd.DataOd <= Data && Data <= x.Zjazd.DataDo)));
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
        public DateTime Poczatek { get; set; }
        public DateTime Koniec { get; set; }

        public ZapytanieZjadOTerminie()
        {
            UstawKryteria(x => x.DataOd == Poczatek && x.DataDo == Koniec);
            DodajMapowanie(x => new Zjazd
            {
                IdZjazdu = x.IdZjazdu
            });
        }
    }

    public class ZapytanieZjady : ZapytanieBase<Zjazd, ZjazdWidokDTO>
    {
        public ZapytanieZjady()
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
