using Plan.Core.DTO;
using Plan.Core.Entities;
using Plan.Core.Exceptions;
using Plan.Core.IDatabase;
using Plan.Core.IServices;
using Plan.Core.Zapytania;
using System;
using System.Globalization;
using System.Linq;

namespace Plan.Core.Services
{
    public class LekcjaService : ILekcjaService
    {
        private IBazaDanych _baza;

        public LekcjaService(IBazaDanych baza)
        {
            _baza = baza;
        }

        public LekcjaWidokDTO[] DajPlan(DateTime data, string nrGrupy)
        {
            var zjazd = _baza.Daj<GrupaZjazd>().Wybierz(new ZapytanieZjadyGrupy(nrGrupy, data));
            if (zjazd.Count() == 0)
                return new LekcjaWidokDTO[0];
            var zjazdNr = zjazd.First().Nr;
            var wynik = _baza.Daj<LekcjaGrupa>().Wybierz(new ZapytaniePlanDnia(nrGrupy, zjazdNr, (int)data.DayOfWeek));

            return wynik.ToArray();
        }

        public void Dodaj(int przedmiotId, int wykladowcaId, int salaId, string godzinaOd, string godzinaDo, FormaLekcji forma)
        {
            WalidujDane(przedmiotId, wykladowcaId, salaId, godzinaOd, godzinaDo);
            _baza.Daj<Lekcja>().Dodaj(new Lekcja
            {
                IdPrzedmiotu = przedmiotId,
                IdWykladowcy = wykladowcaId,
                IdSali = salaId,
                GodzinaOd = godzinaOd,
                GodzinaDo = godzinaDo,
                Forma = forma
            });
            _baza.Zapisz();
        }

        public void PrzypiszGrupe(int lekcjaId, string nrGrupy, int nrZjazdu, int dzienTygodnia, bool czyOdpracowanie)
        {
            if (_baza.Daj<Lekcja>().Znajdz(lekcjaId) == null)
                throw new BladBiznesowy($"Lekcja o id {lekcjaId} nie istnieje.");
            if (_baza.Daj<Grupa>().Znajdz(nrGrupy) == null)
                throw new BladBiznesowy($"Grupa o numerze {nrGrupy} nie istnieje.");
            if (nrZjazdu < 0)
                throw new BladBiznesowy("Podano niepoprawny numer zjazdu.");
            if (dzienTygodnia < 0 || dzienTygodnia > 6)
                throw new BladBiznesowy("Podano niepoprawny dzień tygodnia.");
            if (czyOdpracowanie)
            {
                var zjazdy = _baza.Daj<GrupaZjazd>().Wybierz(new ZapytanieZjadyGrupy(nrGrupy));
                if (!zjazdy.Any(x => x.Nr == nrZjazdu && x.CzyOdpracowanie))
                    throw new BladBiznesowy($"Brak ustalonej daty odpracowania zjazdu nr {nrZjazdu} dla grupy {nrGrupy}. Dodaj zjazd z datą odpracowania.");
            }
            _baza.Daj<LekcjaGrupa>().Dodaj(new LekcjaGrupa
            {
                IdLekcji = lekcjaId,
                NrGrupy = nrGrupy,
                NrZjazdu = nrZjazdu,
                DzienTygodnia = dzienTygodnia,
                CzyOdpracowanie = czyOdpracowanie
            });
            _baza.Zapisz();

        }

        public void Usun(int lekcjaId)
        {
            // TODO: sprawdzić czy usuwając lekcję nie usunie się z automaty grupa, sala itd..
            if (_baza.Daj<Lekcja>().Znajdz(lekcjaId) == null)
                throw new BladBiznesowy($"Nie istnieje lekcja o id {lekcjaId}");
            _baza.Daj<Lekcja>().Usun(lekcjaId);
            _baza.Zapisz();
        }

        public void Zmien(int lekcjaId, int przedmiotId, int wykladowcaId, int salaId, string godzinaOd, string godzinaDo, FormaLekcji forma)
        {
            IRepozytorium<Lekcja> repo = _baza.Daj<Lekcja>();
            var lekcja = repo.Znajdz(lekcjaId);
            if (lekcja == null)
                throw new BladBiznesowy($"Nie istnieje lekcja o id {lekcjaId}");
            WalidujDane(przedmiotId, wykladowcaId, salaId, godzinaOd, godzinaDo);
            lekcja.IdPrzedmiotu = przedmiotId;
            lekcja.IdWykladowcy = wykladowcaId;
            lekcja.IdSali = salaId;
            lekcja.GodzinaOd = godzinaOd;
            lekcja.GodzinaDo = godzinaDo;
            lekcja.Forma = forma;
            repo.Edytuj(lekcja);
            _baza.Zapisz();
        }

        private void WalidujDane(int przedmiotId, int wykladowcaId, int salaId, string godzinaOd, string godzinaDo)
        {
            if (_baza.Daj<Przedmiot>().Znajdz(przedmiotId) == null)
                throw new BladBiznesowy($"Przedmiot o id {przedmiotId} nie istnieje.");
            if (_baza.Daj<Wykladowca>().Znajdz(wykladowcaId) == null)
                throw new BladBiznesowy($"Wykładowca o id {wykladowcaId} nie istnieje.");
            if (_baza.Daj<Sala>().Znajdz(salaId) == null)
                throw new BladBiznesowy($"Sala o id {salaId} nie istnieje.");
            try
            {
                DateTime.ParseExact(godzinaOd, "HH:mm:ss", CultureInfo.InvariantCulture);
                DateTime.ParseExact(godzinaDo, "HH:mm:ss", CultureInfo.InvariantCulture);
            }
            catch (Exception)
            {
                throw new BladBiznesowy("Podano niepoprawny format godziny. Podaj godzinę w formacie HH:mm:ss (np. 09:45:00)");
            }
        }
    }
}
