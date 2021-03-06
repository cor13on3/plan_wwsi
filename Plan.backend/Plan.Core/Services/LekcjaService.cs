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
            var zjazd = _baza.DajRepozytorium<GrupaZjazd>().Wybierz(new ZapytanieZjadyGrupy
            {
                NumerGrupy = nrGrupy,
                Data = data
            }).FirstOrDefault();
            if (zjazd == null)
                return new LekcjaWidokDTO[0];
            var wynik = _baza.DajRepozytorium<LekcjaGrupa>().Wybierz(new ZapytanieLekcjeGrupy()
            {
                NrGrupy = nrGrupy,
                NrZjazdu = zjazd.Nr,
                DzienTygodnia = (int)data.DayOfWeek,
            });
            wynik = wynik.Where(x => x.CzyOdpracowanie == zjazd.CzyOdpracowanie);

            return wynik.ToArray();
        }

        public PlanDnia[] DajPlanGrupyNaTydzien(string nrGrupy)
        {
            var lekcje = _baza.DajRepozytorium<LekcjaGrupa>().Wybierz(new ZapytanieLekcjeGrupy() { NrGrupy = nrGrupy });
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
            var lekcje = _baza.DajRepozytorium<LekcjaGrupa>().Wybierz(new ZapytanieLekcjeGrupy()
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
            var lekcje = _baza.DajRepozytorium<LekcjaGrupa>().Wybierz(new ZapytanieLekcje(trybStudiow, semestr, dzienTygodnia))
             .GroupBy(x => x.IdLekcji)
             .Select(x => x.First());
            return lekcje.ToArray();

        }

        public int Dodaj(int przedmiotId, int wykladowcaId, int salaId, int dzienTygodnia, string godzinaOd, string godzinaDo, FormaLekcji forma)
        {
            WalidujDane(przedmiotId, wykladowcaId, salaId, godzinaOd, godzinaDo, dzienTygodnia);
            var lekcja = new Lekcja
            {
                IdPrzedmiotu = przedmiotId,
                IdWykladowcy = wykladowcaId,
                DzienTygodnia = dzienTygodnia,
                GodzinaOd = godzinaOd,
                GodzinaDo = godzinaDo,
                Forma = forma
            };
            if (salaId > 0)
                lekcja.IdSali = salaId;
            else
                lekcja.IdSali = null;
            _baza.DajRepozytorium<Lekcja>().Dodaj(lekcja);
            _baza.Zapisz();
            var id = lekcja.IdLekcji;
            return id;
        }

        public void PrzypiszGrupe(int lekcjaId, string nrGrupy, int nrZjazdu, bool czyOdpracowanie)
        {
            if (_baza.DajRepozytorium<Lekcja>().Znajdz(lekcjaId) == null)
                throw new BladBiznesowy($"Lekcja o id {lekcjaId} nie istnieje.");
            if (_baza.DajRepozytorium<Grupa>().Znajdz(nrGrupy) == null)
                throw new BladBiznesowy($"Grupa o numerze {nrGrupy} nie istnieje.");
            if (nrZjazdu < 0)
                throw new BladBiznesowy("Podano niepoprawny numer zjazdu.");
            var zjazdy = _baza.DajRepozytorium<GrupaZjazd>().Wybierz(new ZapytanieZjadyGrupy { NumerGrupy = nrGrupy });
            if (!zjazdy.Any(x => x.Nr == nrZjazdu))
                throw new BladBiznesowy($"Brak ustalonego zjazdu o numerze {nrZjazdu} dla grupy {nrGrupy}.");
            if (czyOdpracowanie && !zjazdy.Any(x => x.Nr == nrZjazdu && x.CzyOdpracowanie))
                throw new BladBiznesowy($"Brak ustalonej daty odpracowania zjazdu nr {nrZjazdu} dla grupy {nrGrupy}. Dodaj zjazd z datą odpracowania.");
            _baza.DajRepozytorium<LekcjaGrupa>().Dodaj(new LekcjaGrupa
            {
                IdLekcji = lekcjaId,
                NrGrupy = nrGrupy,
                NrZjazdu = nrZjazdu,
                CzyOdpracowanie = czyOdpracowanie
            });
            _baza.Zapisz();
        }

        public void WypiszGrupe(int idLekcji, string nrGrupy, int nrZjazdu, bool czyOdpracowanie)
        {
            var lekcjeGrupy = _baza.DajRepozytorium<LekcjaGrupa>();
            var wynik = lekcjeGrupy.WybierzPierwszy(x =>
                x.IdLekcji == idLekcji &&
                x.NrGrupy == nrGrupy &&
                x.NrZjazdu == nrZjazdu &&
                x.CzyOdpracowanie == czyOdpracowanie);
            if (wynik == null)
                throw new BladBiznesowy("Grupa nie uczestniczy w danej lekcji.");
            lekcjeGrupy.Usun(wynik);
            PorzadkujNieuzywaneLekcje(lekcjeGrupy, idLekcji);
            _baza.Zapisz();
        }

        void PorzadkujNieuzywaneLekcje(IRepozytorium<LekcjaGrupa> repo, int idLekcji)
        {
            var lekcja = repo.WybierzPierwszy(x => x.IdLekcji == idLekcji);
            if (lekcja == null)
            {
                var repoLekcja = _baza.DajRepozytorium<Lekcja>();
                repoLekcja.Usun(idLekcji);
            }
        }

        public void Usun(int lekcjaId)
        {
            var repo = _baza.DajRepozytorium<Lekcja>();
            if (repo.Znajdz(lekcjaId) == null)
                throw new BladBiznesowy($"Nie istnieje lekcja o id {lekcjaId}");
            repo.Usun(lekcjaId);
            _baza.Zapisz();
        }

        private void WalidujDane(int przedmiotId, int wykladowcaId, int salaId, string godzinaOd, string godzinaDo, int? dzienTygodnia = null)
        {
            if (przedmiotId <= 0)
                throw new BladBiznesowy($"Wybierz przedmiot.");
            if (_baza.DajRepozytorium<Przedmiot>().Znajdz(przedmiotId) == null)
                throw new BladBiznesowy($"Przedmiot o id {przedmiotId} nie istnieje.");
            if (wykladowcaId <= 0)
                throw new BladBiznesowy($"Wybierz wykładowcę.");
            if (_baza.DajRepozytorium<Wykladowca>().Znajdz(wykladowcaId) == null)
                throw new BladBiznesowy($"Wykładowca o id {wykladowcaId} nie istnieje.");
            if (dzienTygodnia != null && (dzienTygodnia < 0 || dzienTygodnia > 6))
                throw new BladBiznesowy($"Podano nieprawidłowy dzień tygodnia. Uzyj wartości 0-6 gdzie 0 oznacza niedzielę.");
            if (salaId > 0 && _baza.DajRepozytorium<Sala>().Znajdz(salaId) == null)
                throw new BladBiznesowy($"Sala o id {salaId} nie istnieje.");
            try
            {
                var poczatek = DateTime.ParseExact(godzinaOd, "HH:mm", CultureInfo.InvariantCulture);
                var koniec = DateTime.ParseExact(godzinaDo, "HH:mm", CultureInfo.InvariantCulture);
                if (poczatek >= koniec)
                    throw new BladBiznesowy("Podano nieprawidłowy zakres godzin.");
            }
            catch (FormatException)
            {
                throw new BladBiznesowy("Podano niepoprawny format godziny. Podaj godzinę w formacie HH:mm (np. 09:45)");
            }
        }
    }
}
