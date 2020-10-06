using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Test2.Entities;
using Test2.Models;
using Test2.Services;

namespace Test2.Controllers
{
    public class GrupaRequest
    {
        public string NrGrupy { get; set; }
        public int Semestr { get; set; }
        public TrybStudiow TrybStudiow { get; set; }
        public StopienStudiow StopienStudiow { get; set; }
    }

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
        public void Post([FromBody] GrupaRequest grupa)
        {
            _grupaService.Dodaj(grupa.NrGrupy, grupa.Semestr, grupa.TrybStudiow, grupa.StopienStudiow);
        }

        [HttpDelete("{numer}")]
        public void Delete(string numer)
        {
            _grupaService.Usun(numer);
        }
    }
}
