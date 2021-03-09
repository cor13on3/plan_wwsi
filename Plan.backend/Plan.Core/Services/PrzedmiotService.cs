using Plan.Core.DTO;
using Plan.Core.Entities;
using Plan.Core.Exceptions;
using Plan.Core.IDatabase;
using Plan.Core.IServices;
using Plan.Core.Zapytania;
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
            if (string.IsNullOrEmpty(nazwa))
                throw new BladBiznesowy("Podaj nazwę");
            var repo = _baza.DajRepozytorium<Przedmiot>();
            repo.Dodaj(new Przedmiot
            {
                Nazwa = nazwa,
            });
            _baza.Zapisz();
        }

        public PrzedmiotWidokDTO[] Przegladaj()
        {
            var repo = _baza.DajRepozytorium<Przedmiot>();
            var wynik = repo.Wybierz(new ZapytaniePrzedmioty());
            return wynik.OrderBy(x => x.Nazwa).ToArray();
        }

        public void Usun(int id)
        {
            var repo = _baza.DajRepozytorium<Przedmiot>();
            var przedmiot = repo.Znajdz(id);
            if (przedmiot == null)
                throw new BladBiznesowy($"Przedmiot o id {id} nie istnieje");
            repo.Usun(przedmiot);
            _baza.Zapisz();
        }
    }
}
