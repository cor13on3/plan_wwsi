using Plan.Core.DTO;
using Plan.Core.Entities;
using System.Linq;

namespace Plan.Core.Zapytania
{
    public class ZapytanieLekcjeGrupy : ZapytanieBase<LekcjaGrupa, LekcjaWidokDTO>
    {
        public string NrGrupy { get; set; }
        public int? NrZjazdu { get; set; }
        public int? DzienTygodnia { get; set; }

        public ZapytanieLekcjeGrupy()
        {
            DolaczEncje("Lekcja.Wykladowca");
            DolaczEncje("Lekcja.Przedmiot");
            DolaczEncje("Lekcja.Sala");
            UstawKryteria(x => x.NrGrupy == NrGrupy &&
                              (DzienTygodnia == null || x.Lekcja.DzienTygodnia == DzienTygodnia) &&
                              (NrZjazdu == null || x.NrZjazdu == NrZjazdu));
            DodajMapowanie(x => new LekcjaWidokDTO
            {
                IdLekcji = x.Lekcja.IdLekcji,
                Od = x.Lekcja.GodzinaOd,
                Do = x.Lekcja.GodzinaDo,
                IdWykladowcy = x.Lekcja.Wykladowca.IdWykladowcy,
                Wykladowca = $"{x.Lekcja.Wykladowca.Tytul}. {x.Lekcja.Wykladowca.Imie[0]} {x.Lekcja.Wykladowca.Nazwisko}",
                Nazwa = x.Lekcja.Przedmiot.Nazwa,
                Sala = x.Lekcja.Sala.Nazwa,
                Forma = x.Lekcja.Forma,
                CzyOdpracowanie = x.CzyOdpracowanie,
                NrZjazdu = x.NrZjazdu,
                DzienTygodnia = x.Lekcja.DzienTygodnia
            });
        }
    }
}
