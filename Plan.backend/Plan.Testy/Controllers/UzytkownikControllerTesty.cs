using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Plan.API.Controllers;
using Plan.API.Komendy;
using Plan.Core.DTO;
using Plan.Core.Entities;
using Plan.Core.IServices;
using Plan.Komendy;
using System;
using System.Net;

namespace Plan.Testy.Controllers
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

        //[TestMethod]
        //public void Dodaj_WyjatekAutoryzacji()
        //{
        //    _controller.ControllerContext = new ControllerContext
        //    {
        //        HttpContext = new DefaultHttpContext
        //        {
        //            Connection = { RemoteIpAddress = IPAddress.Parse("128.1.1.32") }
        //        }
        //    };
        //    Assert.ThrowsException<UnauthorizedAccessException>(() =>
        //    {
        //        _controller.Dodaj(new KomendaDodajUzytkownika
        //        {
        //            Imie = "I1",
        //            Nazwisko = "N1",
        //            Email = "E1",
        //            Haslo = "H1"
        //        });
        //    });
        //}

        [TestMethod]
        public void Dodaj_WywolujeDodanie()
        {
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    Connection = { RemoteIpAddress = IPAddress.Parse("127.0.0.1") }
                }
            };
            _controller.Dodaj(new KomendaDodajUzytkownika
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
            _controller.Zaloguj(new KomendaZaloguj
            {
                Email = "E1",
                Haslo = "H1"
            });

            _uzytkownikService.Verify(x => x.Zaloguj("E1", "H1"), Times.Once);
        }
    }
}
