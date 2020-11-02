using Plan.Core.DTO;
using Plan.Core.Entities;
using Plan.Core.Exceptions;
using Plan.Core.IDatabase;
using Plan.Core.IServices;
using Plan.Core.Zapytania;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Plan.Core.Services
{
    public class KalendariumService : IKalendariumService
    {
        private IBazaDanych _baza;

        public KalendariumService(IBazaDanych baza)
        {
            _baza = baza;
        }

        public void DodajZjazd(ZjazdDTO z)
        {
            var repo = _baza.Daj<Zjazd>();
            var zjazdOTerminie = repo.Wybierz(new ZapytanieZjadOTerminie(z.DataOd, z.DataDo));
            if (zjazdOTerminie.Count() > 0)
                throw new BladBiznesowy($"Istnieje już zjazd w terminie {z.DataOd:dd-MM-yyyy} - {z.DataDo:dd-MM-yyyy}");
            var zjazd = new Zjazd
            {
                DataOd = z.DataOd,
                DataDo = z.DataDo,
                RodzajSemestru = z.RodzajSemestru
            };
            repo.Dodaj(zjazd);

            _baza.Zapisz();
        }

        public ZjazdWidokDTO[] PrzegladajZjazdyGrupy(string nrGrupy = null)
        {
            var repo = _baza.Daj<GrupaZjazd>();
            var wynik = repo.Wybierz(new ZapytanieZjadyGrupy(nrGrupy));
            return wynik.ToArray();
        }

        public ZjazdWidokDTO[] PrzegladajZjazdy()
        {
            var repo = _baza.Daj<Zjazd>();
            var wynik = repo.Wybierz(new ZapytanieZjady());
            return wynik.ToArray();
        }

        [ExcludeFromCodeCoverage]
        public PropozycjaZjazduWidokDTO[] PrzygotujZjazdy(DateTime poczatekSemestru, DateTime koniecSemestru, TrybStudiow trybStudiow)
        {
            var result = new List<PropozycjaZjazduWidokDTO>();
            var offset = trybStudiow == TrybStudiow.Stacjonarne ? 7 : 14;
            var dlugosc = trybStudiow == TrybStudiow.Stacjonarne ? 5 : 3;
            for (DateTime d = poczatekSemestru; d <= koniecSemestru; d = d.AddDays(offset))
            {
                result.Add(new PropozycjaZjazduWidokDTO
                {
                    DataOd = d,
                    DataDo = d.AddDays(dlugosc - 1)
                });
            }
            return result.ToArray();
        }

        public void PrzyporzadkujZjazdyGrupie(string nrGrupy, ZjazdKolejnyDTO[] zjazdy)
        {
            var grupa = _baza.Daj<Grupa>().Znajdz(nrGrupy);
            if (grupa == null)
                throw new BladBiznesowy($"Grupa o numerze {nrGrupy} nie istnieje");
            var repo = _baza.Daj<GrupaZjazd>();
            var zjazdyGrupy = repo.Wybierz(new ZapytanieZjadyGrupy(nrGrupy));
            foreach (var z in zjazdy)
            {
                if (zjazdyGrupy.Any(x => x.IdZjazdu == z.IdZjazdu))
                    throw new BladBiznesowy($"Grupa {nrGrupy} ma już przypisany zjazd o id: {z.IdZjazdu}");
                repo.Dodaj(new GrupaZjazd
                {
                    NrZjazdu = z.NrZjazdu,
                    NrGrupy = nrGrupy,
                    IdZjazdu = z.IdZjazdu,
                    CzyOdpracowanie = z.CzyOdpracowanie
                });
            }
            _baza.Zapisz();
        }

        public void PrzyporzadkujGrupyDoZjazdu(ZjazdKolejnyDTO zjazd, string[] grupy)
        {
            var repo = _baza.Daj<GrupaZjazd>();
            var repoGrupa = _baza.Daj<Grupa>();
            foreach (var nr in grupy)
            {
                var grupa = repoGrupa.Znajdz(nr);
                if (grupa == null)
                    throw new BladBiznesowy($"Grupa o numerze {nr} nie istnieje");
                var zjazdyGrupy = repo.Wybierz(new ZapytanieZjadyGrupy(nr));
                if (zjazdyGrupy.Any(x => x.IdZjazdu == zjazd.IdZjazdu))
                    throw new BladBiznesowy($"Grupa {nr} ma już przypisany zjazd o id: {zjazd.IdZjazdu}");
                repo.Dodaj(new GrupaZjazd
                {
                    NrZjazdu = zjazd.NrZjazdu,
                    NrGrupy = nr,
                    IdZjazdu = zjazd.IdZjazdu,
                    CzyOdpracowanie = zjazd.CzyOdpracowanie
                });
            }
            _baza.Zapisz();
        }

        public void UsunGrupyZZjazdu(string[] grupy, int nrKolejny)
        {
            var repo = _baza.Daj<GrupaZjazd>();
            var repoGrupa = _baza.Daj<Grupa>();
            foreach (var nr in grupy)
            {
                var grupa = repoGrupa.Znajdz(nr);
                if (grupa == null)
                    throw new BladBiznesowy($"Grupa o numerze {nr} nie istnieje");
                repo.Usun(x => x.NrGrupy == nr && x.NrZjazdu == nrKolejny);
            }
            _baza.Zapisz();
        }
    }
}
