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
    public class WykladowcaServiceTesty
    {
        private Mock<IBazaDanych> _db;
        private Mock<IRepozytorium<Wykladowca>> _repoWykladowca;
        private Mock<IRepozytorium<Specjalnosc>> _repoSpecjalnosc;
        private Mock<IRepozytorium<WykladowcaSpecjalizacja>> _repoWyklSpec;
        private WykladowcaService _service;

        public WykladowcaServiceTesty()
        {
            _db = new Mock<IBazaDanych>();
            _repoWykladowca = new Mock<IRepozytorium<Wykladowca>>();
            _repoSpecjalnosc = new Mock<IRepozytorium<Specjalnosc>>();
            _repoWyklSpec = new Mock<IRepozytorium<WykladowcaSpecjalizacja>>();
            _db.Setup(x => x.DajRepozytorium<Wykladowca>()).Returns(_repoWykladowca.Object);
            _db.Setup(x => x.DajRepozytorium<Specjalnosc>()).Returns(_repoSpecjalnosc.Object);
            _db.Setup(x => x.DajRepozytorium<WykladowcaSpecjalizacja>()).Returns(_repoWyklSpec.Object);
            _service = new WykladowcaService(_db.Object);
        }

        [TestMethod]
        public void Daj_WyjatekWykladowcaNieIstnieje()
        {
            _repoWykladowca.Setup(x => x.Wybierz(It.IsAny<ZapytanieWykladowca>())).Returns(new WykladowcaDTO[0]);

            AssertHelper.OczekiwanyWyjatek<BladBiznesowy>(() => _service.Daj(1),
                "Wykładowca o id 1 nie istnieje.");
        }

        [TestMethod]
        public void Daj_ZwracaDTO()
        {
            _repoWykladowca.Setup(x => x.Wybierz(It.IsAny<ZapytanieWykladowca>())).Returns(new WykladowcaDTO[]
            {
                new WykladowcaDTO{Id = 1},
            });

            var wynik = _service.Daj(1);

            Assert.IsNotNull(wynik);
            Assert.AreEqual(1, wynik.Id);
        }

        [TestMethod]
        public void DajWykladowcow_ZwracaListe()
        {
            _repoWykladowca.Setup(x => x.Wybierz(It.IsAny<ZapytanieWykladowcy>())).Returns(new WykladowcaWidokDTO[]
            {
                new WykladowcaWidokDTO{Id = 1}
            });

            var wynik = _service.Przegladaj();

            Assert.IsNotNull(wynik);
            Assert.AreEqual(1, wynik.Length);
            Assert.AreEqual(1, wynik[0].Id);
        }

        [TestMethod]
        public void Dodaj_WyjatekNiekompletneInformacje()
        {
            AssertHelper.OczekiwanyWyjatek<BladBiznesowy>(() => _service.Dodaj("T1", "I1", "N1", null, new int[] { 1, 2 }),
                "Uzupełnij komplet informacji");
            AssertHelper.OczekiwanyWyjatek<BladBiznesowy>(() => _service.Dodaj("T1", "I1", null, "E1", new int[] { 1, 2 }),
                "Uzupełnij komplet informacji");
            AssertHelper.OczekiwanyWyjatek<BladBiznesowy>(() => _service.Dodaj("T1", null, "N1", "E1", new int[] { 1, 2 }),
                "Uzupełnij komplet informacji");
        }

        [TestMethod]
        public void Dodaj_WyjatekSpecjalnoscNieIstnieje()
        {
            _repoSpecjalnosc.Setup(x => x.Znajdz(It.IsAny<int>())).Returns((Specjalnosc)null);

            AssertHelper.OczekiwanyWyjatek<BladBiznesowy>(() => _service.Dodaj("T1", "I1", "N1", "E1", new int[] { 1, 2 }),
                "Specjalność o id 1 nie istnieje.");
        }

        [TestMethod]
        public void Dodaj_DodajeWykladowce()
        {
            _repoSpecjalnosc.Setup(x => x.Znajdz(1)).Returns(new Specjalnosc { IdSpecjalnosci = 1 });
            _repoSpecjalnosc.Setup(x => x.Znajdz(2)).Returns(new Specjalnosc { IdSpecjalnosci = 2 });

            _service.Dodaj("T1", "I1", "N1", "E1", new int[] { 1, 2 });

            _repoWykladowca.Verify(x => x.Dodaj(It.Is<Wykladowca>(x =>
                x.Imie == "I1" &&
                x.Nazwisko == "N1" &&
                x.Tytul == "T1" &&
                x.Email == "E1"
            )), Times.Once);
        }

        [TestMethod]
        public void Dodaj_DodajePowiazanieWykladowcaSpecjalizacja()
        {
            _repoSpecjalnosc.Setup(x => x.Znajdz(1)).Returns(new Specjalnosc { IdSpecjalnosci = 1 });
            _repoSpecjalnosc.Setup(x => x.Znajdz(2)).Returns(new Specjalnosc { IdSpecjalnosci = 2 });

            _service.Dodaj("T1", "I1", "N1", "E1", new int[] { 1, 2 });

            _repoWyklSpec.Verify(x => x.Dodaj(It.Is<WykladowcaSpecjalizacja>(x =>
                x.Wykladowca.Imie == "I1" &&
                x.Specjalnosc.IdSpecjalnosci == 1
            )), Times.Once);
            _repoWyklSpec.Verify(x => x.Dodaj(It.Is<WykladowcaSpecjalizacja>(x =>
                x.Wykladowca.Imie == "I1" &&
                x.Specjalnosc.IdSpecjalnosci == 2
            )), Times.Once);
            _db.Verify(x => x.Zapisz(), Times.Once);
        }

        [TestMethod]
        public void UsunWykladowce_WyjatekWykladowcaNieIstnieje()
        {
            _repoWykladowca.Setup(x => x.Znajdz(1)).Returns((Wykladowca)null);

            AssertHelper.OczekiwanyWyjatek<BladBiznesowy>(() => _service.Usun(1),
                "Wykładowca o id 1 nie istnieje.");
        }

        [TestMethod]
        public void Usun_WywolujeUsuniecie()
        {
            _repoWykladowca.Setup(x => x.Znajdz(It.IsAny<int>())).Returns(new Wykladowca { IdWykladowcy = 1 });

            _service.Usun(1);

            _repoWykladowca.Verify(x => x.Usun(It.Is<Wykladowca>(x => x.IdWykladowcy == 1)), Times.Once);
            _db.Verify(x => x.Zapisz(), Times.Once);
        }

        [TestMethod]
        public void ZmienWykladowce_WyjatekWykladowcaNieIstnieje()
        {
            _repoWykladowca.Setup(x => x.Znajdz(It.IsAny<int>())).Returns((Wykladowca)null);

            AssertHelper.OczekiwanyWyjatek<BladBiznesowy>(() => _service.Zmien(1, "T1", "I1", "N1", "E1", new int[] { 1, 2 }),
                "Wykładowca o id 1 nie istnieje.");
        }

        [TestMethod]
        public void ZmienWykladowce_WyjatekSpecjalnoscNieIstnieje()
        {
            _repoWykladowca.Setup(x => x.Znajdz(It.IsAny<int>())).Returns(new Wykladowca());
            _repoSpecjalnosc.Setup(x => x.Znajdz(It.IsAny<int>())).Returns((Specjalnosc)null);

            AssertHelper.OczekiwanyWyjatek<BladBiznesowy>(() => _service.Zmien(1, "T1", "I1", "N1", "E1", new int[] { 1, 2 }),
                "Specjalność o id 1 nie istnieje.");
        }

        [TestMethod]
        public void ZmienWykladowce_WywolujeEdycje()
        {
            _repoWykladowca.Setup(x => x.Znajdz(It.IsAny<int>())).Returns(new Wykladowca 
            {
                IdWykladowcy = 1,
                Tytul = "T1",
                Imie = "I1",
                Nazwisko = "N1",
                Email = "E1"
            });

            _service.Zmien(1, "T0", "I0", "N0", "E0", new int[0]);

            _repoWykladowca.Verify(x => x.Edytuj(It.Is<Wykladowca>(x =>
                x.IdWykladowcy == 1 &&
                x.Tytul == "T0" &&
                x.Imie == "I0" &&
                x.Nazwisko == "N0" &&
                x.Email == "E0"
            )), Times.Once);
            _db.Verify(x => x.Zapisz(), Times.Once);
        }

        [TestMethod]
        public void ZmienWykladowce_ZmieniaPowiazania()
        {
            _repoSpecjalnosc.Setup(x => x.Znajdz(1)).Returns(new Specjalnosc { IdSpecjalnosci = 1 });
            _repoSpecjalnosc.Setup(x => x.Znajdz(2)).Returns(new Specjalnosc { IdSpecjalnosci = 2 });
            _repoWykladowca.Setup(x => x.Znajdz(It.IsAny<int>())).Returns(new Wykladowca
            {
                IdWykladowcy = 1,
                Tytul = "T1",
                Imie = "I1",
                Nazwisko = "N1",
                Email = "E1"
            });
            var wyklSpec = new WykladowcaSpecjalizacja[] { new WykladowcaSpecjalizacja()};
            _repoWyklSpec.Setup(x => x.Wybierz(It.IsAny<ZapytanieWykladowcaSpecjalizacja>())).Returns(wyklSpec);

            _service.Zmien(1, "T0", "I0", "N0", "E0", new int[] { 1,2 });

            _repoWyklSpec.Verify(x => x.UsunWiele(wyklSpec), Times.Once);
            _repoWyklSpec.Verify(x => x.Dodaj(It.Is<WykladowcaSpecjalizacja>(x =>
                x.Wykladowca.Imie == "I0" &&
                x.Specjalnosc.IdSpecjalnosci == 1
            )), Times.Once);
            _repoWyklSpec.Verify(x => x.Dodaj(It.Is<WykladowcaSpecjalizacja>(x =>
                x.Wykladowca.Imie == "I0" &&
                x.Specjalnosc.IdSpecjalnosci == 2
            )), Times.Once);
            _db.Verify(x => x.Zapisz(), Times.Once);
        }
    }
}
