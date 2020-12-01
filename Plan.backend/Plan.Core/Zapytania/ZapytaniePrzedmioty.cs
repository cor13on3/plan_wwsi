using Plan.Core.DTO;
using Plan.Core.Entities;

namespace Plan.Core.Zapytania
{
    public class ZapytaniePrzedmioty : ZapytanieBase<Przedmiot, PrzedmiotWidokDTO>
    {
        public ZapytaniePrzedmioty()
        {
            DodajMapowanie(x => new PrzedmiotWidokDTO
            {
                Id = x.IdPrzedmiotu,
                Nazwa = x.Nazwa,
            });
        }
    }
}
