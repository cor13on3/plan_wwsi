﻿using Microsoft.AspNetCore.Mvc;
using Plan.Core.DTO;
using Plan.Core.IServices;

namespace Plan.Controllers
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
        public void Zaloguj([FromBody] UzytkownikDTO dto)
        {
            _uzytkownikService.Zaloguj(dto.Email, dto.Haslo);
        }
    }
}
