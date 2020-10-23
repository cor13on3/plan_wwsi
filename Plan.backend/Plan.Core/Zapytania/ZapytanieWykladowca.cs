using Plan.Core.DTO;
using Plan.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Plan.Core.Zapytania
{
    public class ZapytanieWykladowca : ZapytanieBase<Wykladowca, WykladowcaDTO>
    {
        public ZapytanieWykladowca(int id) :
            base(x => x.IdWykladowcy == id)
        {
            DodajSkladowa("WyklSpecList.Specjalnosc");
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
        public ZapytanieWykladowcy() :
            base(x => true)
        {
            DodajMapowanie(w => new WykladowcaWidokDTO
            {
                Id = w.IdWykladowcy,
                Nazwa = $"{w.Tytul}. {w.Imie[0]} {w.Nazwisko}",
                Email = w.Email
            });
        }
    }

    public class ZapytanieWykladowcaSpecjalizacja : ZapytanieBase<WykladowcaSpecjalizacja, WykladowcaSpecjalizacja>
    {
        public ZapytanieWykladowcaSpecjalizacja() :
            base(x => true)
        {
        }
    }
}
