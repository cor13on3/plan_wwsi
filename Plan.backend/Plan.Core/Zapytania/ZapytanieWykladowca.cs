using Plan.Core.DTO;
using Plan.Core.Entities;
using System.Linq;

namespace Plan.Core.Zapytania
{
    public class ZapytanieWykladowca : ZapytanieBase<Wykladowca, WykladowcaDTO>
    {
        public int IdWykladowcy { get; set; }

        public ZapytanieWykladowca()
        {
            DolaczEncje("WyklSpecList.Specjalnosc");
            UstawKryteria(x => x.IdWykladowcy == IdWykladowcy);
            DodajMapowanie(x => new WykladowcaDTO
            {
                Specjalnosci = x.WyklSpecList.Select(y => new SpecjalnoscDTO
                {
                    Id = y.IdSpecjalnosci,
                    Nazwa = y.Specjalnosc.Nazwa
                }).ToArray(),
                Imie = x.Imie,
                Id = x.IdWykladowcy,
                Nazwisko = x.Nazwisko,
                Email = x.Email,
                Tytul = x.Tytul,
            });
        }
    }

    public class ZapytanieWykladowcy : ZapytanieBase<Wykladowca, WykladowcaWidokDTO>
    {
        public ZapytanieWykladowcy()
        {
            DodajMapowanie(w => new WykladowcaWidokDTO
            {
                Id = w.IdWykladowcy,
                Nazwa = $"{w.Tytul} {w.Imie[0]}. {w.Nazwisko}",
                Email = w.Email
            });
        }
    }

    public class ZapytanieWykladowcaSpecjalizacja : ZapytanieBase<WykladowcaSpecjalizacja, WykladowcaSpecjalizacja>
    {
        public int IdWykladowcy { get; set; }

        public ZapytanieWykladowcaSpecjalizacja()
        {
            UstawKryteria(x => x.IdWykladowcy == IdWykladowcy);
            DodajMapowanie(x => x);
        }
    }
}
