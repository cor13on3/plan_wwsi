using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Plan.API.Controllers;
using Plan.Core.DTO;
using Plan.Core.IServices;

namespace Plan.Testy.Controllers
{
    [TestClass]
    public class PrzedmiotControllerTesty
    {
        private Mock<IPrzedmiotService> _przedmiotService;
        private PrzedmiotController _controller;

        [TestInitialize]
        public void Init()
        {
            _przedmiotService = new Mock<IPrzedmiotService>();
            _controller = new PrzedmiotController(_przedmiotService.Object);
        }

        [TestMethod]
        public void Get_ZwracaListePrzedmiotow()
        {
            var lista = new PrzedmiotWidokDTO[]
            {
                new PrzedmiotWidokDTO()
            };
            _przedmiotService.Setup(x => x.Przegladaj()).Returns(lista);

            var wynik = _controller.Przegladaj();
            Assert.IsNotNull(wynik);
            Assert.AreEqual(lista, wynik);
        }

        [TestMethod]
        public void Dodaj_WywolujeDodaniePrzedmiotu()
        {
            _controller.Dodaj("Programowanie");

            _przedmiotService.Verify(x => x.Dodaj("Programowanie"), Times.Once);
        }

        [TestMethod]
        public void Usun_WywolujeUsunieciePrzedmiotu()
        {
            _controller.Usun(3);

            _przedmiotService.Verify(x => x.Usun(3), Times.Once);
        }
    }
}
