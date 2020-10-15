using Plan.Core.DTO;
using Plan.Core.Entities;
using Plan.Core.IDatabase;
using Plan.Core.IServices;
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
            var grupa = _db.Daj<Grupa>();
            if (string.IsNullOrEmpty(numer) || semestr < 0)
                throw new Exception("Uzupełnij dane");
            if (grupa.Przegladaj(x => x.NrGrupy == numer, "").FirstOrDefault() != null)
                throw new Exception($"Istnieje już grupa o numerze {numer}");
            grupa.Dodaj(new Grupa
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
            var grupa = _db.Daj<Grupa>();
            var wynik = grupa.Przegladaj()
                .Select(x => new GrupaWidokDTO
                {
                    Numer = x.NrGrupy
                })
                .ToArray();
            return wynik;
        }

        public void Usun(string numer)
        {
            var grupa = _db.Daj<Grupa>();
            if (grupa.Przegladaj(x => x.NrGrupy == numer, "").FirstOrDefault() == null)
                throw new Exception($"Grupa o numerze {numer} nie istnieje.");
            grupa.Usun(numer);
            _db.Zapisz();
        }
    }
}
