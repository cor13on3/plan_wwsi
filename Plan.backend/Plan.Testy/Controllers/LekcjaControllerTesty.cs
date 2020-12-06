using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Plan.API.Controllers;
using Plan.API.Komendy;
using Plan.Core.DTO;
using Plan.Core.Entities;
using Plan.Core.IServices;
using System;

namespace Plan.Testy
{
    [TestClass]
    public class LekcjaControllerTesty
    {
        private Mock<ILekcjaService> _lekcjaService;
        private LekcjaController _controller;

        [TestInitialize]
        public void Init()
        {
            _lekcjaService = new Mock<ILekcjaService>();
            _controller = new LekcjaController(_lekcjaService.Object);
        }

        [TestMethod]
        public void Dodaj_WywolujeDodanieLekcji()
        {
            _controller.DodajLekcje(new KomendaDodajLekcje
            {
                IdWykladowcy = 1,
                IdPrzedmiotu = 2,
                IdSali = 3,
                GodzinaOd = "08:00:00",
                GodzinaDo = "09:35:00",
                Forma = FormaLekcji.Wyklad
            });

            _lekcjaService.Verify(x => x.Dodaj(2, 1, 3, 0, "08:00:00", "09:35:00", FormaLekcji.Wyklad), Times.Once);
        }

        [TestMethod]
        public void PrzypiszGrupe_WywolujePrzypisanieGrupy()
        {
            _controller.PrzypiszGrupe(new KomendaPrzypiszGrupeLekcji
            {
                IdLekcji = 1,
                NrZjazdu = 2,
                CzyOdpracowanie = false,
                NrGrupy = "Z101"
            });

            _lekcjaService.Verify(x => x.PrzypiszGrupe(1, "Z101", 2, false), Times.Once);
        }

        [TestMethod]
        public void DajPlan_ZwracaPlan()
        {
            var plan = new LekcjaWidokDTO[]
            {
                new LekcjaWidokDTO{ Nazwa = "N1"}
            };
            _lekcjaService.Setup(x => x.DajPlanGrupyNaDzien(It.IsAny<DateTime>(), It.IsAny<string>())).Returns(plan);

            var wynik = _controller.DajPlanNaDzien(new DateTime(), "Z101");
            Assert.IsNotNull(wynik);
            Assert.AreEqual(1, wynik.Length);
            Assert.AreEqual("N1", wynik[0].Nazwa);
        }
    }
}
