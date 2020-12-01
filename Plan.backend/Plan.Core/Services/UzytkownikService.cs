using Microsoft.AspNetCore.Identity;
using Plan.Core.DTO;
using Plan.Core.Entities;
using Plan.Core.Exceptions;
using Plan.Core.IServices;
using System.Threading.Tasks;

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

        public async Task Dodaj(string imie, string nazwisko, string email, string haslo)
        {
            var res = await _userManager.FindByNameAsync(email);
            if (res != null)
                throw new BladBiznesowy("Użytkownik o podanym adresie e-mail jest już zarejestrowany.");
            var user = new Uzytkownik(email)
            {
                Imie = imie,
                Nazwisko = nazwisko
            };
            _userManager.CreateAsync(user, haslo).Wait();
        }

        public async Task<DaneUzytkownikaDTO> Zaloguj(string email, string haslo)
        {
            var wynik = await _signInManager.PasswordSignInAsync(email, haslo, false, false);
            if (!wynik.Succeeded)
                throw new BladBiznesowy("Podano nieprawidłowy e-mail lub hasło.");
            var uzytkownik = await _userManager.FindByNameAsync(email);
            return new DaneUzytkownikaDTO
            {
                Email = uzytkownik.Email,
                Imie = uzytkownik.Imie,
                Nazwisko = uzytkownik.Nazwisko
            };
        }

        public void Wyloguj()
        {
            _signInManager.SignOutAsync();
        }
    }
}
