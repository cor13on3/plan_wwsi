using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Test2.Entities;
using Test2.Models;
using Test2.Services;

namespace Test2.Controllers
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
        public IEnumerable<SalaDTO> Przegladaj()
        {
            return _salaService.Przegladaj();
        }

        [HttpPost]
        public void Dodaj([FromBody] SalaDodajRequest req)
        {
            _salaService.Dodaj(req.Nazwa, req.Rodzaj);
        }

        [HttpDelete("{id}")]
        public void Usun(int id)
        {
            _salaService.Usun(id);
        }
    }

    public class SalaDodajRequest
    {
        public string Nazwa { get; set; }
        public RodzajSali Rodzaj { get; set; }
    }
}
