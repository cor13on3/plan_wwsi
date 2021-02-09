using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Plan.Core.DTO;
using Plan.Core.IServices;

namespace Plan.API.Controllers
{
    [Route("api/specjalizacja")]
    [ApiController]
    public class SpecjalizacjaController : ControllerBase
    {
        private ISpecjalizacjaService _specService;

        public SpecjalizacjaController(ISpecjalizacjaService specService)
        {
            _specService = specService;
        }

        [HttpPost]
        [Authorize]
        public void Dodaj([FromBody] string nazwa)
        {
            _specService.Dodaj(nazwa);
        }

        [HttpGet]
        public SpecjalizacjaDTO[] Przegladaj()
        {
            return _specService.Przegladaj();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public void Usun(int id)
        {
            _specService.Usun(id);
        }
    }
}
