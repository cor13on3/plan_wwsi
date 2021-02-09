using Plan.Core.DTO;
using Plan.Core.Entities;

namespace Plan.Core.Zapytania
{
    public class ZapytanieSpecjalizacje : ZapytanieBase<Specjalizacja, SpecjalizacjaDTO>
    {
        public ZapytanieSpecjalizacje()
        {
            DodajMapowanie(x => new SpecjalizacjaDTO
            {
                Id = x.IdSpecjalizacji,
                Nazwa = x.Nazwa,
            });
        }
    }
}
