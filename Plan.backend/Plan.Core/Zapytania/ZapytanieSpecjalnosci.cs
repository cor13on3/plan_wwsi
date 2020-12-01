using Plan.Core.DTO;
using Plan.Core.Entities;

namespace Plan.Core.Zapytania
{
    public class ZapytanieSpecjalnosci : ZapytanieBase<Specjalnosc, SpecjalnoscDTO>
    {
        public ZapytanieSpecjalnosci()
        {
            DodajMapowanie(x => new SpecjalnoscDTO
            {
                Id = x.IdSpecjalnosci,
                Nazwa = x.Nazwa,
            });
        }
    }
}
