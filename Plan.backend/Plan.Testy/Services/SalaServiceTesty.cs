using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Plan.Core.DTO;
using Plan.Core.Entities;
using Plan.Core.Exceptions;
using Plan.Core.IDatabase;
using Plan.Core.Services;
using Plan.Core.Zapytania;
using Plan.Testy.Helpers;

namespace Plan.Testy.Services
{
    [TestClass]
    public class SalaServiceTesty
    {
        private Mock<IBazaDanych> _db;
        private Mock<IRepozytorium<Sala>> _repo;
        private SalaService _service;

        public SalaServiceTesty()
        {
            _db = new Mock<IBazaDanych>();
            _repo = new Mock<IRepozytorium<Sala>>();
            _db.Setup(x => x.DajTabele<Sala>()).Returns(_repo.Object);
            _service = new SalaService(_db.Object);
        }

        [TestMethod]
        public void Dodaj_ZglaszaWyjatekBrakParametrow()
        {
            AssertHelper.OczekiwanyWyjatek<BladBiznesowy>(() => _service.Dodaj("", RodzajSali.Cwiczeniowa), "Podaj nazwę");
        }

        [TestMethod]
        public void Dodaj_WywolujeDodanie()
        {
            _service.Dodaj("100",RodzajSali.Cwiczeniowa);

            _repo.Verify(x => x.Dodaj(It.Is<Sala>(x =>
                x.Nazwa == "100" &&
                x.Rodzaj == RodzajSali.Cwiczeniowa
            )));
            _db.Verify(x => x.Zapisz(), Times.Once);
        }

        [TestMethod]
        public void Przegladaj_ZwracaWynik()
        {
            _repo.Setup(x => x.Wybierz(It.IsAny<ZapytanieSale>())).Returns(new SalaWidokDTO[]
            {
                new SalaWidokDTO
                {
                    Id = 1,
                    Nazwa = "100",
                    Rodzaj = RodzajSali.Cwiczeniowa
                }
            });

            var wynik = _service.Przegladaj();

            Assert.IsNotNull(wynik);
            Assert.AreEqual(1, wynik.Length);
            Assert.AreEqual(1, wynik[0].Id);
            Assert.AreEqual("100", wynik[0].Nazwa);
            Assert.AreEqual(RodzajSali.Cwiczeniowa, wynik[0].Rodzaj);
        }

        [TestMethod]
        public void Usun_WyjatekSalaNieIstnieje()
        {
            _repo.Setup(x => x.Znajdz(It.IsAny<int>())).Returns((Sala)null);

            AssertHelper.OczekiwanyWyjatek<BladBiznesowy>(() => _service.Usun(1), "Sala o id 1 nie istnieje");
        }

        [TestMethod]
        public void Usun_WywolujeUsuniecie()
        {
            _repo.Setup(x => x.Znajdz(It.IsAny<int>())).Returns(new Sala());
            _service.Usun(1);

            _repo.Verify(x => x.Usun(1), Times.Once);
            _db.Verify(x => x.Zapisz(), Times.Once);
        }
    }
}
