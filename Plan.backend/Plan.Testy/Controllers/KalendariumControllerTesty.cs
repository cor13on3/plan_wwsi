using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Plan.API.Controllers;
using Plan.API.Komendy;
using Plan.Core.DTO;
using Plan.Core.Entities;
using Plan.Core.IServices;
using System;

namespace Plan.Testy
{
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
            var zjazdy = new ZjazdWidokDTO[]
            {
                new ZjazdWidokDTO{Nr = 1}
            };
            _kalendariumService.Setup(x => x.PrzegladajZjazdyGrupy(It.IsAny<string>())).Returns(zjazdy);

            var wynik = _controller.DajZjazdyGrupy("Z101");
            Assert.IsNotNull(wynik);
            Assert.AreEqual(zjazdy, wynik);
        }
    }
}
