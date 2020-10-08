using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Test2.Models;
using Test2.Services;

namespace Test2.Controllers
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
        public IEnumerable<PrzedmiotDTO> Przegladaj()
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
