using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Plan.API.Controllers;
using Plan.API.Komendy;
using Plan.Core.DTO;
using Plan.Core.Entities;
using Plan.Core.IServices;

namespace Plan.Testy
{
    [TestClass]
    public class GrupaControllerTesty
    {
        private Mock<IGrupaService> _grupaService;
        private GrupaController _controller;

        [TestInitialize]
        public void Init()
        {
            _grupaService = new Mock<IGrupaService>();
            _controller = new GrupaController(_grupaService.Object);
        }

        [TestMethod]
        public void Przegladaj_ZwracaListeGrup()
        {
            _grupaService.Setup(x => x.Przegladaj(null)).Returns(new GrupaDTO[]
            {
                new GrupaDTO
                {
                    Numer = "Z101"
                },
                new GrupaDTO
                {
                    Numer = "Z102"
                }
            });

            var wynik = _controller.Przegladaj(null);

            Assert.IsNotNull(wynik);
            Assert.AreEqual(2, wynik.Length);
            Assert.AreEqual("Z101", wynik[0].Numer);
            Assert.AreEqual("Z102", wynik[1].Numer);
        }

        [TestMethod]
        public void Post_WywolujeDodanieGrupy()
        {
            _controller.Dodaj(new KomendaDodajGrupe
            {
                NrGrupy = "Z101",
                Semestr = 1,
                StopienStudiow = StopienStudiow.Inzynierskie,
                TrybStudiow = TrybStudiow.Niestacjonarne
            });

            _grupaService.Verify(x => x.Dodaj("Z101", 1, TrybStudiow.Niestacjonarne, StopienStudiow.Inzynierskie), Times.Once);
        }

        [TestMethod]
        public void Delete_WywolujeUsuniecieGrupy()
        {
            _controller.Usun("Z101");
            _grupaService.Verify(x => x.Usun("Z101"), Times.Once);
        }
    }
}
