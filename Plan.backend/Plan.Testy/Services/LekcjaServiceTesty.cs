using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Plan.Core.DTO;
using Plan.Core.Entities;
using Plan.Core.Exceptions;
using Plan.Core.IDatabase;
using Plan.Core.Services;
using Plan.Core.Zapytania;
using Plan.Testy.Helpers;
using System;

namespace Plan.Testy.Services
{
    [TestClass]
    public class LekcjaServiceTesty
    {
        private Mock<IBazaDanych> _db;
        private Mock<IRepozytorium<GrupaZjazd>> _repoGrupaZjazd;
        private Mock<IRepozytorium<Zjazd>> _repoZjazd;
        private Mock<IRepozytorium<Grupa>> _repoGrupa;
        private Mock<IRepozytorium<LekcjaGrupa>> _repoLekcjaGrupa;
        private Mock<IRepozytorium<Przedmiot>> _repoPrzedmiot;
        private Mock<IRepozytorium<Wykladowca>> _repoWykladowca;
        private Mock<IRepozytorium<Sala>> _repoSala;
        private Mock<IRepozytorium<Lekcja>> _repoLekcja;
        private LekcjaService _service;

        public LekcjaServiceTesty()
        {
            _db = new Mock<IBazaDanych>();
            _repoGrupaZjazd = new Mock<IRepozytorium<GrupaZjazd>>();
            _repoZjazd = new Mock<IRepozytorium<Zjazd>>();
            _repoGrupa = new Mock<IRepozytorium<Grupa>>();
            _repoLekcjaGrupa = new Mock<IRepozytorium<LekcjaGrupa>>();
            _repoPrzedmiot = new Mock<IRepozytorium<Przedmiot>>();
            _repoWykladowca = new Mock<IRepozytorium<Wykladowca>>();
            _repoSala = new Mock<IRepozytorium<Sala>>();
            _repoLekcja = new Mock<IRepozytorium<Lekcja>>();
            _db.Setup(x => x.DajTabele<GrupaZjazd>()).Returns(_repoGrupaZjazd.Object);
            _db.Setup(x => x.DajTabele<Zjazd>()).Returns(_repoZjazd.Object);
            _db.Setup(x => x.DajTabele<Grupa>()).Returns(_repoGrupa.Object);
            _db.Setup(x => x.DajTabele<LekcjaGrupa>()).Returns(_repoLekcjaGrupa.Object);
            _db.Setup(x => x.DajTabele<Przedmiot>()).Returns(_repoPrzedmiot.Object);
            _db.Setup(x => x.DajTabele<Wykladowca>()).Returns(_repoWykladowca.Object);
            _db.Setup(x => x.DajTabele<Sala>()).Returns(_repoSala.Object);
            _db.Setup(x => x.DajTabele<Lekcja>()).Returns(_repoLekcja.Object);
            _service = new LekcjaService(_db.Object);
        }

        [TestMethod]
        public void DajPlan_ZwracaPustyGdyBrakZjazdu()
        {
            _repoGrupaZjazd.Setup(x => x.Wybierz(It.IsAny<ZapytanieZjadyGrupy>())).Returns(new ZjazdWidokDTO[0]);

            var wynik = _service.DajPlanGrupyNaDzien(new System.DateTime(), "Z101");

            Assert.AreEqual(0, wynik.Length);
        }

        [TestMethod]
        public void DajPlanGrupyNaDzien_ZwracaPoprawnyWynik()
        {
            _repoGrupaZjazd.Setup(x => x.Wybierz(It.IsAny<ZapytanieZjadyGrupy>())).Returns(new ZjazdWidokDTO[]
            {
                new ZjazdWidokDTO{ Nr = 2, CzyOdpracowanie = true }
            });
            _repoLekcjaGrupa.Setup(x => x.Wybierz(It.IsAny<ZapytanieLekcjeGrupy>())).Returns(new LekcjaWidokDTO[]
            {
                new LekcjaWidokDTO
                {
                    Nazwa = "N1",
                    Od = "08",
                    Do = "09",
                    Forma = FormaLekcji.Cwiczenia,
                    CzyOdpracowanie = true,
                    Sala = "210",
                    Wykladowca = "W1"
                }
            });

            var wynik = _service.DajPlanGrupyNaDzien(new DateTime(), "Z101");

            _repoLekcjaGrupa.Verify(x => x.Wybierz(It.Is<ZapytanieLekcjeGrupy>(x =>
                x.DzienTygodnia == (int)new DateTime().DayOfWeek &&
                x.NrGrupy == "Z101" &&
                x.NrZjazdu == 2
            )), Times.Once);
            Assert.IsNotNull(wynik);
            Assert.AreEqual(1, wynik.Length);
            Assert.AreEqual("N1", wynik[0].Nazwa);
            Assert.AreEqual("08", wynik[0].Od);
            Assert.AreEqual("09", wynik[0].Do);
            Assert.AreEqual(FormaLekcji.Cwiczenia, wynik[0].Forma);
            Assert.AreEqual(true, wynik[0].CzyOdpracowanie);
            Assert.AreEqual("210", wynik[0].Sala);
            Assert.AreEqual("W1", wynik[0].Wykladowca);
        }

