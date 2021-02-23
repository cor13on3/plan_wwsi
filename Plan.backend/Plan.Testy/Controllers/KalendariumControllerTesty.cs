using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Plan.API.Controllers;
using Plan.API.Komendy;
using Plan.Core.DTO;
using Plan.Core.Entities;
using Plan.Core.IDatabase;
using Plan.Core.IServices;
using Plan.Core.Zapytania;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Plan.Testy.Controllers
{
    class TesterZapytan<T>
    {
        private List<T> _dane;

        public TesterZapytan()
        {
            _dane = new List<T>();
        }

        public IEnumerable<TDTO> Wybierz<TDTO>(IZapytanie<T, TDTO> spec)
        {
            var x = _dane.AsQueryable().Where(spec.Kryteria.Compile()).ToArray();
            return x.Select(spec.Mapowanie.Compile());
        }

        public void Dodaj(params T[] dane)
        {
            foreach (var d in dane)
                _dane.Add(d);
        }
    }

    [TestClass]
    public class KalendariumControllerTesty
    {
        private Mock<IKalendariumService> _kalendariumService;
        private KalendariumController _controller;

        [TestInitialize]
        public void Init()
        {
            _kalendariumService = new Mock<IKalendariumService>();
            _controller = new KalendariumController(_kalendariumService.Object);
        }

        //[TestMethod]
        //public void MyTestMethod()
        //{
        //    var tester = new TesterZapytan<Wykladowca>();
        //    tester.Dodaj(new Wykladowca { IdWykladowcy = 1, Imie = "andrzej" }, new Wykladowca { IdWykladowcy = 2, Imie = "jan" });
        //    var wynik = tester.Wybierz(new ZapytanieWykladowcy("ja"));
        //    Assert.AreEqual("jan", wynik.First().Id);
        //}

        [TestMethod]
        public void DodajZjazd_WywolujeDodanieZjazdu()
        {
            _controller.DodajZjazd(new KomendaDodajZjazd
            {
                DataOd = new DateTime(2020, 10, 1),
                DataDo = new DateTime(2020, 10, 3),
                RodzajSemestru = RodzajSemestru.Zimowy
            });

            _kalendariumService.Verify(x => x.DodajZjazd(new DateTime(2020, 10, 1), new DateTime(2020, 10, 3), RodzajSemestru.Zimowy), Times.Once);
        }

        [TestMethod]
        public void DajZjazdyGrupy_ZwracaZjazdy()
        {
            var zjazdy = new ZjazdWidokDTO[] { new ZjazdWidokDTO { Nr = 1 } };
            _kalendariumService.Setup(x => x.PrzegladajZjazdyGrupy("Z715")).Returns(zjazdy);

            var wynik = _controller.DajZjazdyGrupy("Z715");

            Assert.IsNotNull(wynik);
            Assert.AreEqual(zjazdy, wynik);
        }
    }
}
