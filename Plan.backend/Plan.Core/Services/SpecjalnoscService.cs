﻿using Plan.Core.DTO;
using Plan.Core.Entities;
using Plan.Core.Exceptions;
using Plan.Core.IDatabase;
using Plan.Core.IServices;
using Plan.Core.Zapytania;
using System.Linq;

namespace Plan.Core.Services
{
    public class SpecjalnoscService : ISpecjalnoscService
    {
        private IBazaDanych _baza;

        public SpecjalnoscService(IBazaDanych baza)
        {
            _baza = baza;
        }

        public void Dodaj(string nazwa)
        {
            if (string.IsNullOrEmpty(nazwa))
                throw new BladBiznesowy("Podaj nazwę");
            var repo = _baza.DajRepozytorium<Specjalnosc>();
            repo.Dodaj(new Specjalnosc
            {
                Nazwa = nazwa,
            });
            _baza.Zapisz();
        }

        public SpecjalnoscDTO[] Przegladaj()
        {
            var repo = _baza.DajRepozytorium<Specjalnosc>();
            var wynik = repo.Wybierz(new ZapytanieSpecjalnosci());
            return wynik.ToArray();
        }

        public void Usun(int id)
        {
            var repo = _baza.DajRepozytorium<Specjalnosc>();
            var specjalnosc = repo.Znajdz(id);
            if (specjalnosc == null)
                throw new BladBiznesowy($"Specjalność o id {id} nie istnieje");
            repo.Usun(specjalnosc);
            _baza.Zapisz();
        }
    }
}
