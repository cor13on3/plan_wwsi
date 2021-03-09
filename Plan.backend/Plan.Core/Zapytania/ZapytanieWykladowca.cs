using Plan.Core.DTO;
using Plan.Core.Entities;
using System;
using System.Linq;

namespace Plan.Core.Zapytania
{
    public class ZapytanieWykladowca : ZapytanieBase<Wykladowca, WykladowcaDTO>
    {
        public int IdWykladowcy { get; set; }

        public ZapytanieWykladowca()
        {
            DolaczEncje("WyklSpecList.Specjalizacja");
            UstawKryteria(x => x.IdWykladowcy == IdWykladowcy);
            DodajMapowanie(x => new WykladowcaDTO
            {
                Specjalizacje = x.WyklSpecList.Select(y => new SpecjalizacjaDTO
                {
                    Id = y.IdSpecjalizacji,
                    Nazwa = y.Specjalizacja.Nazwa
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
        public ZapytanieWykladowcy(string fraza)
        {
            if (!string.IsNullOrWhiteSpace(fraza))
                UstawKryteria(x => x.Nazwisko.ToLower().Contains(fraza.ToLower()) || x.Email.ToLower().Contains(fraza.ToLower()) || x.Tytul.ToLower().Contains(fraza.ToLower()));
            DodajMapowanie(w => new WykladowcaWidokDTO
            {
                Id = w.IdWykladowcy,
                Nazwa = $"{w.Tytul} {w.Imie[0]}. {w.Nazwisko}",
                Email = w.Email,
                Nazwisko = w.Nazwisko
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
