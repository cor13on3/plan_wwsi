using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Plan.API.Controllers;
using Plan.API.Komendy;
using Plan.Core.DTO;
using Plan.Core.IServices;

namespace Plan.Testy
{
    [TestClass]
    public class WykladowcaControllerTesty
    {
        private Mock<IWykladowcaService> _wykladowcaService;
        private WykladowcaController _controller;

        [TestInitialize]
        public void Init()
        {
            _wykladowcaService = new Mock<IWykladowcaService>();
            _controller = new WykladowcaController(_wykladowcaService.Object);
        }

        [TestMethod]
        public void Get_ZwracaListe()
        {
            var lista = new WykladowcaWidokDTO[]
            {
                new WykladowcaWidokDTO()
            };
            _wykladowcaService.Setup(x => x.Przegladaj(null)).Returns(lista).Verifiable();

            var wynik = _controller.Przegladaj();
            Assert.IsNotNull(wynik);
            Assert.AreEqual(lista, wynik);
        }

        [TestMethod]
        public void Get_ZwracaWykladowce()
        {
            var wykladowca = new WykladowcaDTO();
            _wykladowcaService.Setup(x => x.Daj(It.IsAny<int>())).Returns(wykladowca).Verifiable();

            var wynik = _controller.Daj(1);
            Assert.IsNotNull(wynik);
            Assert.AreEqual(wykladowca, wynik);
        }

        [TestMethod]
        public void Post_WykonujeDodanieWykladowcy()
        {
            _controller.Dodaj(new KomendaDodajWykladowce
            {
                Tytul = "T1",
                Imie = "I1",
                Nazwisko = "N1",
                Email = "E1",
                Specjalnosci = new int[] { 1, 2 }
            });

            _wykladowcaService.Verify(x => x.Dodaj("T1", "I1", "N1", "E1", new int[] { 1, 2 }), Times.Once);
        }

        [TestMethod]
        public void Put_WykonujeEdycjeWykladowcy()
        {
            _controller.Zmien(1, new KomendaEdytujWykladowce
            {
                Tytul = "T1",
                Imie = "I1",
                Nazwisko = "N1",
                Email = "E1",
                Specjalnosci = new int[] { 1, 2 }
            });

            _wykladowcaService.Verify(x => x.Zmien(1, "T1", "I1", "N1", "E1", new int[] { 1, 2 }), Times.Once);
        }

        [TestMethod]
        public void Delete_WykonujeUsuniecie()
        {
            _controller.Usun(1);

            _wykladowcaService.Verify(x => x.Usun(1), Times.Once);
        }
    }
}
