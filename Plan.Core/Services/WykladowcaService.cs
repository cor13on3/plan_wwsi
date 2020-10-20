using Plan.Core.DTO;
using Plan.Core.Entities;
using Plan.Core.Exceptions;
using Plan.Core.IDatabase;
using Plan.Core.IServices;
using Plan.Core.Zapytania;
using System.Linq;

namespace Plan.Core.Services
{
    public class WykladowcaService : IWykladowcaService
    {
        private IBazaDanych _baza;

        public WykladowcaService(IBazaDanych baza)
        {
            _baza = baza;
        }

        public WykladowcaDTO Daj(int id)
        {
            var wynik = _baza.Daj<Wykladowca>().Wybierz(new ZapytanieWykladowca(id));
            if (wynik.Count() == 0)
                throw new BladBiznesowy($"Wykładowca o id {id} nie istnieje.");
            return wynik.First();
        }

        public WykladowcaWidokDTO[] DajWykladowcow(string fraza = null)
        {
            var wynik = _baza.Daj<Wykladowca>().Wybierz(new ZapytanieWykladowcy());
            return wynik.ToArray();
        }

        public void DodajWykladowce(string tytul, string imie, string nazwisko, string email, int[] idSpecjalnosci)
        {
            if (string.IsNullOrEmpty(imie) || string.IsNullOrEmpty(nazwisko) || string.IsNullOrEmpty(email))
                throw new BladBiznesowy("Uzupełnij komplet informacji");
            var wykladowca = new Wykladowca
            {
                Imie = imie,
                Nazwisko = nazwisko,
                Tytul = tytul,
                Email = email,
            };
            _baza.Daj<Wykladowca>().Dodaj(wykladowca);
            foreach (var id in idSpecjalnosci)
            {
                var specjalnosc = _baza.Daj<Specjalnosc>().Znajdz(id);
                if (specjalnosc == null)
                    throw new BladBiznesowy($"Specjalność o id {id} nie istnieje.");
                var ws = new WykladowcaSpecjalizacja
                {
                    Wykladowca = wykladowca,
                    Specjalnosc = specjalnosc
                };
                _baza.Daj<WykladowcaSpecjalizacja>().Dodaj(ws);
            }
            _baza.Zapisz();
        }

        public void UsunWykladowce(int id)
        {
            var repo = _baza.Daj<Wykladowca>();
            var wykladowca = repo.Znajdz(id);
            if (wykladowca == null)
                throw new BladBiznesowy($"Wykładowca o id {id} nie istnieje.");
            repo.Usun(wykladowca);
            _baza.Zapisz();
        }

        public void ZmienWykladowce(int id, string tytul, string imie, string nazwisko, string email, int[] idSpecjalnosci)
        {
            var repoWykladowca = _baza.Daj<Wykladowca>();
            var wykladowca = repoWykladowca.Znajdz(id);
            if (wykladowca == null)
                throw new BladBiznesowy($"Wykładowca o id {id} nie istnieje.");
            wykladowca.Tytul = tytul;
            wykladowca.Imie = imie;
            wykladowca.Nazwisko = nazwisko;
            wykladowca.Email = email;
            repoWykladowca.Edytuj(wykladowca);
            var repoWyklSpec = _baza.Daj<WykladowcaSpecjalizacja>();
            var wyklSpec = repoWyklSpec.Wybierz(new ZapytanieWykladowcaSpecjalizacja());
            repoWyklSpec.UsunWiele(wyklSpec);
            foreach (var specId in idSpecjalnosci)
            {
                var specjalnosc = _baza.Daj<Specjalnosc>().Znajdz(specId);
                if (specjalnosc == null)
                    throw new BladBiznesowy($"Specjalność o id {specId} nie istnieje.");
                var ws = new WykladowcaSpecjalizacja
                {
                    Wykladowca = wykladowca,
                    Specjalnosc = specjalnosc
                };
                repoWyklSpec.Dodaj(ws);
            }
            _baza.Zapisz();
        }
    }
}
