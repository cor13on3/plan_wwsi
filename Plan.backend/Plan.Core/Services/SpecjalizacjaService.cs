using Plan.Core.DTO;
using Plan.Core.Entities;
using Plan.Core.Exceptions;
using Plan.Core.IDatabase;
using Plan.Core.IServices;
using Plan.Core.Zapytania;
using System.Linq;

namespace Plan.Core.Services
{
    public class SpecjalizacjaService : ISpecjalizacjaService
    {
        private IBazaDanych _baza;

        public SpecjalizacjaService(IBazaDanych baza)
        {
            _baza = baza;
        }

        public void Dodaj(string nazwa)
        {
            if (string.IsNullOrEmpty(nazwa))
                throw new BladBiznesowy("Podaj nazwę");
            var repo = _baza.DajRepozytorium<Specjalizacja>();
            repo.Dodaj(new Specjalizacja
            {
                Nazwa = nazwa,
            });
            _baza.Zapisz();
        }

        public SpecjalizacjaDTO[] Przegladaj()
        {
            var repo = _baza.DajRepozytorium<Specjalizacja>();
            var wynik = repo.Wybierz(new ZapytanieSpecjalizacje());
            return wynik.ToArray();
        }

        public void Usun(int id)
        {
            var repo = _baza.DajRepozytorium<Specjalizacja>();
            var spec = repo.Znajdz(id);
            if (spec == null)
                throw new BladBiznesowy($"Specjalizacja o id {id} nie istnieje");
            repo.Usun(spec);
            _baza.Zapisz();
        }
    }
}
