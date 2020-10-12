using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Plan.API.DTO;
using Plan.Interfejsy;

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

        [HttpGet]
        public IEnumerable<PrzedmiotWidokDTO> Przegladaj()
        {
            return _przedmiotService.Przegladaj();
        }

        [HttpPost]
        public void Dodaj([FromBody] string nazwa)
        {
            _przedmiotService.Dodaj(nazwa);
        }

        [HttpDelete("{id}")]
        public void Usun(int id)
        {
            _przedmiotService.Usun(id);
        }
    }
}
