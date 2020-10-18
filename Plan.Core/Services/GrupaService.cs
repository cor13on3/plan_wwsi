using Plan.Core.DTO;
using Plan.Core.Entities;
using Plan.Core.Exceptions;
using Plan.Core.IDatabase;
using Plan.Core.IServices;
using Plan.Core.Zapytania;
using System;
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
            var repo = _db.Daj<Grupa>();
            if (string.IsNullOrEmpty(numer) || semestr < 0)
                throw new BladBiznesowy("Uzupełnij dane");
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

        public GrupaWidokDTO[] Przegladaj()
        {
            var repo = _db.Daj<Grupa>();
            var wynik = repo.Wybierz(new ZapytanieGrupy()).ToArray();
            return wynik;
        }

        public void Usun(string numer)
        {
            var repo = _db.Daj<Grupa>();
            if (repo.Znajdz(numer) == null)
                throw new BladBiznesowy($"Grupa o numerze {numer} nie istnieje.");
            repo.Usun(numer);
            _db.Zapisz();
        }
    }
}
