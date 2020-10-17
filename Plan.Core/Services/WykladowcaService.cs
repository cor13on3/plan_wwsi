using Plan.Core.DTO;
using Plan.Core.Entities;
using Plan.Core.IDatabase;
using Plan.Core.IServices;
using Plan.Core.Zapytania;
using System;
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
                throw new Exception($"Wykładowca o id {id} nie istnieje.");
            return wynik.First();
        }

        public WykladowcaWidokDTO[] DajWykladowcow(string fraza = null)
        {
            var wynik = _baza.Daj<Wykladowca>().Wybierz(new ZapytanieWykladowcy());
            return wynik.ToArray();
        }

        public void DodajWykladowce(string tytul, string imie, string nazwisko, string email, int[] idSpecjalnosci)
        {
            if (string.IsNullOrEmpty(tytul) || string.IsNullOrEmpty(imie) || string.IsNullOrEmpty(nazwisko) || string.IsNullOrEmpty(email))
                throw new Exception("Uzupełnij komplet informacji");
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
                    throw new Exception($"Specjalnosc o id {idSpecjalnosci} nie istnieje.");
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
            var wykladowca = _baza.Daj<Wykladowca>().Znajdz(id);
            if (wykladowca == null)
                throw new Exception($"Wykładowca o id {id} nie istnieje.");
            _baza.Daj<Wykladowca>().Usun(wykladowca);
            _baza.Zapisz();
        }

        public void ZmienWykladowce(int id, string tytul, string imie, string nazwisko, string email, int[] idSpecjalnosci)
        {
            var res = _baza.Daj<Wykladowca>().Znajdz(id);
            if (res == null)
                throw new Exception($"Wykładowca o id {id} nie istnieje.");
            res.Tytul = tytul;
            res.Imie = imie;
            res.Nazwisko = nazwisko;
            res.Email = email;
            var repo = _baza.Daj<WykladowcaSpecjalizacja>();
            var wyklSpec = repo.Wybierz(new ZapytanieWykladowcaSpecjalizacja());
            repo.UsunWiele(wyklSpec);
            foreach (var specId in idSpecjalnosci)
            {
                var specjalnosc = _baza.Daj<Specjalnosc>().Znajdz(specId);
                if (specjalnosc == null)
                    throw new Exception($"Specjalnosc o id {specId} nie istnieje.");
                var ws = new WykladowcaSpecjalizacja
                {
                    Wykladowca = res,
                    Specjalnosc = specjalnosc
                };
                repo.Dodaj(ws);
            }
            _baza.Zapisz();
        }
    }
}
