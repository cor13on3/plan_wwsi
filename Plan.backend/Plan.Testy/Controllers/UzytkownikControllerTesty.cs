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
    public class UzytkownikControllerTesty
    {
        private Mock<IUzytkownikService> _uzytkownikService;
        private UzytkownikController _controller;

        [TestInitialize]
        public void Init()
        {
            _uzytkownikService = new Mock<IUzytkownikService>();
            _controller = new UzytkownikController(_uzytkownikService.Object);
        }
        
        [TestMethod]
        public void Dodaj_WywolujeDodanie()
        {
            _controller.Dodaj(new UzytkownikDTO
            {
                Imie = "I1",
                Nazwisko = "N1",
                Email = "E1",
                Haslo = "H1"
            });

            _uzytkownikService.Verify(x => x.Dodaj("I1","N1","E1","H1"), Times.Once);
        }

        [TestMethod]
        public void Zaloguj_WywolujeLogowanie()
        {
            _controller.Zaloguj(new LogowanieDTO
            {
                Email = "E1",
                Haslo = "H1"
            });

            _uzytkownikService.Verify(x => x.Zaloguj("E1", "H1"), Times.Once);
        }
    }
}
