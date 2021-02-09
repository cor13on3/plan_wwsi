using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Plan.API.Komendy;
using Plan.Core.DTO;
using Plan.Core.IServices;
using System;

namespace Plan.API.Controllers
{
    [Route("api/wykladowca")]
    [ApiController]
    public class WykladowcaController : ControllerBase
    {
        private IWykladowcaService _wykladowcaService;

        public WykladowcaController(IWykladowcaService wykladowcaService)
        {
            _wykladowcaService = wykladowcaService;
        }

        [HttpPost]
        [Authorize]
        public void Dodaj([FromBody] KomendaDodajWykladowce dto)
        {
            _wykladowcaService.Dodaj(dto.Tytul, dto.Imie, dto.Nazwisko, dto.Email, dto.Specjalizacje);
        }

        [HttpGet]
        public WykladowcaWidokDTO[] Przegladaj([FromQuery] string fraza)
        {
            return _wykladowcaService.Przegladaj(fraza);
        }

        [HttpGet("plan/{id}/{data}")]
        public LekcjaWidokDTO[] DajPlanWykladowcy(int id, DateTime data)
        {
            return _wykladowcaService.DajPlan(id, data);
        }

        [HttpGet("{id}")]
        public WykladowcaDTO Daj(int id)
        {
            return _wykladowcaService.Daj(id);
        }

        [HttpPut("{id}")]
        [Authorize]
        public void Zmien(int id, [FromBody] KomendaEdytujWykladowce dto)
        {
            _wykladowcaService.Zmien(id, dto.Tytul, dto.Imie, dto.Nazwisko, dto.Email, dto.Specjalizacje);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public void Usun(int id)
        {
            _wykladowcaService.Usun(id);
        }
    }
}
