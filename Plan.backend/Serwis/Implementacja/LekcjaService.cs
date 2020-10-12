using Plan.API.DTO;
using Plan.Interfejsy;
using Plan.Serwis.BazaDanych;
using Plan.Serwis.BazaDanych.Encje;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Plan.Serwis.Implementacja
{
    public class LekcjaService : ILekcjaService
    {
        private PlanContext _planContext;

        public LekcjaService(PlanContext planContext)
        {
            _planContext = planContext;
        }

        public IEnumerable<LekcjaWidokDTO> DajPlan(DateTime data, string nrGrupy)
        {
            var nrZjazduQuery = from zjazd in _planContext.Set<Zjazd>()
                                join gz in _planContext.Set<GrupaZjazd>()
                                     on zjazd.IdZjazdu equals gz.IdZjazdu
                                where zjazd.DataOd <= data && data <= zjazd.DataDo
                                select gz.NrZjazdu;
            var nr = nrZjazduQuery.First();

            var dzienTyg = (int)data.DayOfWeek;
            var query = from lekcjagrupa in _planContext.Set<LekcjaGrupa>()
                        join lekcja in _planContext.Set<Lekcja>()
                            on lekcjagrupa.IdLekcji equals lekcja.IdLekcji
                        join wykladowca in _planContext.Set<Wykladowca>()
                            on lekcja.IdWykladowcy equals wykladowca.IdWykladowcy
                        join przedmiot in _planContext.Set<Przedmiot>()
                            on lekcja.IdPrzedmiotu equals przedmiot.IdPrzedmiotu
                        join sala in _planContext.Set<Sala>()
                            on lekcja.IdSali equals sala.IdSali

                        where lekcjagrupa.NrGrupy == nrGrupy && lekcjagrupa.NrZjazdu == nr && lekcjagrupa.DzienTygodnia == dzienTyg
                        select new LekcjaWidokDTO
                        {
                            Od = lekcja.GodzinaOd,
                            Do = lekcja.GodzinaDo,
                            Wykladowca = wykladowca.Nazwisko,
                            Nazwa = przedmiot.Nazwa,
                            Sala = sala.Nazwa,
                            Forma = (int)lekcja.Forma,
                        };
            return query.ToArray();
        }

        public void Dodaj(int przedmiotId, int wykladowcaId, int salaId, string godzinaOd, string godzinaDo, FormaLekcji forma)
        {
            WalidujDane(przedmiotId, wykladowcaId, salaId, godzinaOd, godzinaDo);
            _planContext.Lekcja.Add(new Lekcja
            {
                IdPrzedmiotu = przedmiotId,
                IdWykladowcy = wykladowcaId,
                IdSali = salaId,
                GodzinaOd = godzinaOd,
                GodzinaDo = godzinaDo,
                Forma = forma
            });
            _planContext.SaveChanges();
        }

        public void PrzypiszGrupe(int lekcjaId, string nrGrupy, int nrZjazdu, int dzienTygodnia, bool czyOdpracowanie)
        {
            if (!_planContext.Lekcja.Any(x => x.IdLekcji == lekcjaId))
                throw new Exception($"Lekcja o id {lekcjaId} nie istnieje.");
            if (!_planContext.Grupa.Any(x => x.NrGrupy == nrGrupy))
                throw new Exception($"Grupa o numerze {nrGrupy} nie istnieje.");
            if (nrZjazdu < 0)
                throw new Exception("Podano niepoprawny numer zjazdu.");
            if (dzienTygodnia < 0 || dzienTygodnia > 6)
                throw new Exception("Podano niepoprawny dzień tygodnia.");
            if (czyOdpracowanie)
            {
                if (!_planContext.GrupaZjazd.Any(x => x.NrGrupy == nrGrupy && x.NrZjazdu == nrZjazdu && x.CzyOdpracowanie))
                    throw new Exception($"Brak ustalonej daty odpracowania zjazdu nr {nrZjazdu} dla grupy {nrGrupy}. Dodaj zjazd z datą odpracowania.");
            }
            _planContext.LekcjaGrupa.Add(new LekcjaGrupa
            {
                IdLekcji = lekcjaId,
                NrGrupy = nrGrupy,
                NrZjazdu = nrZjazdu,
                DzienTygodnia = dzienTygodnia,
                CzyOdpracowanie = czyOdpracowanie
            });
            _planContext.SaveChanges();

        }

        public void Usun(int lekcjaId)
        {
            // TODO: sprawdzić czy usuwając lekcję nie usunie się z automaty grupa, sala itd..
            var lekcja = _planContext.Lekcja.Find(lekcjaId);
            if (lekcja == null)
                throw new Exception($"Nie istnieje lekcja o id {lekcjaId}");
            _planContext.Lekcja.Remove(lekcja);
            _planContext.SaveChanges();
        }

        public void Zmien(int lekcjaId, int przedmiotId, int wykladowcaId, int salaId, string godzinaOd, string godzinaDo, FormaLekcji forma)
        {
            var lekcja = _planContext.Lekcja.Find(lekcjaId);
            if (lekcja == null)
                throw new Exception($"Nie istnieje lekcja o id {lekcjaId}");
            WalidujDane(przedmiotId, wykladowcaId, salaId, godzinaOd, godzinaDo);
            lekcja.IdPrzedmiotu = przedmiotId;
            lekcja.IdWykladowcy = wykladowcaId;
            lekcja.IdSali = salaId;
            lekcja.GodzinaOd = godzinaOd;
            lekcja.GodzinaDo = godzinaDo;
            lekcja.Forma = forma;
            _planContext.SaveChanges();
        }

        private void WalidujDane(int przedmiotId, int wykladowcaId, int salaId, string godzinaOd, string godzinaDo)
        {
            if (!_planContext.Przedmiot.Any(x => x.IdPrzedmiotu == przedmiotId))
                throw new Exception($"Przedmiot o id {przedmiotId} nie istnieje.");
            if (!_planContext.Wykladowca.Any(x => x.IdWykladowcy == wykladowcaId))
                throw new Exception($"Wykładowca o id {wykladowcaId} nie istnieje.");
            if (!_planContext.Sala.Any(x => x.IdSali == salaId))
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
