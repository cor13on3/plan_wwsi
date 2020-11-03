using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Plan.API.Komendy;
using Plan.Core.DTO;
using Plan.Core.IServices;

namespace Plan.API.Controllers
{
    [Route("api/lekcja")]
    [ApiController]
    public class LekcjaController : ControllerBase
    {
        private ILekcjaService _lekcjaService;

        public LekcjaController(ILekcjaService lekcjaService)
        {
            _lekcjaService = lekcjaService;
        }

        [Authorize]
        [HttpPost("dodaj")]
        public int DodajLekcje([FromBody] KomendaDodajLekcje req)
        {
            return _lekcjaService.Dodaj(req.IdPrzedmiotu, req.IdWykladowcy, req.IdSali, req.GodzinaOd, req.GodzinaDo, req.Forma);
        }

        [Authorize]
        [HttpPost("przypisz-grupe")]
        public void PrzypiszGrupe([FromBody] KomendaPrzypiszGrupeLekcji req)
        {
            _lekcjaService.PrzypiszGrupe(req.IdLekcji, req.NrGrupy, req.NrZjazdu, req.DzienTygodnia, req.CzyOdpracowanie);
        }

        [HttpGet("daj-plan")]
        public LekcjaWidokDTO[] DajPlanNaDzien(DateTime data, string grupa)
        {
            var wynik = _lekcjaService.DajPlanNaDzien(data, grupa);
            return wynik;
        }

        [HttpGet("daj-plan-na-tydzien/{grupa}")]
        public PlanDnia[] DajPlanNaTydzien(string grupa)
        {
            var wynik = _lekcjaService.DajPlanNaTydzien(grupa);
            return wynik;
        }
    }
}
