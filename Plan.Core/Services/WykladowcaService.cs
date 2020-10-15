using Plan.Core.DTO;
using Plan.Core.Entities;
using Plan.Core.IDatabase;
using Plan.Core.IServices;
using System;
using System.Collections.Generic;
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
            var result = _baza.Daj<Wykladowca>().Przegladaj(x => x.IdWykladowcy == id, "WyklSpecList.Specjalnosc").FirstOrDefault();
            if (result == null)
                throw new Exception($"Wykładowca o id {id} nie istnieje.");
            var dto = new WykladowcaDTO
            {
                Specjalnosci = result.WyklSpecList.Select(y => new SpecjalnoscDTO
                {
                    Id = y.IdSpecjalnosci,
                    Nazwa = y.Specjalnosc.Nazwa
                }).ToArray(),
                Imie = result.Imie,
                Id = result.IdWykladowcy,
                Nazwisko = result.Nazwisko,
                Email = result.Email,
                Tytul = result.Tytul,
            };
            return dto;
        }

        public WykladowcaWidokDTO[] DajWykladowcow(string fraza = null)
        {
            var res = _baza.Daj<Wykladowca>().Przegladaj()
                .Select(w => new WykladowcaWidokDTO
                {
                    Id = w.IdWykladowcy,
                    Nazwa = $"{w.Tytul}. {w.Imie[0]} {w.Nazwisko}",
                    Email = w.Email,
                })
                .ToArray();
            return res;
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
                var specjalnosc = _baza.Daj<Specjalnosc>().Daj(id);
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
            var wykladowca = _baza.Daj<Wykladowca>().Daj(id);
            if (wykladowca == null)
                throw new Exception($"Wykładowca o id {id} nie istnieje.");
            _baza.Daj<Wykladowca>().Usun(wykladowca);
            _baza.Zapisz();
        }

        public void ZmienWykladowce(int id, string tytul, string imie, string nazwisko, string email, int[] idSpecjalnosci)
        {
            var res = _baza.Daj<Wykladowca>().Daj(id);
            if (res == null)
                throw new Exception($"Wykładowca o id {id} nie istnieje.");
            res.Tytul = tytul;
            res.Imie = imie;
            res.Nazwisko = nazwisko;
            res.Email = email;
            var specjalnosci = _baza.Daj<WykladowcaSpecjalizacja>().Przegladaj(x => x.IdWykladowcy == id, "").ToArray();
            var specjalnoscDb = _baza.Daj<WykladowcaSpecjalizacja>();
            foreach (var spec in specjalnosci)
                specjalnoscDb.Usun(spec);
            foreach (var specId in idSpecjalnosci)
            {
                var specjalnosc = _baza.Daj<Specjalnosc>().Daj(specId);
                if (specjalnosc == null)
                    throw new Exception($"Specjalnosc o id {specId} nie istnieje.");
                var ws = new WykladowcaSpecjalizacja
                {
                    Wykladowca = res,
                    Specjalnosc = specjalnosc
                };
                specjalnoscDb.Dodaj(ws);
            }
            _baza.Zapisz();
        }
    }
}
