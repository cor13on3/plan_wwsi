using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Plan.Core.DTO;
using Plan.Core.Entities;
using Plan.Core.Exceptions;
using Plan.Core.IDatabase;
using Plan.Core.Services;
using Plan.Core.Zapytania;
using Plan.Testy.Helpers;
using System;

namespace Plan.Testy.Services
{
    [TestClass]
    public class KalendariumServiceTesty
    {
        private Mock<IBazaDanych> _db;
        private Mock<IRepozytorium<GrupaZjazd>> _repoGrupaZjazd;
        private Mock<IRepozytorium<Zjazd>> _repoZjazd;
        private Mock<IRepozytorium<Grupa>> _repoGrupa;
        private KalendariumService _service;

        public KalendariumServiceTesty()
        {
            _db = new Mock<IBazaDanych>();
            _repoGrupaZjazd = new Mock<IRepozytorium<GrupaZjazd>>();
            _repoZjazd = new Mock<IRepozytorium<Zjazd>>();
            _repoGrupa = new Mock<IRepozytorium<Grupa>>();
            _db.Setup(x => x.DajTabele<GrupaZjazd>()).Returns(_repoGrupaZjazd.Object);
            _db.Setup(x => x.DajTabele<Zjazd>()).Returns(_repoZjazd.Object);
            _db.Setup(x => x.DajTabele<Grupa>()).Returns(_repoGrupa.Object);
            _service = new KalendariumService(_db.Object);
        }

        [TestMethod]
        public void DodajZjazd_WyjatekIstniejeTakiZjazd()
        {
            _repoZjazd.Setup(x => x.Wybierz(It.IsAny<ZapytanieZjadOTerminie>())).Returns(new Zjazd[] { new Zjazd() });

            AssertHelper.OczekiwanyWyjatek<BladBiznesowy>(() => _service.DodajZjazd(

                new DateTime(2020, 10, 19),
                new DateTime(2020, 10, 21),
                RodzajSemestru.Zimowy
            ), "Istnieje już zjazd w terminie 19-10-2020 - 21-10-2020");
        }

        [TestMethod]
        public void DodajZjazdy_DodajeZjazd()
        {
            _repoZjazd.Setup(x => x.Wybierz(It.IsAny<ZapytanieZjadOTerminie>())).Returns(new Zjazd[] { });

            _service.DodajZjazd(new DateTime(2020, 10, 19), new DateTime(2020, 10, 21), RodzajSemestru.Zimowy);

            _repoZjazd.Verify(x => x.Dodaj(It.Is<Zjazd>(x =>
                x.DataOd == new DateTime(2020, 10, 19) &&
                x.DataDo == new DateTime(2020, 10, 21) &&
                x.RodzajSemestru == RodzajSemestru.Zimowy
            )), Times.Once);

            _db.Verify(x => x.Zapisz(), Times.Once);
        }

        [TestMethod]
        public void PrzegladajZjazdy_ZwracaListe()
        {
            _repoGrupaZjazd.Setup(x => x.Wybierz(It.IsAny<ZapytanieZjadyGrupy>())).Returns(new ZjazdWidokDTO[]
            {
                new ZjazdWidokDTO
                {
                    DataOd = new DateTime(2020,10,19),
                    DataDo = new DateTime(2020,10,21),
                    IdZjazdu = 2,
                    CzyOdpracowanie = true,
                    Nr = 4
                }
            });

            var wynik = _service.PrzegladajZjazdyGrupy("Z101");

            Assert.IsNotNull(wynik);
            Assert.AreEqual(1, wynik.Length);
            Assert.AreEqual(new DateTime(2020, 10, 19), wynik[0].DataOd);
            Assert.AreEqual(new DateTime(2020, 10, 21), wynik[0].DataDo);
            Assert.AreEqual(2, wynik[0].IdZjazdu);
            Assert.AreEqual(true, wynik[0].CzyOdpracowanie);
            Assert.AreEqual(4, wynik[0].Nr);
        }
    }
}
