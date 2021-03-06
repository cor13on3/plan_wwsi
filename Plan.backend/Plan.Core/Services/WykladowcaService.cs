using Plan.Core.DTO;
using Plan.Core.Entities;
using Plan.Core.Exceptions;
using Plan.Core.IDatabase;
using Plan.Core.IServices;
using Plan.Core.Zapytania;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Plan.Core.Services
{
    public class WykladowcaService : IWykladowcaService
    {
        private IBazaDanych _db;

        public WykladowcaService(IBazaDanych baza)
        {
            _db = baza;
        }

        public WykladowcaDTO Daj(int id)
        {
            var wynik = _db.DajRepozytorium<Wykladowca>().Wybierz(new ZapytanieWykladowca { IdWykladowcy = id });
            if (wynik.Count() == 0)
                throw new BladBiznesowy($"Wykładowca o id {id} nie istnieje.");
            return wynik.First();
        }

        public LekcjaWidokDTO[] DajPlan(int id, DateTime data)
        {
            var zjazd = _db.DajRepozytorium<Zjazd>().WybierzPierwszy(x => x.DataOd <= data && data <= x.DataDo);
            if (zjazd == null)
                return new LekcjaWidokDTO[0];
            var grupy = _db.DajRepozytorium<GrupaZjazd>().Wybierz(new ZapytanieGrupyWZjezdzie { IdZjazdu = zjazd.IdZjazdu });
            IEnumerable<LekcjaWidokDTO> lista = new List<LekcjaWidokDTO>();
            foreach (var grupa in grupy)
                lista = lista.Concat(_db.DajRepozytorium<LekcjaGrupa>().Wybierz(new ZapytanieLekcjeGrupy
                {
                    NrGrupy = grupa.NrGrupy,
                    NrZjazdu = grupa.NrZjazdu,
                    DzienTygodnia = (int)data.DayOfWeek
                }));
            return lista.Where(x => x.IdWykladowcy == id).ToArray();
        }

        public WykladowcaWidokDTO[] Przegladaj(string fraza)
        {
            var wynik = _db.DajRepozytorium<Wykladowca>().Wybierz(new ZapytanieWykladowcy(fraza));
            return wynik.OrderBy(x => x.Nazwisko).ToArray();
        }

        public void Dodaj(string tytul, string imie, string nazwisko, string email, int[] idSpecjalizacji)
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
            _db.DajRepozytorium<Wykladowca>().Dodaj(wykladowca);
            foreach (var id in idSpecjalizacji)
            {
                var spec = _db.DajRepozytorium<Specjalizacja>().Znajdz(id);
                if (spec == null)
                    throw new BladBiznesowy($"Specjalizacja o id {id} nie istnieje.");
                var ws = new WykladowcaSpecjalizacja
                {
                    Wykladowca = wykladowca,
                    Specjalizacja = spec
                };
                _db.DajRepozytorium<WykladowcaSpecjalizacja>().Dodaj(ws);
            }
            _db.Zapisz();
        }

        public void Usun(int id)
        {
            var wykladowcy = _db.DajRepozytorium<Wykladowca>();
            var rekord = wykladowcy.Znajdz(id);
            if (rekord == null)
                throw new BladBiznesowy($"Wykładowca o id {id} nie istnieje.");
            wykladowcy.Usun(rekord);
            _db.Zapisz();
        }

        public void Zmien(int id, string tytul, string imie, string nazwisko, string email, int[] idSpecjalizacji)
        {
            var repoWykladowca = _db.DajRepozytorium<Wykladowca>();
            var wykladowca = repoWykladowca.Znajdz(id);
            if (wykladowca == null)
                throw new BladBiznesowy($"Wykładowca o id {id} nie istnieje.");
            wykladowca.Tytul = tytul;
            wykladowca.Imie = imie;
            wykladowca.Nazwisko = nazwisko;
            wykladowca.Email = email;
            repoWykladowca.Edytuj(wykladowca);
            var repoWyklSpec = _db.DajRepozytorium<WykladowcaSpecjalizacja>();
            var wyklSpec = repoWyklSpec.Wybierz(new ZapytanieWykladowcaSpecjalizacja { IdWykladowcy = id });
            repoWyklSpec.UsunWiele(wyklSpec);
            foreach (var specId in idSpecjalizacji)
            {
                var specj = _db.DajRepozytorium<Specjalizacja>().Znajdz(specId);
                if (specj == null)
                    throw new BladBiznesowy($"Specjalizacja o id {specId} nie istnieje.");
                var ws = new WykladowcaSpecjalizacja
                {
                    Wykladowca = wykladowca,
                    Specjalizacja = specj
                };
                repoWyklSpec.Dodaj(ws);
            }
            _db.Zapisz();
        }
    }
}
