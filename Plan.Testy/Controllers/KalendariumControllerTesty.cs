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
        public void DajProponowaneZjazdy_ZwracaProponowaneZjazdy()
        {
            _kalendariumService.Setup(x => x.PrzygotujZjazdy(It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<TrybStudiow>()))
                .Returns(new PropozycjaZjazduWidokDTO[]
                {
                    new PropozycjaZjazduWidokDTO
                    {
                        DataOd = new DateTime(),
                        DataDo = new DateTime().AddDays(1)
                    }
                });

            var wynik = _controller.DajProponowaneZjazdy(new DateTime(), new DateTime(), TrybStudiow.Niestacjonarne);

            Assert.IsNotNull(wynik);
            Assert.AreEqual(1, wynik.Length);
            Assert.AreEqual(new DateTime(), wynik[0].DataOd);
            Assert.AreEqual(new DateTime().AddDays(1), wynik[0].DataDo);
        }

        [TestMethod]
        public void DodajZjazdy_WywolujeDodanieZjazdow()
        {
            var zjazdy = new ZjazdDTO[] { new ZjazdDTO() };
            _controller.DodajZjazdy(zjazdy);

            _kalendariumService.Verify(x => x.DodajZjazdy(zjazdy), Times.Once);
        }

        [TestMethod]
        public void PrzyporzadkujZjazdyGrupie_WywolujePrzyporzadkujZjazdyGrupie()
        {
            var zjazdy = new ZjazdKolejnyDTO[] { new ZjazdKolejnyDTO() };
            _controller.PrzyporzadkujZjazdyGrupie(new KomendaPrzypiszZjazdyGrupie
            {
                NrGrupy = "Z101",
                Zjazdy = zjazdy
            });

            _kalendariumService.Verify(x => x.PrzyporzadkujZjazdyGrupie("Z101", zjazdy));
        }

        [TestMethod]
        public void DajZjazdy_ZwracaZjazdy()
        {
            var zjazdy = new ZjazdWidokDTO[]
            {
                new ZjazdWidokDTO{Nr = 1}
            };
            _kalendariumService.Setup(x => x.PrzegladajZjazdy(It.IsAny<string>())).Returns(zjazdy);

            var wynik = _controller.DajZjazdy("Z101");
            Assert.IsNotNull(wynik);
            Assert.AreEqual(zjazdy, wynik);
        }
    }
}
