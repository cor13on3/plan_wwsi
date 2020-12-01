using Plan.Core.DTO;
using Plan.Core.Entities;

namespace Plan.Core.Zapytania
{
    public class ZapytanieLekcje : ZapytanieBase<LekcjaGrupa, LekcjaDTO>
    {
        public TrybStudiow Tryb { get; set; }
        public int Semestr { get; set; }
        public int DzienTygodnia { get; set; }

        public ZapytanieLekcje()
        {
            DolaczEncje("Grupa");
            DolaczEncje("Lekcja.Wykladowca");
            DolaczEncje("Lekcja.Przedmiot");
            DolaczEncje("Lekcja.Sala");
            UstawKryteria(x => x.Grupa.TrybStudiow == Tryb && x.Grupa.Semestr == Semestr && x.DzienTygodnia == DzienTygodnia);
            DodajMapowanie(x => new LekcjaDTO
            {
                IdLekcji = x.Lekcja.IdLekcji,
                GodzinaOd = x.Lekcja.GodzinaOd,
                GodzinaDo = x.Lekcja.GodzinaDo,
                Wykladowca = $"{x.Lekcja.Wykladowca.Tytul}. {x.Lekcja.Wykladowca.Imie[0]} {x.Lekcja.Wykladowca.Nazwisko}",
                IdWykladowcy = x.Lekcja.Wykladowca.IdWykladowcy,
                Sala = x.Lekcja.Sala.Nazwa,
                IdSali = x.Lekcja.IdSali,
                Przedmiot = x.Lekcja.Przedmiot.Nazwa,
                IdPrzedmiotu = x.Lekcja.IdPrzedmiotu,
                Forma = x.Lekcja.Forma,
            });
        }
    }
}