        [TestMethod]
        public void Dodaj_WyjatekPrzedmiotNieIstnieje()
        {
            _repoPrzedmiot.Setup(x => x.Znajdz(It.IsAny<int>())).Returns((Przedmiot)null);

            AssertHelper.OczekiwanyWyjatek<BladBiznesowy>(() => _service.Dodaj(1, 2, 3, "08:00:00", "09:35:00", FormaLekcji.Cwiczenia),
                "Przedmiot o id 1 nie istnieje.");
        }

        [TestMethod]
        public void Dodaj_WyjatekWykladowcaNieIstnieje()
        {
            _repoPrzedmiot.Setup(x => x.Znajdz(It.IsAny<int>())).Returns(new Przedmiot());
            _repoWykladowca.Setup(x => x.Znajdz(It.IsAny<int>())).Returns((Wykladowca)null);

            AssertHelper.OczekiwanyWyjatek<BladBiznesowy>(() => _service.Dodaj(1, 2, 3, "08:00:00", "09:35:00", FormaLekcji.Cwiczenia),
                "Wykładowca o id 2 nie istnieje.");
        }

        [TestMethod]
        public void Dodaj_WyjatekSalaNieIstnieje()
        {
            _repoPrzedmiot.Setup(x => x.Znajdz(It.IsAny<int>())).Returns(new Przedmiot());
            _repoWykladowca.Setup(x => x.Znajdz(It.IsAny<int>())).Returns(new Wykladowca());
            _repoSala.Setup(x => x.Znajdz(It.IsAny<int>())).Returns((Sala)null);

            AssertHelper.OczekiwanyWyjatek<BladBiznesowy>(() => _service.Dodaj(1, 2, 3, "08:00:00", "09:35:00", FormaLekcji.Cwiczenia),
                "Sala o id 3 nie istnieje.");
        }

        [TestMethod]
        public void Dodaj_WyjatekNiepoprawnyFormatGodziny()
        {
            _repoPrzedmiot.Setup(x => x.Znajdz(It.IsAny<int>())).Returns(new Przedmiot());
            _repoWykladowca.Setup(x => x.Znajdz(It.IsAny<int>())).Returns(new Wykladowca());
            _repoSala.Setup(x => x.Znajdz(It.IsAny<int>())).Returns(new Sala());

            AssertHelper.OczekiwanyWyjatek<BladBiznesowy>(() => _service.Dodaj(1, 2, 3, "080000", "093500", FormaLekcji.Cwiczenia),
                "Podano niepoprawny format godziny. Podaj godzinę w formacie HH:mm (np. 09:45)");
        }

        [TestMethod]
        public void Dodaj_DodajeLekcje()
        {
            _repoPrzedmiot.Setup(x => x.Znajdz(It.IsAny<int>())).Returns(new Przedmiot());
            _repoWykladowca.Setup(x => x.Znajdz(It.IsAny<int>())).Returns(new Wykladowca());
            _repoSala.Setup(x => x.Znajdz(It.IsAny<int>())).Returns(new Sala());

            _service.Dodaj(1, 2, 3, "08:00", "09:35", FormaLekcji.Cwiczenia);

            _repoLekcja.Verify(x => x.Dodaj(It.Is<Lekcja>(x =>
                x.IdPrzedmiotu == 1 &&
                x.IdWykladowcy == 2 &&
                x.IdSali == 3 &&
                x.GodzinaOd == "08:00" &&
                x.GodzinaDo == "09:35" &&
                x.Forma == FormaLekcji.Cwiczenia
            )), Times.Once);
        }

        [TestMethod]
        public void PrzypiszGrupe_WyjatekLekcjaNieIstnieje()
        {
            _repoLekcja.Setup(x => x.Znajdz(It.IsAny<int>())).Returns((Lekcja)null);

            AssertHelper.OczekiwanyWyjatek<BladBiznesowy>(() => _service.PrzypiszGrupe(1, "Z101", 2, 3, true)
            , "Lekcja o id 1 nie istnieje.");
        }

