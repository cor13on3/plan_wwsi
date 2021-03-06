using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Plan.Core.Entities;
using Plan.Core.Exceptions;
using Plan.Core.Services;
using Plan.Testy.Helpers;
using System.Threading.Tasks;

namespace Plan.Testy.Services
{
    public class FakeSignInManager : SignInManager<Uzytkownik>
    {
        public FakeSignInManager()
                : base(new Mock<UserManager<Uzytkownik>>(new Mock<IUserStore<Uzytkownik>>().Object, null, null, null, null, null, null, null, null).Object,
                     new Mock<IHttpContextAccessor>().Object,
                     new Mock<IUserClaimsPrincipalFactory<Uzytkownik>>().Object,
                     new Mock<IOptions<IdentityOptions>>().Object,
                     new Mock<ILogger<SignInManager<Uzytkownik>>>().Object,
                     new Mock<IAuthenticationSchemeProvider>().Object,
                     new Mock<IUserConfirmation<Uzytkownik>>().Object)
        { }
    }

    [TestClass]
    public class UzytkownikServiceTesty
    {
        private Mock<UserManager<Uzytkownik>> _userManager;
        private Mock<FakeSignInManager> _signInManager;
        private UzytkownikService _service;

        public UzytkownikServiceTesty()
        {
            _userManager = new Mock<UserManager<Uzytkownik>>(new Mock<IUserStore<Uzytkownik>>().Object, null, null, null, null, null, null, null, null);
            _signInManager = new Mock<FakeSignInManager>();
            _service = new UzytkownikService(_userManager.Object, _signInManager.Object);
        }

        [TestMethod]
        public void Dodaj_WyjatekUzytkownikJuzIstnieje()
        {
            _userManager.Setup(x => x.FindByNameAsync(It.IsAny<string>())).ReturnsAsync(new Uzytkownik());

            AssertHelper.OczekiwanyWyjatekAsync<BladBiznesowy>(() => _service.Dodaj("I1", "N1", "E1", "H1"),
                "Użytkownik o podanym adresie e-mail jest już zarejestrowany.");
        }

        [TestMethod]
        public async Task Dodaj_WywolujeDodanie()
        {
            _userManager.Setup(x => x.FindByNameAsync(It.IsAny<string>())).ReturnsAsync((Uzytkownik)null);
            await _service.Dodaj("I1", "N1", "E1", "H1");

            _userManager.Verify(x => x.CreateAsync(It.Is<Uzytkownik>(x =>
                x.Imie == "I1" &&
                x.Nazwisko == "N1"
            ), "H1"), Times.Once);
        }

        [TestMethod]
        public void Zaloguj_WyjatekNiepoprawneDaneLogowania()
        {
            _signInManager.Setup(x => x.PasswordSignInAsync(It.IsAny<string>(), It.IsAny<string>(), false, false))
                .ReturnsAsync(SignInResult.Failed);

            AssertHelper.OczekiwanyWyjatekAsync<BladBiznesowy>(() => _service.Zaloguj("E1", "H1"),
                "Podano nieprawidłowy e-mail lub hasło.");
        }

        [TestMethod]
        public async Task Zaloguj_ZwracaDaneUzytkownika()
        {
            _signInManager.Setup(x => x.PasswordSignInAsync(It.IsAny<string>(), It.IsAny<string>(), false, false))
                .ReturnsAsync(SignInResult.Success);
            _userManager.Setup(x => x.FindByNameAsync(It.IsAny<string>())).ReturnsAsync(new Uzytkownik
            {
                Email = "a@a.pl",
                Imie = "A",
                Nazwisko = "B"
            });

            var wynik = await _service.Zaloguj("E1", "H1");

            Assert.IsNotNull(wynik);
            Assert.AreEqual("a@a.pl", wynik.Email);
            Assert.AreEqual("A", wynik.Imie);
            Assert.AreEqual("B", wynik.Nazwisko);
        }
    }
}
