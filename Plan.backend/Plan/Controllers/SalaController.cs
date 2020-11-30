using Microsoft.AspNetCore.Authorization;
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

        [HttpPost]
        [Authorize]
        public void Dodaj([FromBody] KomendaDodajSale req)
        {
            _salaService.Dodaj(req.Nazwa, req.Rodzaj);
        }

        [HttpGet]
        public SalaWidokDTO[] Przegladaj()
        {
            return _salaService.Przegladaj();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public void Usun(int id)
        {
            _salaService.Usun(id);
        }
    }
}
