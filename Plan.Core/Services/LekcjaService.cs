using Plan.Core.DTO;
using Plan.Core.Entities;
using Plan.Core.IDatabase;
using Plan.Core.IServices;
using System;
using System.Collections.Generic;
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

        public IEnumerable<LekcjaWidokDTO> DajPlan(DateTime data, string nrGrupy)
        {
            var nr = _baza.Daj<GrupaZjazd>().Przegladaj(x => x.NrGrupy == nrGrupy && x.Zjazd.DataOd <= data && data <= x.Zjazd.DataDo, "Zjazd").First().NrZjazdu;
            var dzienTyg = (int)data.DayOfWeek;
            var query = _baza.Daj<LekcjaGrupa>().Przegladaj(x => x.NrGrupy == nrGrupy && x.NrZjazdu == nr && x.DzienTygodnia == dzienTyg, "Lekcja.Wykladowca,Lekcja.Przedmiot,Lekcja.Sala")
                .Select(x => new LekcjaWidokDTO
                {
                    Od = x.Lekcja.GodzinaOd,
                    Do = x.Lekcja.GodzinaDo,
                    Wykladowca = x.Lekcja.Wykladowca.Nazwisko,
                    Nazwa = x.Lekcja.Przedmiot.Nazwa,
                    Sala = x.Lekcja.Sala.Nazwa,
                    Forma = (int)x.Lekcja.Forma,
                });

            return query.ToArray();
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
            if (_baza.Daj<Lekcja>().Przegladaj(x => x.IdLekcji == lekcjaId, "").Count() == 0)
                throw new Exception($"Lekcja o id {lekcjaId} nie istnieje.");
            if (_baza.Daj<Grupa>().Przegladaj(x => x.NrGrupy == nrGrupy, "").Count() == 0)
                throw new Exception($"Grupa o numerze {nrGrupy} nie istnieje.");
            if (nrZjazdu < 0)
                throw new Exception("Podano niepoprawny numer zjazdu.");
            if (dzienTygodnia < 0 || dzienTygodnia > 6)
                throw new Exception("Podano niepoprawny dzień tygodnia.");
            if (czyOdpracowanie)
            {
                if (_baza.Daj<GrupaZjazd>().Przegladaj(x => x.NrGrupy == nrGrupy && x.NrZjazdu == nrZjazdu && x.CzyOdpracowanie, "").Count() == 0)
                    throw new Exception($"Brak ustalonej daty odpracowania zjazdu nr {nrZjazdu} dla grupy {nrGrupy}. Dodaj zjazd z datą odpracowania.");
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
            if (_baza.Daj<Lekcja>().Daj(lekcjaId) == null)
                throw new Exception($"Nie istnieje lekcja o id {lekcjaId}");
            _baza.Daj<Lekcja>().Usun(lekcjaId);
            _baza.Zapisz();
        }

        public void Zmien(int lekcjaId, int przedmiotId, int wykladowcaId, int salaId, string godzinaOd, string godzinaDo, FormaLekcji forma)
        {
            var lekcja = _baza.Daj<Lekcja>().Daj(lekcjaId);
            if (lekcja == null)
                throw new Exception($"Nie istnieje lekcja o id {lekcjaId}");
            WalidujDane(przedmiotId, wykladowcaId, salaId, godzinaOd, godzinaDo);
            lekcja.IdPrzedmiotu = przedmiotId;
            lekcja.IdWykladowcy = wykladowcaId;
            lekcja.IdSali = salaId;
            lekcja.GodzinaOd = godzinaOd;
            lekcja.GodzinaDo = godzinaDo;
            lekcja.Forma = forma;
            _baza.Daj<Lekcja>().Edytuj(lekcja);
            _baza.Zapisz();
        }

        private void WalidujDane(int przedmiotId, int wykladowcaId, int salaId, string godzinaOd, string godzinaDo)
        {
            if (_baza.Daj<Przedmiot>().Daj(przedmiotId) == null)
                throw new Exception($"Przedmiot o id {przedmiotId} nie istnieje.");
            if (_baza.Daj<Wykladowca>().Daj(wykladowcaId) == null)
                throw new Exception($"Wykładowca o id {wykladowcaId} nie istnieje.");
            if (_baza.Daj<Sala>().Daj(salaId) == null)
                throw new Exception($"Sala o id {salaId} nie istnieje.");
            try
            {
                DateTime.ParseExact(godzinaOd, "HH:mm:ss", CultureInfo.InvariantCulture);
                DateTime.ParseExact(godzinaDo, "HH:mm:ss", CultureInfo.InvariantCulture);
            }
            catch (Exception)
            {
                throw new Exception("Podano niepoprawny format godziny. Podaj godzinę w formacie HH:mm:ss (np. 09:45:00)");
            }
        }
    }
}
