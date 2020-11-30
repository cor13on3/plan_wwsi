using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Plan.API.Komendy;
using Plan.Core.DTO;
using Plan.Core.IServices;

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
            _wykladowcaService.Dodaj(dto.Tytul, dto.Imie, dto.Nazwisko, dto.Email, dto.Specjalnosci);
        }

        [HttpGet]
        public WykladowcaWidokDTO[] Przegladaj()
        {
            return _wykladowcaService.Przegladaj();
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
            _wykladowcaService.Zmien(id, dto.Tytul, dto.Imie, dto.Nazwisko, dto.Email, dto.Specjalnosci);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public void Delete(int id)
        {
            _wykladowcaService.Usun(id);
        }
    }
}