        [TestMethod]
        public void PrzypiszGrupe_WyjatekGrupaNieIstnieje()
        {
            _repoLekcja.Setup(x => x.Znajdz(It.IsAny<int>())).Returns(new Lekcja());
            _repoGrupa.Setup(x => x.Znajdz(It.IsAny<string>())).Returns((Grupa)null);

            AssertHelper.OczekiwanyWyjatek<BladBiznesowy>(() => _service.PrzypiszGrupe(1, "Z101", 2, 3, true)
            , "Grupa o numerze Z101 nie istnieje.");
        }

        [TestMethod]
        public void PrzypiszGrupe_WyjatekZlyNrZjazdu()
        {
            _repoLekcja.Setup(x => x.Znajdz(It.IsAny<int>())).Returns(new Lekcja());
            _repoGrupa.Setup(x => x.Znajdz(It.IsAny<string>())).Returns(new Grupa());
            AssertHelper.OczekiwanyWyjatek<BladBiznesowy>(() => _service.PrzypiszGrupe(1, "Z101", -1, 3, true)
            , "Podano niepoprawny numer zjazdu.");
        }

        [TestMethod]
        public void PrzypiszGrupe_WyjatekZlyDzienTygodnia()
        {
            _repoLekcja.Setup(x => x.Znajdz(It.IsAny<int>())).Returns(new Lekcja());
            _repoGrupa.Setup(x => x.Znajdz(It.IsAny<string>())).Returns(new Grupa());
            AssertHelper.OczekiwanyWyjatek<BladBiznesowy>(() => _service.PrzypiszGrupe(1, "Z101", 1, 13, true)
            , "Podano niepoprawny dzień tygodnia.");
        }

        [TestMethod]
        public void PrzypiszGrupe_ZjazdNieJestZjademOdpracowwywanym()
        {
            _repoLekcja.Setup(x => x.Znajdz(It.IsAny<int>())).Returns(new Lekcja());
            _repoGrupa.Setup(x => x.Znajdz(It.IsAny<string>())).Returns(new Grupa());
            _repoGrupaZjazd.Setup(x => x.Wybierz(It.IsAny<ZapytanieZjadyGrupy>())).Returns(new ZjazdWidokDTO[]
            {
                new ZjazdWidokDTO
                {
                    Nr = 1,
                    CzyOdpracowanie = false
                },
                new ZjazdWidokDTO
                {
                    Nr = 2, CzyOdpracowanie = true
                }
            });

            AssertHelper.OczekiwanyWyjatek<BladBiznesowy>(() => _service.PrzypiszGrupe(1, "Z101", 1, 0, true),
                "Brak ustalonej daty odpracowania zjazdu nr 1 dla grupy Z101. Dodaj zjazd z datą odpracowania.");
        }

        [TestMethod]
        public void PrzypiszGrupe_DodajePowiazanie()
        {
            _repoLekcja.Setup(x => x.Znajdz(It.IsAny<int>())).Returns(new Lekcja());
            _repoGrupa.Setup(x => x.Znajdz(It.IsAny<string>())).Returns(new Grupa());
            _repoGrupaZjazd.Setup(x => x.Wybierz(It.IsAny<ZapytanieZjadyGrupy>())).Returns(new ZjazdWidokDTO[]
            {
                new ZjazdWidokDTO
                {
                    Nr = 1,
                    CzyOdpracowanie = false
                },
                new ZjazdWidokDTO
                {
                    Nr = 2,
                    CzyOdpracowanie = true
                }
            });

            _service.PrzypiszGrupe(1, "Z101", 2, 1, true);

            _repoLekcjaGrupa.Verify(x => x.Dodaj(It.Is<LekcjaGrupa>(x =>
                x.IdLekcji == 1 &&
                x.NrGrupy == "Z101" &&
                x.NrZjazdu == 2 &&
                x.DzienTygodnia == 1 &&
                x.CzyOdpracowanie
            )), Times.Once);
            _db.Verify(x => x.Zapisz(), Times.Once);
        }

        [TestMethod]
        public void Usun_WyjatekLekcjaNieIstnieje()
        {
            _repoLekcja.Setup(x => x.Znajdz(It.IsAny<int>())).Returns((Lekcja)null);

            AssertHelper.OczekiwanyWyjatek<BladBiznesowy>(() => _service.Usun(1), "Nie istnieje lekcja o id 1");
        }

        [TestMethod]
        public void Usun_WywolujeUsuniecie()
        {
            _repoLekcja.Setup(x => x.Znajdz(It.IsAny<int>())).Returns(new Lekcja());
            _service.Usun(1);

            _repoLekcja.Verify(x => x.Usun(1), Times.Once);
            _db.Verify(x => x.Zapisz(), Times.Once);
        }

