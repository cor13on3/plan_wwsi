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

        public LekcjaWidokDTO[] DajPlanGrupyNaDzien(DateTime data, string nrGrupy)
        {
            var zjazd = _baza.DajTabele<GrupaZjazd>().Wybierz(new ZapytanieZjadyGrupy
            {
                NumerGrupy = nrGrupy,
                Data = data
            });
            if (zjazd.Count() == 0)
                return new LekcjaWidokDTO[0];
            var zjazdNr = zjazd.First().Nr;
            var wynik = _baza.DajTabele<LekcjaGrupa>().Wybierz(new ZapytanieLekcjeGrupy()
            {
                NrGrupy = nrGrupy,
                NrZjazdu = zjazdNr,
                DzienTygodnia = (int)data.DayOfWeek,
            });
            wynik = wynik.Where(x => x.CzyOdpracowanie == zjazd.First().CzyOdpracowanie);

            return wynik.ToArray();
        }

        public PlanDnia[] DajPlanGrupyNaTydzien(string nrGrupy)
        {
            var lekcje = _baza.DajTabele<LekcjaGrupa>().Wybierz(new ZapytanieLekcjeGrupy()
            {
                NrGrupy = nrGrupy,
            });

            var dni = lekcje.GroupBy(x => x.DzienTygodnia).Select(x => new PlanDnia
            {
                DzienTygodnia = x.Key,
                Lekcje = x.Where(x => !x.CzyOdpracowanie).GroupBy(z => z.IdLekcji).Select(z => new LekcjaWZjazdach
                {
                    Lekcja = new LekcjaWidokDTO
                    {
                        IdLekcji = z.First().IdLekcji,
                        Nazwa = z.First().Nazwa,
                        Wykladowca = z.First().Wykladowca,
                        Sala = z.First().Sala,
                        Od = z.First().Od,
                        Do = z.First().Do,
                        Forma = z.First().Forma,
                        CzyOdpracowanie = z.First().CzyOdpracowanie,
                        DzienTygodnia = z.First().DzienTygodnia,
                        NrZjazdu = z.First().NrZjazdu
                    },
                    Zjazdy = z.Select(y => y.NrZjazdu).ToArray()
                }).ToArray()
            });

            return dni.ToArray();
        }

        public PlanDnia[] DajPlanOdpracowania(string nrGrupy, int nrZjazdu)
        {
            var lekcje = _baza.DajTabele<LekcjaGrupa>().Wybierz(new ZapytanieLekcjeGrupy()
            {
                NrGrupy = nrGrupy,
                NrZjazdu = nrZjazdu,
            });

            var dni = lekcje.GroupBy(x => x.DzienTygodnia).Select(x => new PlanDnia
            {
                DzienTygodnia = x.Key,
                Lekcje = x.Where(x => x.CzyOdpracowanie).Select(z => new LekcjaWZjazdach
                {
                    Lekcja = new LekcjaWidokDTO
                    {
                        IdLekcji = z.IdLekcji,
                        Nazwa = z.Nazwa,
                        Wykladowca = z.Wykladowca,
                        Sala = z.Sala,
                        Od = z.Od,
                        Do = z.Do,
                        Forma = z.Forma,
                        CzyOdpracowanie = z.CzyOdpracowanie,
                        DzienTygodnia = z.DzienTygodnia,
                        NrZjazdu = z.NrZjazdu
                    },
                    Zjazdy = new int[] { z.NrZjazdu }
                }).ToArray()
            });

            return dni.ToArray();
        }

        public LekcjaDTO[] DajLekcjeNaDzienTygodnia(TrybStudiow trybStudiow, int semestr, int dzienTygodnia)
        {
            var lekcje = _baza.DajTabele<LekcjaGrupa>().Wybierz(new ZapytanieLekcje()
            {
                Tryb = trybStudiow,
                Semestr = semestr,
            })
             .GroupBy(x => x.IdLekcji)
             .Select(x => x.First());
            return lekcje.ToArray();

        }

        public int Dodaj(int przedmiotId, int wykladowcaId, int salaId, string godzinaOd, string godzinaDo, FormaLekcji forma)
        {
            WalidujDane(przedmiotId, wykladowcaId, salaId, godzinaOd, godzinaDo);
            var lekcja = new Lekcja
            {
                IdPrzedmiotu = przedmiotId,
                IdWykladowcy = wykladowcaId,
                IdSali = salaId,
                GodzinaOd = godzinaOd,
                GodzinaDo = godzinaDo,
                Forma = forma
            };
            _baza.DajTabele<Lekcja>().Dodaj(lekcja);
            _baza.Zapisz();
            var id = lekcja.IdLekcji;
            return id;
        }

        public void PrzypiszGrupe(int lekcjaId, string nrGrupy, int nrZjazdu, int dzienTygodnia, bool czyOdpracowanie)
        {
            if (_baza.DajTabele<Lekcja>().Znajdz(lekcjaId) == null)
                throw new BladBiznesowy($"Lekcja o id {lekcjaId} nie istnieje.");
            if (_baza.DajTabele<Grupa>().Znajdz(nrGrupy) == null)
                throw new BladBiznesowy($"Grupa o numerze {nrGrupy} nie istnieje.");
            if (nrZjazdu < 0)
                throw new BladBiznesowy("Podano niepoprawny numer zjazdu.");
            if (dzienTygodnia < 0 || dzienTygodnia > 6)
                throw new BladBiznesowy("Podano niepoprawny dzień tygodnia.");
            var zjazdy = _baza.DajTabele<GrupaZjazd>().Wybierz(new ZapytanieZjadyGrupy
            {
                NumerGrupy = nrGrupy
            });
            if (!zjazdy.Any(x => x.Nr == nrZjazdu))
                throw new BladBiznesowy($"Brak ustalonego zjazdu o numerze {nrZjazdu} dla grupy {nrGrupy}.");
            if (czyOdpracowanie)
            {
                if (!zjazdy.Any(x => x.Nr == nrZjazdu && x.CzyOdpracowanie))
                    throw new BladBiznesowy($"Brak ustalonej daty odpracowania zjazdu nr {nrZjazdu} dla grupy {nrGrupy}. Dodaj zjazd z datą odpracowania.");
            }
            _baza.DajTabele<LekcjaGrupa>().Dodaj(new LekcjaGrupa
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
            var repo = _baza.DajTabele<Lekcja>();
            if (repo.Znajdz(lekcjaId) == null)
                throw new BladBiznesowy($"Nie istnieje lekcja o id {lekcjaId}");
            repo.Usun(lekcjaId);
            _baza.Zapisz();
        }

        public void Zmien(int lekcjaId, int przedmiotId, int wykladowcaId, int salaId, string godzinaOd, string godzinaDo, FormaLekcji forma)
        {
            WalidujDane(przedmiotId, wykladowcaId, salaId, godzinaOd, godzinaDo);
            IRepozytorium<Lekcja> repo = _baza.DajTabele<Lekcja>();
            var lekcja = repo.Znajdz(lekcjaId);
            if (lekcja == null)
                throw new BladBiznesowy($"Nie istnieje lekcja o id {lekcjaId}");
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
            if (przedmiotId <= 0)
                throw new BladBiznesowy($"Wybierz przedmiot.");
            if (_baza.DajTabele<Przedmiot>().Znajdz(przedmiotId) == null)
                throw new BladBiznesowy($"Przedmiot o id {przedmiotId} nie istnieje.");
            if (wykladowcaId <= 0)
                throw new BladBiznesowy($"Wybierz wykładowcę.");
            if (_baza.DajTabele<Wykladowca>().Znajdz(wykladowcaId) == null)
                throw new BladBiznesowy($"Wykładowca o id {wykladowcaId} nie istnieje.");
            if (salaId <= 0)
                throw new BladBiznesowy($"Wybierz salę.");
            if (_baza.DajTabele<Sala>().Znajdz(salaId) == null)
                throw new BladBiznesowy($"Sala o id {salaId} nie istnieje.");
            try
            {
                DateTime.ParseExact(godzinaOd, "HH:mm", CultureInfo.InvariantCulture);
                DateTime.ParseExact(godzinaDo, "HH:mm", CultureInfo.InvariantCulture);
            }
            catch (Exception)
            {
                throw new BladBiznesowy("Podano niepoprawny format godziny. Podaj godzinę w formacie HH:mm (np. 09:45)");
            }
        }
    }
}
