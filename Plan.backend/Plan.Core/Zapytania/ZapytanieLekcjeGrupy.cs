using Plan.Core.DTO;
using Plan.Core.Entities;

namespace Plan.Core.Zapytania
{
    public class ZapytanieLekcjeGrupy : ZapytanieBase<LekcjaGrupa, LekcjaWidokDTO>
    {
        public string NrGrupy { get; set; }
        public int? NrZjazdu { get; set; }
        public int? DzienTygodnia { get; set; }

        public ZapytanieLekcjeGrupy()
        {
            UstawKryteria(x => x.NrGrupy == NrGrupy &&
                (DzienTygodnia == null || x.DzienTygodnia == DzienTygodnia) &&
                (NrZjazdu == null || x.NrZjazdu == NrZjazdu));
            DodajSkladowa("Lekcja.Wykladowca");
            DodajSkladowa("Lekcja.Przedmiot");
            DodajSkladowa("Lekcja.Sala");
            DodajMapowanie(x => new LekcjaWidokDTO
            {
                IdLekcji = x.Lekcja.IdLekcji,
                Od = x.Lekcja.GodzinaOd,
                Do = x.Lekcja.GodzinaDo,
                Wykladowca = $"{x.Lekcja.Wykladowca.Tytul}. {x.Lekcja.Wykladowca.Imie[0]} {x.Lekcja.Wykladowca.Nazwisko}",
                Nazwa = x.Lekcja.Przedmiot.Nazwa,
                Sala = x.Lekcja.Sala.Nazwa,
                Forma = x.Lekcja.Forma,
                CzyOdpracowanie = x.CzyOdpracowanie,
                NrZjazdu = x.NrZjazdu,
                DzienTygodnia = x.DzienTygodnia
            });
        }
    }
}
