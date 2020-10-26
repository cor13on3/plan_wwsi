using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Plan.API.Komendy;
using Plan.Core.DTO;
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
        public GrupaWidokDTO[] Get()
        {
            var wynik = _grupaService.Przegladaj();
            return wynik;
        }

        [Authorize]
        [HttpPost]
        public void Post([FromBody] KomendaDodajGrupe req)
        {
            _grupaService.Dodaj(req.NrGrupy, req.Semestr, req.TrybStudiow, req.StopienStudiow);
        }

        [Authorize]
        [HttpDelete("{numer}")]
        public void Delete(string numer)
        {
            _grupaService.Usun(numer);
        }
    }
}
