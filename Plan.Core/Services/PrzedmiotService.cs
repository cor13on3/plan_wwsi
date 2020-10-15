using Plan.Core.DTO;
using Plan.Core.Entities;
using Plan.Core.IDatabase;
using Plan.Core.IServices;
using System.Linq;

namespace Plan.Core.Services
{
    public class PrzedmiotService : IPrzedmiotService
    {
        private IBazaDanych _baza;

        public PrzedmiotService(IBazaDanych baza)
        {
            _baza = baza;
        }

        public void Dodaj(string nazwa)
        {
            var przedmiot = _baza.Daj<Przedmiot>();
            przedmiot.Dodaj(new Przedmiot
            {
                Nazwa = nazwa,
            });
            _baza.Zapisz();
        }

        public PrzedmiotWidokDTO[] Przegladaj()
        {
            var przedmiot = _baza.Daj<Przedmiot>();
            return przedmiot.Przegladaj()
                .Select(x => new PrzedmiotWidokDTO
                {
                    Id = x.IdPrzedmiotu,
                    Nazwa = x.Nazwa,
                })
                .ToArray();
        }

        public void Usun(int id)
        {
            var przedmiot = _baza.Daj<Przedmiot>();
            przedmiot.Usun(id);
            _baza.Zapisz();
        }
    }
}
