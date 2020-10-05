using System;
using System.Collections.Generic;
using System.Linq;
using Test2.Data;
using Test2.Entities;

namespace Test2.Services
{
    public class PlanService : IPlanService
    {
        private PlanContext _planContext;

        public PlanService(PlanContext planContext)
        {
            _planContext = planContext;
        }

        public IEnumerable<Grupa> DajGrupy()
        {
            return _planContext.Grupa.ToArray();
        }

        public IEnumerable<Lekcja> DajLekcje()
        {
            return _planContext.Lekcja.ToArray();
        }

        public IEnumerable<Przedmiot> DajPrzedmioty()
        {
            return _planContext.Przedmiot.ToArray();
        }

        public IEnumerable<Sala> DajSale()
        {
            return _planContext.Sala.ToArray();
        }

        public IEnumerable<Specjalnosc> DajSpecjalnosci(string fraza = null)
        {
            return _planContext.Specjalnosc.Where(x => x.Nazwa.Contains(fraza)).ToArray();
        }

        public IEnumerable<Wykladowca> DajWykladowcow(string fraza = null)
        {
            return _planContext.Wykladowca.Where(x => x.Nazwisko.Contains(fraza)).ToArray();
        }

        public IEnumerable<Zjazd> DajZjazdy()
        {
            return _planContext.Zjazd.ToArray();
        }

        public void DodajGrupe(string numer, short semestr, TrybStudiow tryb, StopienStudiow stopien)
        {
            var grupa = new Grupa
            {
                NrGrupy = numer,
                Semestr = semestr,
                TrybStudiow = tryb,
                StopienStudiow = stopien
            };
            _planContext.Grupa.Add(grupa);
            _planContext.SaveChanges();
        }

        public void DodajLekcje(int idPrzedmiotu, int idWykladowcy, int idSali, TimeSpan godzinaOd, TimeSpan godzinaDo, bool czyOdpracowanie)
        {
            var przedmiot = _planContext.Przedmiot.FirstOrDefault(x => x.IdPrzedmiotu == idPrzedmiotu);
            var wykladowca = _planContext.Wykladowca.FirstOrDefault(x => x.IdWykladowcy == idWykladowcy);
            var sala = _planContext.Sala.FirstOrDefault(x => x.IdSali == idSali);
            var lekcja = new Lekcja
            {
                Przedmiot = przedmiot,
                Wykladowca = wykladowca,
                Sala = sala,
                GodzinaOd = godzinaOd.ToString(),
                GodzinaDo = godzinaDo.ToString(),
            };
            _planContext.Lekcja.Add(lekcja);
            _planContext.SaveChanges();
        }

        public void DodajPrzedmiot(string nazwa, FormaPrzedmiotu forma)
        {
            var przedmiot = new Przedmiot
            {
                Nazwa = nazwa,
                Forma = forma,
            };
            _planContext.Przedmiot.Add(przedmiot);
            _planContext.SaveChanges();
        }

        public void DodajSale(string nazwa, RodzajSali rodzaj)
        {
            var sala = new Sala
            {
                Nazwa = nazwa,
                Rodzaj = rodzaj
            };
            _planContext.Sala.Add(sala);
            _planContext.SaveChanges();
        }

        public void DodajSpecjalnosc(string nazwa)
        {
            var spec = new Specjalnosc
            {
                Nazwa = nazwa
            };
            _planContext.Specjalnosc.Add(spec);
            _planContext.SaveChanges();
        }

        public void DodajWykladowce(string tytul, string imie, string nazwisko, string email, int idSpecjalnosci)
        {
            var spec = _planContext.Specjalnosc.FirstOrDefault(x => x.IdSpecjalnosci == idSpecjalnosci);
            var wykl = new Wykladowca
            {
                Imie = imie,
                Nazwisko = nazwisko,
                Email = email,
                Tytul = tytul,
            };
            _planContext.Wykladowca.Add(wykl);
            _planContext.WyklSpec.Add(new WyklSpec
            {
                Specjalnosc = spec,
                Wykladowca = wykl
            });
            _planContext.SaveChanges();
        }

        public void DodajZjazd(DateTime dataOd, DateTime dataDo, RodzajSemestru semestr)
        {
            var z = new Zjazd
            {
                DataOd = dataOd,
                DataDo = dataDo,
                RodzajSemestru = semestr
            };
            _planContext.Zjazd.Add(z);
            _planContext.SaveChanges();
        }

        public void PrzyporzadkujLekcje(string nrGrupy, int idLekcji)
        {
            var g = _planContext.Grupa.FirstOrDefault(x => x.NrGrupy == nrGrupy);
            var l = _planContext.Lekcja.FirstOrDefault(x => x.IdLekcji == idLekcji);
            _planContext.LekcjaGrupa.Add(new LekcjaGrupa
            {
                Grupa = g,
                Lekcja = l
            });
            _planContext.SaveChanges();
        }

        public void PrzyporzadkujZjazd(short nrZjazdu, string nrGrupy, int idZjazdu)
        {
            var g = _planContext.Grupa.FirstOrDefault(x => x.NrGrupy == nrGrupy);
            var z = _planContext.Zjazd.FirstOrDefault(x => x.IdZjazdu == idZjazdu);
            _planContext.GrupaZjazd.Add(new GrupaZjazd
            {
                NrZjazdu = nrZjazdu,
                Grupa = g,
                Zjazd = z
            });
            _planContext.SaveChanges();
        }

        public void UsunGrupe(string numer)
        {
            var g = _planContext.Grupa.FirstOrDefault(x => x.NrGrupy == numer);
            _planContext.Grupa.Remove(g);
            _planContext.SaveChanges();
        }

        public void UsunLekcje(int idLekcji)
        {
            throw new NotImplementedException();
        }

        public void UsunPrzedmiot(int id)
        {
            throw new NotImplementedException();
        }

        public void UsunSale(int id)
        {
            throw new NotImplementedException();
        }

        public void UsunSpecjalnosc(int id)
        {
            throw new NotImplementedException();
        }

        public void UsunWykladowce(int id)
        {
            throw new NotImplementedException();
        }

        public void UsunZjazd(int id)
        {
            throw new NotImplementedException();
        }

        public void ZmienGrupe(string numer, short semestr, TrybStudiow tryb, StopienStudiow stopien)
        {
            var g = _planContext.Grupa.Find(numer);
            g.Semestr = semestr;
            g.TrybStudiow = tryb;
            g.StopienStudiow = stopien;
            _planContext.Grupa.Update(g);
            _planContext.SaveChanges();
        }

        public void ZmienLekcje(int idLekcji, int idPrzedmiotu, int idWykladowcy, int idSali, TimeSpan godzinaOd, TimeSpan godzinaDo, bool czyOdpracowanie)
        {
            throw new NotImplementedException();
        }

        public void ZmienPrzedmiot(int id, string nazwa, FormaPrzedmiotu forma)
        {
            throw new NotImplementedException();
        }

        public void ZmienSale(int id, string nazwa, RodzajSali rodzaj)
        {
            throw new NotImplementedException();
        }

        public void ZmienSpecjalnosc(int id, string nazwa)
        {
            throw new NotImplementedException();
        }

        public void ZmienWykladowce(int id, string tytul, string imie, string nazwisko, string email, int idSpecjalnosci)
        {
            throw new NotImplementedException();
        }

        public void ZmienZjazd(int id, DateTime dataOd, DateTime dataDo, RodzajSemestru semestr)
        {
            throw new NotImplementedException();
        }
    }
}
