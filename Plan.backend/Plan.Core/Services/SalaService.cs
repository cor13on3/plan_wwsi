using Plan.Core.DTO;
using Plan.Core.Entities;
using Plan.Core.Exceptions;
using Plan.Core.IDatabase;
using Plan.Core.IServices;
using Plan.Core.Zapytania;
using System.Linq;

namespace Plan.Core.Services
{
    public class SalaService : ISalaService
    {
        private IBazaDanych _baza;

        public SalaService(IBazaDanych baza)
        {
            _baza = baza;
        }

        public void Dodaj(string nazwa, RodzajSali rodzajSali)
        {
            if (string.IsNullOrEmpty(nazwa))
                throw new BladBiznesowy("Podaj nazwę");
            _baza.DajTabele<Sala>().Dodaj(new Sala
            {
                Nazwa = nazwa,
                Rodzaj = rodzajSali
            });
            _baza.Zapisz();
        }

        public SalaWidokDTO[] Przegladaj()
        {
            var wynik = _baza.DajTabele<Sala>().Wybierz(new ZapytanieSale());
            return wynik.ToArray();
        }

        public void Usun(int id)
        {
            IRepozytorium<Sala> repo = _baza.DajTabele<Sala>();
            if (repo.Znajdz(id) == null)
                throw new BladBiznesowy($"Sala o id {id} nie istnieje");
            repo.Usun(id);
            _baza.Zapisz();
        }
    }
}
