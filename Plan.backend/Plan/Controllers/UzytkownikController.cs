using Microsoft.AspNetCore.Mvc;
using Plan.Core.DTO;
using Plan.Core.IServices;
using Plan.Komendy;
using System;
using System.Threading.Tasks;

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
        public async Task Dodaj([FromBody] KomendaDodajUzytkownika req)
        {
            var ip = HttpContext.Connection.RemoteIpAddress?.ToString();
            if (ip != "127.0.0.1" && ip != "::1")
                throw new UnauthorizedAccessException();
            await _uzytkownikService.Dodaj(req.Imie, req.Nazwisko, req.Email, req.Haslo);
        }

        [HttpGet("dodaj")]
        public async Task DodajGet([FromQuery] string imie, [FromQuery] string nazwisko, [FromQuery] string email, [FromQuery] string haslo)
        {
            var ip = HttpContext.Connection.RemoteIpAddress?.ToString();
            if (ip != "127.0.0.1" && ip != "::1")
                throw new UnauthorizedAccessException();
            await _uzytkownikService.Dodaj(imie, nazwisko, email, haslo);
        }

        [HttpGet("czy-zalogowany")]
        public bool CzyZalogowany()
        {
            return User.Identity.IsAuthenticated;
        }

        [HttpPost("zaloguj")]
        public async Task<DaneUzytkownikaDTO> Zaloguj([FromBody] KomendaZaloguj req)
        {
            return await _uzytkownikService.Zaloguj(req.Email, req.Haslo);
        }

        [HttpPost("wyloguj")]
        public void Wyloguj()
        {
            _uzytkownikService.Wyloguj();
        }
    }
}
