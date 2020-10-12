using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Plan.API.DTO;
using Plan.API.Komendy;
using Plan.Interfejsy;

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
        public IEnumerable<WykladowcaWidokDTO> Get()
        {
            var res = _wykladowcaService.DajWykladowcow();
            return res;
        }

        [HttpGet("{id}")]
        public WykladowcaDTO Get(int id)
        {
            return _wykladowcaService.Daj(id);
        }

        [HttpPost]
        public void Post([FromBody] KomendaDodajWykladowce dto)
        {
            _wykladowcaService.DodajWykladowce(dto.Tytul, dto.Imie, dto.Nazwisko, dto.Email, dto.Specjalnosci);
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] KomendaDodajWykladowce dto)
        {
            _wykladowcaService.ZmienWykladowce(id, dto.Tytul, dto.Imie, dto.Nazwisko, dto.Email, dto.Specjalnosci);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _wykladowcaService.UsunWykladowce(id);
        }
    }
}
