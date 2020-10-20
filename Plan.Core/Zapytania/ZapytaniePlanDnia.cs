using Plan.Core.DTO;
using Plan.Core.Entities;

namespace Plan.Core.Zapytania
{
    public class ZapytaniePlanDnia : ZapytanieBase<LekcjaGrupa, LekcjaWidokDTO>
    {
        public string NrGrupy { get; set; }
        public int NrZjazdu { get; set; }
        public int DzienTygodnia { get; set; }

        public ZapytaniePlanDnia()
        {
            UstawKryteria(x => x.NrGrupy == NrGrupy && x.NrZjazdu == NrZjazdu && x.DzienTygodnia == (int)DzienTygodnia);
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
                CzyOdpracowanie = x.CzyOdpracowanie
            });
        }
    }
}
