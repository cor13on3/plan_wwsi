using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Plan.API.Komendy;
using Plan.Core.DTO;
using Plan.Core.IServices;

namespace Plan.API.Controllers
{
    [Route("api/sala")]
    [ApiController]
    public class SalaController : ControllerBase
    {
        private ISalaService _salaService;

        public SalaController(ISalaService salaService)
        {
            _salaService = salaService;
        }

        [HttpGet]
        public IEnumerable<SalaWidokDTO> Przegladaj()
        {
            return _salaService.Przegladaj();
        }

        [HttpPost]
        public void Dodaj([FromBody] KomendaDodajSale req)
        {
            _salaService.Dodaj(req.Nazwa, req.Rodzaj);
        }

        [HttpDelete("{id}")]
        public void Usun(int id)
        {
            _salaService.Usun(id);
        }
    }
}
