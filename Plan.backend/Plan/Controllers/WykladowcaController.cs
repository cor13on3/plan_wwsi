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

        [HttpGet]
        public WykladowcaWidokDTO[] Get()
        {
            return _wykladowcaService.DajWykladowcow();
        }

        [HttpGet("{id}")]
        public WykladowcaDTO Get(int id)
        {
            return _wykladowcaService.Daj(id);
        }

        [Authorize]
        [HttpPost]
        public void Post([FromBody] KomendaDodajWykladowce dto)
        {
            _wykladowcaService.DodajWykladowce(dto.Tytul, dto.Imie, dto.Nazwisko, dto.Email, dto.Specjalnosci);
        }

        [Authorize]
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] KomendaEdytujWykladowce dto)
        {
            _wykladowcaService.ZmienWykladowce(id, dto.Tytul, dto.Imie, dto.Nazwisko, dto.Email, dto.Specjalnosci);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _wykladowcaService.UsunWykladowce(id);
        }
    }
}
