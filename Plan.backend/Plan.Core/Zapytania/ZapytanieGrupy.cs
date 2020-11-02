using Plan.Core.DTO;
using Plan.Core.Entities;

namespace Plan.Core.Zapytania
{
    public class ZapytanieGrupy : ZapytanieBase<Grupa,GrupaWidokDTO>
    {
        public ZapytanieGrupy(TrybStudiow? tryb = null, StopienStudiow? stopien = null, int? semestr = null) : base(x => true)
        {
            UstawKryteria(x => (tryb == null || x.TrybStudiow == tryb) && (stopien == null || x.StopienStudiow == stopien) && (semestr == null || x.Semestr == semestr));
            DodajMapowanie(x => new GrupaWidokDTO
            {
                Numer = x.NrGrupy
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
