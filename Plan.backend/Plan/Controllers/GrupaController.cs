using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Plan.API.Komendy;
using Plan.Core.DTO;
using Plan.Core.Entities;
using Plan.Core.IServices;

namespace Plan.API.Controllers
{
    [Route("api/grupa")]
    [ApiController]
    public class GrupaController : ControllerBase
    {
        private IGrupaService _grupaService;

        public GrupaController(IGrupaService grupaService)
        {
            _grupaService = grupaService;
        }

        [HttpPost]
        [Authorize]
        public void Dodaj([FromBody] KomendaDodajGrupe req)
        {
            _grupaService.Dodaj(req.NrGrupy, req.Semestr, req.TrybStudiow, req.StopienStudiow);
        }

        [HttpGet]
        public GrupaDTO[] Przegladaj([FromQuery] string fraza)
        {
            var lista = _grupaService.Przegladaj(fraza);
            return lista;
        }

        [HttpGet("filtruj/{tryb?}/{stopien?}/{semestr?}")]
        public GrupaDTO[] Filtruj(TrybStudiow? tryb = null, StopienStudiow? stopien = null, int? semestr = null)
        {
            var lista = _grupaService.Filtruj(tryb, stopien, semestr);
            return lista;
        }

        [HttpGet("{numer}")]
        public GrupaDTO Daj(string numer)
        {
            var grupa = _grupaService.Daj(numer);
            return grupa;
        }

        [HttpDelete("{numer}")]
        [Authorize]
        public void Usun(string numer)
        {
            _grupaService.Usun(numer);
        }
    }
}
