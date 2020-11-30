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

        public void DodajZjazd(DateTime dataOd, DateTime dataDo, RodzajSemestru rodzajSemestru)
        {
            var repo = _baza.Daj<Zjazd>();
            var zjazdOTerminie = repo.Wybierz(new ZapytanieZjadOTerminie(dataOd, dataDo));
            if (zjazdOTerminie.Count() > 0)
                throw new BladBiznesowy($"Istnieje już zjazd w terminie {dataOd:dd-MM-yyyy} - {dataDo:dd-MM-yyyy}");
            var zjazd = new Zjazd
            {
                DataOd = dataOd,
                DataDo = dataDo,
                RodzajSemestru = rodzajSemestru
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

        public void PrzyporzadkujGrupyDoZjazdu(ZjazdKolejny zjazd, string[] grupy)
        {
            var repo = _baza.Daj<GrupaZjazd>();
            var repoGrupa = _baza.Daj<Grupa>();
            var repoZjazd = _baza.Daj<Zjazd>();
            var z = repoZjazd.Znajdz(zjazd.IdZjazdu);
            if (z == null)
                throw new BladBiznesowy($"Wybierz zjazd");
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
