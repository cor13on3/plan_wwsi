using System;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;

namespace PlanWWSI.Testy
{
    [TestFixture(Platform.Android)]
    public class AplikacjaTesty
    {
        IApp app;
        Platform platform;

        public AplikacjaTesty(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            app = AppInitializer.StartApp(platform);
        }

        [Test]
        public void WyborGrupyDzialaPrawidlowo()
        {
            app.EnterText("NrGrupyInput", "Z715");
            app.Tap("NrGrupySubmit");
            app.WaitForElement("PlanWyborDaty");
            var wyborDaty = app.Query("PlanWyborDaty").FirstOrDefault();
            Assert.IsNotNull(wyborDaty);
            Assert.AreEqual(DateTime.Now.ToString("dddd, dd-MM-yyyy"), wyborDaty.Text);
        }

        [Test]
        public void PlanZajecZawieraPoprawneDane()
        {
            app.EnterText("NrGrupyInput", "Z715");
            app.Tap("NrGrupySubmit");
            app.WaitForElement("PlanWyborDaty");

            app.Tap("DataWstecz");
            app.WaitForElement("ZjazdInfo");
            Assert.AreEqual("Zjazd 3. (odpracowanie)", app.Query("ZjazdInfo").First().Text);
            Assert.AreEqual("niedziela, 08-11-2020", app.Query("PlanWyborDaty").First().Text);
            Assert.IsNull(app.Query("LekcjaNazwa").FirstOrDefault());

            app.Tap("DataWstecz");
            Assert.AreEqual("Zjazd 3. (odpracowanie)", app.Query("ZjazdInfo").First().Text);
            Assert.AreEqual("sobota, 07-11-2020", app.Query("PlanWyborDaty").First().Text);
            Assert.AreEqual("Programowanie", app.Query("LekcjaNazwa").First().Text);
            Assert.AreEqual("inż. A. Kowalski", app.Query("LekcjaWykl").First().Text);
            Assert.AreEqual("208", app.Query("LekcjaSala").First().Text);
            Assert.AreEqual("08:00 - 09:35", app.Query("LekcjaCzas").First().Text);

            app.Tap("DataWstecz");
            Assert.AreEqual("Zjazd 3. (odpracowanie)", app.Query("ZjazdInfo").First().Text);
            Assert.AreEqual("piątek, 06-11-2020", app.Query("PlanWyborDaty").First().Text);
            Assert.IsNull(app.Query("LekcjaNazwa").FirstOrDefault());

            app.Tap("DataWstecz");
            Assert.AreEqual("Zjazd 3.", app.Query("ZjazdInfo").First().Text);
            Assert.AreEqual("sobota, 31-10-2020", app.Query("PlanWyborDaty").First().Text);
            Assert.AreEqual("Algebra liniowa", app.Query("LekcjaNazwa").First().Text);
            Assert.AreEqual("inż. A. Kowalski", app.Query("LekcjaWykl").First().Text);
            Assert.AreEqual("110", app.Query("LekcjaSala").First().Text);
            Assert.AreEqual("08:00 - 09:35", app.Query("LekcjaCzas").First().Text);

            app.Tap("DataWstecz");
            Assert.AreEqual("Zjazd 3.", app.Query("ZjazdInfo").First().Text);
            Assert.AreEqual("piątek, 30-10-2020", app.Query("PlanWyborDaty").First().Text);
            Assert.AreEqual(2, app.Query("LekcjaNazwa").Length);
            Assert.AreEqual("Programowanie", app.Query("LekcjaNazwa")[0].Text);
            Assert.AreEqual("inż. A. Kowalski", app.Query("LekcjaWykl")[0].Text);
            Assert.AreEqual("208", app.Query("LekcjaSala")[0].Text);
            Assert.AreEqual("08:00 - 09:35", app.Query("LekcjaCzas")[0].Text);
            Assert.AreEqual("Przedsiębiorczość", app.Query("LekcjaNazwa")[1].Text);
            Assert.AreEqual("mgr A. Malinowska", app.Query("LekcjaWykl")[1].Text);
            Assert.AreEqual("210", app.Query("LekcjaSala")[1].Text);
            Assert.AreEqual("09:45 - 11:15", app.Query("LekcjaCzas")[1].Text);

            app.Tap("DataWstecz");
            app.Tap("DataWstecz");
            app.Tap("DataWstecz");
            Assert.AreEqual("Zjazd 2.", app.Query("ZjazdInfo").First().Text);
            Assert.AreEqual("piątek, 16-10-2020", app.Query("PlanWyborDaty").First().Text);
            Assert.AreEqual(1, app.Query("LekcjaNazwa").Length);
            Assert.AreEqual("Przedsiębiorczość", app.Query("LekcjaNazwa")[0].Text);
            Assert.AreEqual("mgr A. Malinowska", app.Query("LekcjaWykl")[0].Text);
            Assert.AreEqual("210", app.Query("LekcjaSala")[0].Text);
            Assert.AreEqual("09:45 - 11:15", app.Query("LekcjaCzas")[0].Text);
        }