        [TestMethod]
        public void Zmien_WyjatekPrzedmiotNieIstnieje()
        {
            _repoPrzedmiot.Setup(x => x.Znajdz(It.IsAny<int>())).Returns((Przedmiot)null);

            AssertHelper.OczekiwanyWyjatek<BladBiznesowy>(() => _service.Zmien(1, 1, 2, 3, "08:00:00", "09:35:00", FormaLekcji.Cwiczenia),
                "Przedmiot o id 1 nie istnieje.");
        }

        [TestMethod]
        public void Zmien_WyjatekWykladowcaNieIstnieje()
        {
            _repoPrzedmiot.Setup(x => x.Znajdz(It.IsAny<int>())).Returns(new Przedmiot());
            _repoWykladowca.Setup(x => x.Znajdz(It.IsAny<int>())).Returns((Wykladowca)null);

            AssertHelper.OczekiwanyWyjatek<BladBiznesowy>(() => _service.Zmien(1, 1, 2, 3, "08:00:00", "09:35:00", FormaLekcji.Cwiczenia),
                "Wykładowca o id 2 nie istnieje.");
        }

        [TestMethod]
        public void Zmien_WyjatekSalaNieIstnieje()
        {
            _repoPrzedmiot.Setup(x => x.Znajdz(It.IsAny<int>())).Returns(new Przedmiot());
            _repoWykladowca.Setup(x => x.Znajdz(It.IsAny<int>())).Returns(new Wykladowca());
            _repoSala.Setup(x => x.Znajdz(It.IsAny<int>())).Returns((Sala)null);

            AssertHelper.OczekiwanyWyjatek<BladBiznesowy>(() => _service.Zmien(1, 1, 2, 3, "08:00:00", "09:35:00", FormaLekcji.Cwiczenia),
                "Sala o id 3 nie istnieje.");
        }

        [TestMethod]
        public void Zmien_WyjatekNiepoprawnyFormatGodziny()
        {
            _repoPrzedmiot.Setup(x => x.Znajdz(It.IsAny<int>())).Returns(new Przedmiot());
            _repoWykladowca.Setup(x => x.Znajdz(It.IsAny<int>())).Returns(new Wykladowca());
            _repoSala.Setup(x => x.Znajdz(It.IsAny<int>())).Returns(new Sala());

            AssertHelper.OczekiwanyWyjatek<BladBiznesowy>(() => _service.Zmien(1, 1, 2, 3, "080000", "093500", FormaLekcji.Cwiczenia),
                "Podano niepoprawny format godziny. Podaj godzinę w formacie HH:mm (np. 09:45)");
        }

        [TestMethod]
        public void Zmien_WyjatekLekcjaNieIstnieje()
        {
            _repoPrzedmiot.Setup(x => x.Znajdz(It.IsAny<int>())).Returns(new Przedmiot());
            _repoWykladowca.Setup(x => x.Znajdz(It.IsAny<int>())).Returns(new Wykladowca());
            _repoSala.Setup(x => x.Znajdz(It.IsAny<int>())).Returns(new Sala());
            _repoLekcja.Setup(x => x.Znajdz(It.IsAny<int>())).Returns((Lekcja)null);

            AssertHelper.OczekiwanyWyjatek<BladBiznesowy>(() => _service.Zmien(1, 1, 2, 3, "08:00", "09:35", FormaLekcji.Cwiczenia),
                "Nie istnieje lekcja o id 1");
        }

        [TestMethod]
        public void Zmien_WywolujeEdycje()
        {
            _repoPrzedmiot.Setup(x => x.Znajdz(It.IsAny<int>())).Returns(new Przedmiot());
            _repoWykladowca.Setup(x => x.Znajdz(It.IsAny<int>())).Returns(new Wykladowca());
            _repoSala.Setup(x => x.Znajdz(It.IsAny<int>())).Returns(new Sala());
            _repoLekcja.Setup(x => x.Znajdz(It.IsAny<int>())).Returns(new Lekcja
            {
                IdLekcji = 1,
                IdPrzedmiotu = 1,
                IdWykladowcy = 2,
                IdSali = 3,
                GodzinaOd = "13:45",
                GodzinaDo = "15:15",
                Forma = FormaLekcji.Wyklad
            });

            _service.Zmien(1, 10, 20, 30, "08:00", "09:35", FormaLekcji.Cwiczenia);

            _repoLekcja.Verify(x => x.Edytuj(It.Is<Lekcja>(x =>
                x.IdLekcji == 1 &&
                x.IdPrzedmiotu == 10 &&
                x.IdWykladowcy == 20 &&
                x.IdSali == 30 &&
                x.GodzinaOd == "08:00" &&
                x.GodzinaDo == "09:35" &&
                x.Forma == FormaLekcji.Cwiczenia
            )), Times.Once);
            _db.Verify(x => x.Zapisz(), Times.Once);
        }

    }
}
