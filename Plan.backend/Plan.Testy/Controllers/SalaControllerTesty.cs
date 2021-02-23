using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Plan.API.Controllers;
using Plan.API.Komendy;
using Plan.Core.DTO;
using Plan.Core.Entities;
using Plan.Core.IServices;

namespace Plan.Testy.Controllers
{
    [TestClass]
    public class SalaControllerTesty
    {
        private Mock<ISalaService> _salaService;
        private SalaController _controller;

        [TestInitialize]
        public void Init()
        {
            _salaService = new Mock<ISalaService>();
            _controller = new SalaController(_salaService.Object);
        }

        [TestMethod]
        public void Get_ZwracaListe()
        {
            var lista = new SalaWidokDTO[]
            {
                new SalaWidokDTO()
            };
            _salaService.Setup(x => x.Przegladaj()).Returns(lista);

            var wynik = _controller.Przegladaj();
            Assert.IsNotNull(wynik);
            Assert.AreEqual(lista, wynik);
        }

        [TestMethod]
        public void Dodaj_WywolujeDodaniePrzedmiotu()
        {
            _controller.Dodaj(new KomendaDodajSale
            {
                Nazwa = "100",
                Rodzaj = RodzajSali.Cwiczeniowa
            });

            _salaService.Verify(x => x.Dodaj("100", RodzajSali.Cwiczeniowa), Times.Once);
        }

        [TestMethod]
        public void Usun_WywolujeUsuniecie()
        {
            _controller.Usun(3);

            _salaService.Verify(x => x.Usun(3), Times.Once);
        }
    }
}
