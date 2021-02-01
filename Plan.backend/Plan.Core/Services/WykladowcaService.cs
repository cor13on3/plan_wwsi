using Plan.Core.DTO;
using Plan.Core.Entities;
using Plan.Core.Exceptions;
using Plan.Core.IDatabase;
using Plan.Core.IServices;
using Plan.Core.Zapytania;
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

        public LekcjaWidokDTO[] DajPlan(int id, System.DateTime data)
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

        public WykladowcaWidokDTO[] Przegladaj(string fraza = null)
        {
            var wynik = _db.DajRepozytorium<Wykladowca>().Wybierz(new ZapytanieWykladowcy());
            return wynik.ToArray();
        }

        public void Dodaj(string tytul, string imie, string nazwisko, string email, int[] idSpecjalnosci)
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
            foreach (var id in idSpecjalnosci)
            {
                var specjalnosc = _db.DajRepozytorium<Specjalnosc>().Znajdz(id);
                if (specjalnosc == null)
                    throw new BladBiznesowy($"Specjalność o id {id} nie istnieje.");
                var ws = new WykladowcaSpecjalizacja
                {
                    Wykladowca = wykladowca,
                    Specjalnosc = specjalnosc
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

        public void Zmien(int id, string tytul, string imie, string nazwisko, string email, int[] idSpecjalnosci)
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
            foreach (var specId in idSpecjalnosci)
            {
                var specjalnosc = _db.DajRepozytorium<Specjalnosc>().Znajdz(specId);
                if (specjalnosc == null)
                    throw new BladBiznesowy($"Specjalność o id {specId} nie istnieje.");
                var ws = new WykladowcaSpecjalizacja
                {
                    Wykladowca = wykladowca,
                    Specjalnosc = specjalnosc
                };
                repoWyklSpec.Dodaj(ws);
            }
            _db.Zapisz();
        }
    }
}
