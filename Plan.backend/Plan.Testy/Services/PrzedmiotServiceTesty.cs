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
    public class PrzedmiotServiceTesty
    {
        private Mock<IBazaDanych> _db;
        private Mock<IRepozytorium<Przedmiot>> _repo;
        private PrzedmiotService _service;

        public PrzedmiotServiceTesty()
        {
            _db = new Mock<IBazaDanych>();
            _repo = new Mock<IRepozytorium<Przedmiot>>();
            _db.Setup(x => x.DajTabele<Przedmiot>()).Returns(_repo.Object);
            _service = new PrzedmiotService(_db.Object);
        }

        [TestMethod]
        public void Dodaj_ZglaszaWyjatekBrakParametrow()
        {
            AssertHelper.OczekiwanyWyjatek<BladBiznesowy>(() => _service.Dodaj(""), "Podaj nazwę");
        }

        [TestMethod]
        public void Dodaj_WywolujeDodanie()
        {
            _service.Dodaj("Sieci");

            _repo.Verify(x => x.Dodaj(It.Is<Przedmiot>(x =>
                x.Nazwa == "Sieci"
            )));
            _db.Verify(x => x.Zapisz(), Times.Once);
        }

        [TestMethod]
        public void Przegladaj_ZwracaWynik()
        {
            _repo.Setup(x => x.Wybierz(It.IsAny<ZapytaniePrzedmioty>())).Returns(new PrzedmiotWidokDTO[]
            {
                new PrzedmiotWidokDTO
                {
                    Id = 1,
                    Nazwa = "Sieci"
                }
            });

            var wynik = _service.Przegladaj();

            Assert.IsNotNull(wynik);
            Assert.AreEqual(1, wynik.Length);
            Assert.AreEqual(1, wynik[0].Id);
            Assert.AreEqual("Sieci", wynik[0].Nazwa);
        }

        [TestMethod]
        public void Usun_WyjatekPrzedmiotNieIstnieje()
        {
            _repo.Setup(x => x.Znajdz(It.IsAny<int>())).Returns((Przedmiot)null);

            AssertHelper.OczekiwanyWyjatek<BladBiznesowy>(() => _service.Usun(1), "Przedmiot o id 1 nie istnieje");
        }

        [TestMethod]
        public void Usun_WywolujeUsuniecie()
        {
            _repo.Setup(x => x.Znajdz(It.IsAny<int>())).Returns(new Przedmiot { IdPrzedmiotu = 1 });
            _service.Usun(1);

            _repo.Verify(x => x.Usun(It.Is<Przedmiot>(x => x.IdPrzedmiotu == 1)), Times.Once);
            _db.Verify(x => x.Zapisz(), Times.Once);
        }
    }
}
