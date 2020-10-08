using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Test2.Entities;
using Test2.Services;

namespace Test2.Controllers
{
    public class LekcjaDodajRequest
    {
        public int IdPrzedmiotu { get; set; }
        public int IdWykladowcy { get; set; }
        public int IdSali { get; set; }
        public string GodzinaOd { get; set; }
        public string GodzinaDo { get; set; }
        public FormaLekcji Forma { get; set; }
    }

    public class LekcjaPrzypiszGrupyRequest
    {
        public int IdLekcji { get; set; }
        public string[] NrGrup { get; set; }
        public int NrZjazdu { get; set; }
        public int DzienTygodnia { get; set; }
        public bool CzyOdpracowanie { get; set; }
    }

    [Route("api/lekcja")]
    [ApiController]
    public class LekcjaController : ControllerBase
    {
        private ILekcjaService _lekcjaService;

        public LekcjaController(ILekcjaService lekcjaService)
        {
            _lekcjaService = lekcjaService;
        }

        [HttpPost("dodaj")]
        public void DodajLekcje([FromBody] LekcjaDodajRequest req)
        {
            _lekcjaService.Dodaj(req.IdPrzedmiotu, req.IdWykladowcy, req.IdSali, req.GodzinaOd, req.GodzinaDo, req.Forma);
        }

        [HttpPost("grupy/przypisz")]
        public void PrzypiszGrupy([FromBody] LekcjaPrzypiszGrupyRequest req)
        {
            foreach (string nr in req.NrGrup)
                _lekcjaService.PrzypiszGrupe(req.IdLekcji, nr, req.NrZjazdu, req.DzienTygodnia, req.CzyOdpracowanie);
            // może dopiero teraz save changes?
        }
    }
}
