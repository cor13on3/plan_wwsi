using Microsoft.AspNetCore.Mvc;
using Plan.Core.DTO;
using Plan.Core.IServices;
using Plan.Komendy;

namespace Plan.API.Controllers
{
    [Route("api/uzytkownik")]
    [ApiController]
    public class UzytkownikController : Controller
    {
        private IUzytkownikService _uzytkownikService;

        public UzytkownikController(IUzytkownikService uzytkownikService)
        {
            _uzytkownikService = uzytkownikService;
        }

        [HttpPost("dodaj")]
        public void Dodaj([FromBody] KomendaDodajUzytkownika req)
        {
            _uzytkownikService.Dodaj(req.Imie, req.Nazwisko, req.Email, req.Haslo);
        }

        [HttpGet("czy-zalogowany")]
        public bool CzyZalogowany()
        {
            return User.Identity.IsAuthenticated;
        }

        [HttpPost("zaloguj")]
        public DaneUzytkownikaDTO Zaloguj([FromBody] KomendaZaloguj req)
        {
            return _uzytkownikService.Zaloguj(req.Email, req.Haslo);
        }

        [HttpPost("wyloguj")]
        public void Wyloguj()
        {
            _uzytkownikService.Wyloguj();
        }
    }
}
