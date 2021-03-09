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
    public class KalendariumService : IKalendariumService
    {
        private IBazaDanych _baza;

        public KalendariumService(IBazaDanych baza)
        {
            _baza = baza;
        }

        public void DodajZjazd(DateTime dataOd, DateTime dataDo, RodzajSemestru rodzajSemestru)
        {
            var repo = _baza.DajRepozytorium<Zjazd>();
            var zjazdOTerminie = repo.Wybierz(new ZapytanieZjadOTerminie
            {
                Poczatek = dataOd,
                Koniec = dataDo
            });
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

        public void UsunZjazd(int id)
        {
            var repo = _baza.DajRepozytorium<Zjazd>();
            var zjazd = repo.Znajdz(id);
            if (zjazd == null)
                throw new BladBiznesowy($"Zjazd o id: {id} nie istnieje");
            repo.Usun(zjazd);
            _baza.Zapisz();
        }

        public ZjazdWidokDTO[] PrzegladajZjazdyGrupy(string nrGrupy = null)
        {
            var zjazdyGrupy = _baza.DajRepozytorium<GrupaZjazd>();
            var wynik = zjazdyGrupy.Wybierz(new ZapytanieZjadyGrupy
            {
                NumerGrupy = nrGrupy
            });
            return wynik.OrderBy(x => x.Nr).ToArray();
        }

        public ZjazdWidokDTO[] PrzegladajZjazdy()
        {
            var repo = _baza.DajRepozytorium<Zjazd>();
            var wynik = repo.Wybierz(new ZapytanieZjady());
            return wynik.OrderBy(x => x.DataOd).ToArray();
        }

        public void PrzyporzadkujGrupyDoZjazdu(ZjazdKolejny wybranyZjazd, string[] numeryGrup)
        {
            var przypisania = _baza.DajRepozytorium<GrupaZjazd>();
            var grupy = _baza.DajRepozytorium<Grupa>();
            var zjazdy = _baza.DajRepozytorium<Zjazd>();
            var zjazd = zjazdy.Znajdz(wybranyZjazd.IdZjazdu);
            if (zjazd == null)
                throw new BladBiznesowy($"Wybrany zjazd nie istnieje.");
            foreach (var nr in numeryGrup)
            {
                var grupa = grupy.Znajdz(nr);
                if (grupa == null)
                    throw new BladBiznesowy($"Grupa o numerze {nr} nie istnieje.");
                var zjazdyGrupy = przypisania.Wybierz(new ZapytanieZjadyGrupy
                {
                    NumerGrupy = nr
                });
                if (zjazdyGrupy.Any(x => x.IdZjazdu == wybranyZjazd.IdZjazdu))
                    throw new BladBiznesowy($"Grupa {nr} ma już przypisany zjazd o id: {wybranyZjazd.IdZjazdu}.");
                przypisania.Dodaj(new GrupaZjazd
                {
                    NrZjazdu = wybranyZjazd.NrZjazdu,
                    NrGrupy = nr,
                    IdZjazdu = wybranyZjazd.IdZjazdu,
                    CzyOdpracowanie = wybranyZjazd.CzyOdpracowanie
                });
            }
            _baza.Zapisz();
        }

        public void UsunGrupyZZjazdu(string[] grupy, int nrKolejny)
        {
            var repo = _baza.DajRepozytorium<GrupaZjazd>();
            var repoGrupa = _baza.DajRepozytorium<Grupa>();
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
