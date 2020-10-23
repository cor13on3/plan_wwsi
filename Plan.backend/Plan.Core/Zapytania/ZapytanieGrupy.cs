using Plan.Core.DTO;
using Plan.Core.Entities;

namespace Plan.Core.Zapytania
{
    public class ZapytanieGrupy : ZapytanieBase<Grupa,GrupaWidokDTO>
    {
        public ZapytanieGrupy() : base(x => true)
        {
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