        [Test]
        public void ListaWykladowcowDzialaPrawidlowo()
        {
            app.EnterText("NrGrupyInput", "Z715");
            app.Tap("NrGrupySubmit");
            app.WaitForElement("PlanWyborDaty");

            app.SwipeLeftToRight(0.99);
            app.Tap(x => x.Text("Wykładowcy"));
            app.WaitForElement("WykladowcaNazwa");
            Assert.AreEqual(2, app.Query("WykladowcaNazwa").Length);
            Assert.AreEqual("inż. A. Kowalski", app.Query("WykladowcaNazwa")[0].Text);
            Assert.AreEqual("a.kowalski@wwsi.edu.pl", app.Query("WykladowcaEmail")[0].Text);
            Assert.AreEqual("mgr A. Malinowska", app.Query("WykladowcaNazwa")[1].Text);
            Assert.AreEqual("a.malinowska@wwsi.edu.pl", app.Query("WykladowcaEmail")[1].Text);

        }

        [Test]
        public void WyszukiwanieWykladowcowDzialaPrawidlowo()
        {
            app.EnterText("NrGrupyInput", "Z715");
            app.Tap("NrGrupySubmit");
            app.WaitForElement("PlanWyborDaty");

            app.SwipeLeftToRight(0.99);
            app.Tap(x => x.Text("Wykładowcy"));
            app.WaitForElement("WykladowcaNazwa");
            Assert.AreEqual(2, app.Query("WykladowcaNazwa").Length);
            app.EnterText(c => c.Marked("Szukajka"), "m");
            Assert.AreEqual(1, app.Query("WykladowcaNazwa").Length);
            Assert.AreEqual("mgr A. Malinowska", app.Query("WykladowcaNazwa")[0].Text);
            app.ClearText(c => c.Marked("Szukajka"));
            Assert.AreEqual(2, app.Query("WykladowcaNazwa").Length);
        }

        [Test]
        public void WyswietlaSzczegolyWykladowcy()
        {
            app.EnterText("NrGrupyInput", "Z715");
            app.Tap("NrGrupySubmit");
            app.WaitForElement("PlanWyborDaty");

            app.SwipeLeftToRight(0.99);
            app.Tap(x => x.Text("Wykładowcy"));
            app.WaitForElement("WykladowcaNazwa");
            app.Tap("WykladowcaNazwa");
            app.WaitForElement("SzczegWyklNazwa");
            Assert.AreEqual("inż. Adam Kowalski", app.Query("SzczegWyklNazwa")[0].Text);
            Assert.AreEqual("a.kowalski@wwsi.edu.pl", app.Query("SzczegWyklEmail")[0].Text);
            Assert.AreEqual("Programowanie", app.Query("SzczegWyklSpec")[0].Text);
        }
    }
}
