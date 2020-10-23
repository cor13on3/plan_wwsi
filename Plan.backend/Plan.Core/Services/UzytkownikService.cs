using Microsoft.AspNetCore.Identity;
using Plan.Core.Entities;
using Plan.Core.Exceptions;
using Plan.Core.IServices;

namespace Plan.Core.Services
{
    public class UzytkownikService : IUzytkownikService
    {
        private UserManager<Uzytkownik> _userManager;
        private SignInManager<Uzytkownik> _signInManager;

        public UzytkownikService(UserManager<Uzytkownik> userManager, SignInManager<Uzytkownik> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public void Dodaj(string imie, string nazwisko, string email, string haslo)
        {
            var res = _userManager.FindByNameAsync(email).Result;
            if (res != null)
                throw new BladBiznesowy("Użytkownik o podanym adresie e-mail jest już zarejestrowany.");
            var user = new Uzytkownik(email)
            {
                Imie = imie,
                Nazwisko = nazwisko
            };
            _userManager.CreateAsync(user, haslo).Wait();
        }

        public void Zaloguj(string email, string haslo)
        {
            var res = _signInManager.PasswordSignInAsync(email, haslo, false, false).Result;
            if (!res.Succeeded)
                throw new BladBiznesowy("Podano nieprawidłowy e-mail lub hasło.");
        }
    }
}
