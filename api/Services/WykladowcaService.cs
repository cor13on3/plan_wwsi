using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Test2.Data;
using Test2.Entities;
using Test2.Models;

namespace Test2.Services
{
    public class WykladowcaService : IWykladowcaService
    {
        private PlanContext _planContext;
        private IRepozytorium<Wykladowca> _repo;

        public WykladowcaService(PlanContext planContext, IRepozytorium<Wykladowca> repo)
        {
            _planContext = planContext;
            _repo = repo;
        }

        public WykladowcaDTO Daj(int id)
        {
            var result = _planContext.Wykladowca.Include(x => x.WyklSpecList).ThenInclude(x => x.Specjalnosc).FirstOrDefault(x => x.IdWykladowcy == id);
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

        public IEnumerable<WykladowcaWidokDTO> DajWykladowcow(string fraza = null)
        {
            // var x = _repo.Przegladaj();
            var query = from w in _planContext.Wykladowca
                        select new WykladowcaWidokDTO
                        {
                            Id = w.IdWykladowcy,
                            Nazwa = $"{w.Tytul}. {w.Imie[0]} {w.Nazwisko}",
                            Email = w.Email,
                        };
            var res = query.ToArray();
            return res;
        }

        public void DodajWykladowce(string tytul, string imie, string nazwisko, string email, int[] idSpecjalnosci)
        {
            if (string.IsNullOrEmpty(tytul) || string.IsNullOrEmpty(imie) || string.IsNullOrEmpty(nazwisko) || string.IsNullOrEmpty(email))
                throw new Exception("Uzupełnij komplet informacji"); // wysyłać odpowiedni status http gdy dany wyjatek
            var wykladowca = new Wykladowca
            {
                Imie = imie,
                Nazwisko = nazwisko,
                Tytul = tytul,
                Email = email,
            };
            _planContext.Wykladowca.Add(wykladowca);
            foreach (var id in idSpecjalnosci)
            {
                var specjalnosc = _planContext.Specjalnosc.FirstOrDefault(x => x.IdSpecjalnosci == id);
                if (specjalnosc == null)
                    throw new Exception($"Specjalnosc o id {idSpecjalnosci} nie istnieje.");
                var ws = new WyklSpec
                {
                    Wykladowca = wykladowca,
                    Specjalnosc = specjalnosc
                };
                _planContext.WyklSpec.Add(ws);
            }
            _planContext.SaveChanges();
        }

        public void UsunWykladowce(int id)
        {
            var wykladowca = _planContext.Wykladowca.FirstOrDefault(x => x.IdWykladowcy == id);
            if (wykladowca == null)
                throw new Exception($"Wykładowca o id {id} nie istnieje.");
            _planContext.Wykladowca.Remove(wykladowca);
            _planContext.SaveChanges();
        }

        public void ZmienWykladowce(int id, string tytul, string imie, string nazwisko, string email, int[] idSpecjalnosci)
        {
            var res = _planContext.Wykladowca.FirstOrDefault(x => x.IdWykladowcy == id);
            if (res == null)
                throw new Exception($"Wykładowca o id {id} nie istnieje.");
            res.Tytul = tytul;
            res.Imie = imie;
            res.Nazwisko = nazwisko;
            res.Email = email;
            var specjalnosci = _planContext.WyklSpec.Where(x => x.IdWykladowcy == id);
            _planContext.WyklSpec.RemoveRange(specjalnosci);
            foreach (var specId in idSpecjalnosci)
            {
                var specjalnosc = _planContext.Specjalnosc.FirstOrDefault(x => x.IdSpecjalnosci == specId);
                if (specjalnosc == null)
                    throw new Exception($"Specjalnosc o id {specId} nie istnieje.");
                var ws = new WyklSpec
                {
                    Wykladowca = res,
                    Specjalnosc = specjalnosc
                };
                _planContext.WyklSpec.Add(ws);
            }
            _planContext.SaveChanges();
        }
    }
}
