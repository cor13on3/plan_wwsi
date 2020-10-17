using Plan.Core.DTO;
using Plan.Core.Entities;
using System;

namespace Plan.Core.Zapytania
{
    public class ZapytaniePlanDnia : ZapytanieBase<LekcjaGrupa, LekcjaWidokDTO>
    {
        public ZapytaniePlanDnia(string nrGrupy, int nrZjazdu, int dzienTyg) :
            base(x => x.NrGrupy == nrGrupy && x.NrZjazdu == nrZjazdu && x.DzienTygodnia == (int)dzienTyg)
        {
            DodajSkladowa("Lekcja.Wykladowca");
            DodajSkladowa("Lekcja.Przedmiot");
            DodajSkladowa("Lekcja.Sala");
            DodajMapowanie(x => new LekcjaWidokDTO
            {
                Od = x.Lekcja.GodzinaOd,
                Do = x.Lekcja.GodzinaDo,
                Wykladowca = x.Lekcja.Wykladowca.Nazwisko,
                Nazwa = x.Lekcja.Przedmiot.Nazwa,
                Sala = x.Lekcja.Sala.Nazwa,
                Forma = x.Lekcja.Forma,
            });
        }
    }
}
