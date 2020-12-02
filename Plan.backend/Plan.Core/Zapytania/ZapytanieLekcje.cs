using Plan.Core.DTO;
using Plan.Core.Entities;

namespace Plan.Core.Zapytania
{
    public class ZapytanieLekcje : ZapytanieBase<LekcjaGrupa, LekcjaDTO>
    {
        public ZapytanieLekcje(TrybStudiow tryb, int semestr, int dzienTygodnia)
        {
            DolaczEncje("Grupa");
            DolaczEncje("Lekcja.Wykladowca");
            DolaczEncje("Lekcja.Przedmiot");
            DolaczEncje("Lekcja.Sala");
            UstawKryteria(x => x.Grupa.TrybStudiow == tryb && x.Grupa.Semestr == semestr && x.DzienTygodnia == dzienTygodnia);
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
