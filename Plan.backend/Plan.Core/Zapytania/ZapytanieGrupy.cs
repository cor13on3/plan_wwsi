using Plan.Core.DTO;
using Plan.Core.Entities;

namespace Plan.Core.Zapytania
{
    public class ZapytanieGrupy : ZapytanieBase<Grupa,GrupaDTO>
    {
        public ZapytanieGrupy(TrybStudiow? tryb = null, StopienStudiow? stopien = null, int? semestr = null) : base(x => true)
        {
            UstawKryteria(x => (tryb == null || x.TrybStudiow == tryb) && (stopien == null || x.StopienStudiow == stopien) && (semestr == null || x.Semestr == semestr));
            DodajMapowanie(x => new GrupaDTO
            {
                Numer = x.NrGrupy,
                Semestr = x.Semestr,
                TrybStudiow = x.TrybStudiow,
                StopienStudiow = x.StopienStudiow
            });
        }
    }

    public class ZapytanieGrupa : ZapytanieBase<Grupa, Grupa>
    {
        public ZapytanieGrupa(string nr) : base(x => x.NrGrupy == nr)
        {
        }
    }
}
