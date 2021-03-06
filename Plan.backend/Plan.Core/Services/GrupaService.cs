using Plan.Core.DTO;
using Plan.Core.Entities;
using Plan.Core.Exceptions;
using Plan.Core.IDatabase;
using Plan.Core.IServices;
using Plan.Core.Zapytania;
using System.Linq;

namespace Plan.Core.Services
{
    public class GrupaService : IGrupaService
    {
        private IBazaDanych _db;

        public GrupaService(IBazaDanych db)
        {
            _db = db;
        }

        public void Dodaj(string numer, int semestr, TrybStudiow trybStudiow, StopienStudiow stopienStudiow)
        {
            if (string.IsNullOrEmpty(numer) || semestr < 0)
                throw new BladBiznesowy("Uzupełnij dane");
            var repo = _db.DajRepozytorium<Grupa>();
            if (repo.Znajdz(numer) != null)
                throw new BladBiznesowy($"Istnieje już grupa o numerze {numer}");
            repo.Dodaj(new Grupa
            {
                NrGrupy = numer,
                Semestr = semestr,
                TrybStudiow = trybStudiow,
                StopienStudiow = stopienStudiow
            });
            _db.Zapisz();
        }

        public GrupaDTO[] Filtruj(TrybStudiow? tryb, StopienStudiow? stopien, int? semestr)
        {
            var repo = _db.DajRepozytorium<Grupa>();
            var wynik = repo.Wybierz(new ZapytanieGrupy(tryb, stopien, semestr)).ToArray();
            return wynik;
        }

        public GrupaDTO[] Przegladaj(string fraza)
        {
            var repo = _db.DajRepozytorium<Grupa>();
            var wynik = repo.Wybierz(new ZapytanieGrupy(fraza: fraza)).ToArray();
            return wynik;
        }

        public GrupaDTO Daj(string numer)
        {
            var repo = _db.DajRepozytorium<Grupa>();
            var grupa = repo.Znajdz(numer);
            if (grupa == null)
                return null;
            return new GrupaDTO
            {
                Numer = grupa.NrGrupy,
                Semestr = grupa.Semestr,
                TrybStudiow = grupa.TrybStudiow,
                StopienStudiow = grupa.StopienStudiow
            };
        }

        public void Usun(string numer)
        {
            var repo = _db.DajRepozytorium<Grupa>();
            if (repo.Znajdz(numer) == null)
                throw new BladBiznesowy($"Grupa o numerze {numer} nie istnieje.");
            repo.Usun(numer);
            _db.Zapisz();
        }
    }
}
