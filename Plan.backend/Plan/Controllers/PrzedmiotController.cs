using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Plan.Core.DTO;
using Plan.Core.IServices;

namespace Plan.API.Controllers
{
    [Route("api/przedmiot")]
    [ApiController]
    public class PrzedmiotController : ControllerBase
    {
        private IPrzedmiotService _przedmiotService;

        public PrzedmiotController(IPrzedmiotService przedmiotService)
        {
            _przedmiotService = przedmiotService;
        }

        [HttpPost]
        [Authorize]
        public void Dodaj([FromBody] string nazwa)
        {
            _przedmiotService.Dodaj(nazwa);
        }

        [HttpGet]
        public PrzedmiotWidokDTO[] Przegladaj()
        {
            return _przedmiotService.Przegladaj();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public void Usun(int id)
        {
            _przedmiotService.Usun(id);
        }
    }
}
