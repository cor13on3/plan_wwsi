using Plan.Core.DTO;
using Plan.Core.Entities;

namespace Plan.Core.Zapytania
{
    public class ZapytanieSale : ZapytanieBase<Sala, SalaWidokDTO>
    {
        public ZapytanieSale()
        {
            DodajMapowanie(x => new SalaWidokDTO
            {
                Id = x.IdSali,
                Nazwa = x.Nazwa,
                Rodzaj = x.Rodzaj
            });
        }
    }
}
