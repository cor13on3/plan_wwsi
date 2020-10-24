using Microsoft.AspNetCore.Mvc;
using Plan.Core.DTO;
using Plan.Core.IServices;

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
        public void Dodaj([FromBody] UzytkownikDTO dto)
        {
            _uzytkownikService.Dodaj(dto.Imie, dto.Nazwisko, dto.Email, dto.Haslo);
        }

        [HttpPost("zaloguj")]
        public DaneUzytkownikaDTO Zaloguj([FromBody] LogowanieDTO dto)
        {
            return _uzytkownikService.Zaloguj(dto.Email, dto.Haslo);
        }

        [HttpPost("wyloguj")]
        public void Wyloguj()
        {
            _uzytkownikService.Wyloguj();
        }
    }
}
