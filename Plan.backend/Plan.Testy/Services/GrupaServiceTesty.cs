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
    public class GrupaServiceTesty
    {
        private Mock<IBazaDanych> _db;
        private Mock<IRepozytorium<Grupa>> _repo;
        private GrupaService _service;

        public GrupaServiceTesty()
        {
            _db = new Mock<IBazaDanych>();
            _repo = new Mock<IRepozytorium<Grupa>>();
            _db.Setup(x => x.DajRepozytorium<Grupa>()).Returns(_repo.Object);
            _service = new GrupaService(_db.Object);
        }

        [TestMethod]
        public void Dodaj_ZglaszaWyjatekBrakParametrow()
        {
            AssertHelper.OczekiwanyWyjatek<BladBiznesowy>(() => _service.Dodaj(null, 0, 0, 0), "Uzupełnij dane");
            AssertHelper.OczekiwanyWyjatek<BladBiznesowy>(() => _service.Dodaj("Z101", -1, 0, 0), "Uzupełnij dane");
        }

        [TestMethod]
        public void Dodaj_ZglaszaWyjatekIstniejeGrupaONumerze()
        {
            _repo.Setup(x => x.Znajdz("Z101")).Returns(new Grupa());
            AssertHelper.OczekiwanyWyjatek<BladBiznesowy>(() => _service.Dodaj("Z101", 1, 0, 0),
                "Istnieje już grupa o numerze Z101");
        }

        [TestMethod]
        public void Dodaj_DodajeGrupe()
        {
            _repo.Setup(x => x.Znajdz("Z101")).Returns((Grupa)null).Verifiable();
            _service.Dodaj("Z101", 1, 0, 0);
            _repo.Verify(x => x.Dodaj(It.Is<Grupa>(x =>

                x.NrGrupy == "Z101" &&
                x.Semestr == 1 &&
                x.StopienStudiow == 0 &&
                x.TrybStudiow == 0
            )), Times.Once);
            _db.Verify(x => x.Zapisz(), Times.Once);
        }

        [TestMethod]
        public void Przegladaj_ZwracaWynikZapytania()
        {
            var lista = new GrupaDTO[]
            {
                new GrupaDTO()
            };
            _repo.Setup(x => x.Wybierz(It.IsAny<ZapytanieGrupy>())).Returns(lista);

            var wynik = _service.Przegladaj();
            Assert.IsNotNull(wynik);
            Assert.AreEqual(1, wynik.Length);
        }

        [TestMethod]
        public void Usun_ZglaszaWyjatekGrupaNieIstnieje()
        {
            _repo.Setup(x => x.Znajdz("Z101")).Returns((Grupa)null).Verifiable();
            AssertHelper.OczekiwanyWyjatek<BladBiznesowy>(() => _service.Usun("Z101"),
                "Grupa o numerze Z101 nie istnieje.");
        }

        [TestMethod]
        public void Usun_UsuwaGrupe()
        {
            _repo.Setup(x => x.Znajdz("Z101")).Returns(new Grupa()).Verifiable();

            _service.Usun("Z101");

            _repo.Verify(x => x.Usun("Z101"), Times.Once);
            _db.Verify(x => x.Zapisz(), Times.Once);
        }
    }
}
