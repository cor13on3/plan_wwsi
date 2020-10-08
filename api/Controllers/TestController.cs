using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Test2.Data;
using Test2.Entities;
using Test2.Services;

namespace Test2.Controllers
{
    public class LekcjaView
    {
        public string Nazwa { get; set; }
        public string Wykladowca { get; set; }
        public string Od { get; set; }
        public string Do { get; set; }
        public string Sala { get; set; }
    }

    [ApiController]
    [Route("api/test")]
    public class TestController : ControllerBase
    {
        private readonly ILogger<TestController> _logger;
        private readonly PlanContext _planContext;

        public TestController(ILogger<TestController> logger, PlanContext planContext)
        {
            _logger = logger;
            _planContext = planContext;
        }

        [HttpGet]
        public IEnumerable<LekcjaView> Get()
        {
            // wydzielić do klasy ZapytaniePlanDnia
            var nrZjazdQuery = from zjazd in _planContext.Set<Zjazd>()
                               join gz in _planContext.Set<GrupaZjazd>()
                                    on zjazd.IdZjazdu equals gz.IdZjazdu
                               where zjazd.DataOd <= new DateTime(2020, 10, 4) && new DateTime(2020, 10, 4) <= zjazd.DataDo
                               select gz.NrZjazdu;
            var nr = nrZjazdQuery.First();

            var dzienTyg = (int)new DateTime(2020, 10, 4).DayOfWeek;
            var query = from lekcjagrupa in _planContext.Set<LekcjaGrupa>()
                        join lekcja in _planContext.Set<Lekcja>()
                            on lekcjagrupa.IdLekcji equals lekcja.IdLekcji
                        join wykladowca in _planContext.Set<Wykladowca>()
                            on lekcja.IdWykladowcy equals wykladowca.IdWykladowcy
                        join przedmiot in _planContext.Set<Przedmiot>()
                            on lekcja.IdPrzedmiotu equals przedmiot.IdPrzedmiotu
                        join sala in _planContext.Set<Sala>()
                            on lekcja.IdSali equals sala.IdSali

                        where lekcjagrupa.NrGrupy == "Z715" && lekcjagrupa.NrZjazdu == nr && lekcjagrupa.DzienTygodnia == dzienTyg
                        select new LekcjaView
                        {
                            Od = lekcja.GodzinaOd,
                            Do = lekcja.GodzinaDo,
                            Wykladowca = wykladowca.Nazwisko,
                            Nazwa = przedmiot.Nazwa,
                            Sala = sala.Nazwa
                        };
            return query.ToArray();
        }

        [HttpPost]
        public void Post()
        {
            var z1 = new Zjazd
            {
                RodzajSemestru = RodzajSemestru.Zimowy,
                DataOd = new DateTime(2020, 10, 3),
                DataDo = new DateTime(2020, 10, 5)
            };
            _planContext.Zjazd.Add(z1);
            var grupa = _planContext.Grupa.First();
            var gz = new GrupaZjazd
            {
                NrZjazdu = 1,
                Grupa = grupa,
                Zjazd = z1
            };
            _planContext.GrupaZjazd.Add(gz);
            _planContext.SaveChanges();
            //var l1 = new Lekcja
            //{
            //    CzasOd = new TimeSpan(13, 45, 0),
            //    CzasDo = new TimeSpan(15, 20, 0),
            //    CzyOdpracowanie = false,
            //};
            //_planContext.Lekcja.Add(l1);
            //var w1 = new Wykladowca
            //{
            //    Nazwisko = "Maleńczuk",
            //    Imie = "Maciej",
            //    Tytul = "mgr",
            //    Email = "m@m.pl",
            //};
            //w1.LekcjaList.Add(l1);
            //_planContext.Wykladowca.Add(w1);
            //var g1 = new Grupa
            //{
            //    NrGrupy = "Z615",
            //    Semestr = 6,
            //    Stopien = StopienStudiow.Inzynierskie,
            //    Tryb = TrybStudiow.Niestacjonarne,
            //};
            //_planContext.Grupa.Add(g1);
            //var lg1 = new LekcjaGrupa
            //{
            //    Lekcja = l1,
            //    Grupa = g1
            //};
            //var p1 = new Przedmiot
            //{
            //    Nazwa = "Elementy sztucznej inteligencji",
            //    Forma = FormaPrzedmiotu.Wyklad
            //};
            //p1.LekcjaList.Add(l1);
            //_planContext.Przedmiot.Add(p1);
            //var s1 = new Sala
            //{
            //    Nazwa = "210",
            //    Rodzaj = RodzajSali.Aula
            //};
            //s1.LekcjaList.Add(l1);
            //_planContext.Sala.Add(s1);



            //_planContext.LekcjaGrupa.Add(lg1);
            //_planContext.SaveChanges();
        }
    }
}
