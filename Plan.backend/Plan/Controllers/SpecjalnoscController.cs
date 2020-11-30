using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Plan.Core.DTO;
using Plan.Core.IServices;

namespace Plan.API.Controllers
{
    [Route("api/specjalnosc")]
    [ApiController]
    public class SpecjalnoscController : ControllerBase
    {
        private ISpecjalnoscService _specjalnoscService;

        public SpecjalnoscController(ISpecjalnoscService specjalnoscService)
        {
            _specjalnoscService = specjalnoscService;
        }

        [HttpPost]
        [Authorize]
        public void Dodaj([FromBody] string nazwa)
        {
            _specjalnoscService.Dodaj(nazwa);
        }

        [HttpGet]
        public SpecjalnoscDTO[] Przegladaj()
        {
            return _specjalnoscService.Przegladaj();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public void Usun(int id)
        {
            _specjalnoscService.Usun(id);
        }
    }
}
