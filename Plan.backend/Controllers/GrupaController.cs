using System.Collections.Generic;
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

        [HttpGet]
        public IEnumerable<GrupaWidokDTO> Get()
        {
            var result = _grupaService.Przegladaj();
            return result;
        }

        [HttpPost]
        public void Post([FromBody] KomendaDodajGrupe req)
        {
            _grupaService.Dodaj(req.NrGrupy, req.Semestr, (TrybStudiow)req.TrybStudiow, (StopienStudiow)req.StopienStudiow);
        }

        [HttpDelete("{numer}")]
        public void Delete(string numer)
        {
            _grupaService.Usun(numer);
        }
    }
}
