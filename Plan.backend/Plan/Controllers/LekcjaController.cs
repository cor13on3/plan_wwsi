using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Plan.API.Komendy;
using Plan.Core.DTO;
using Plan.Core.Entities;
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

        [HttpPost("dodaj")]
        [Authorize]
        public int DodajLekcje([FromBody] KomendaDodajLekcje req)
        {
            return _lekcjaService.Dodaj(req.IdPrzedmiotu, req.IdWykladowcy, req.IdSali, req.GodzinaOd, req.GodzinaDo, req.Forma);
        }

        [HttpPost("przypisz-grupe")]
        [Authorize]
        public void PrzypiszGrupe([FromBody] KomendaPrzypiszGrupeLekcji req)
        {
            _lekcjaService.PrzypiszGrupe(req.IdLekcji, req.NrGrupy, req.NrZjazdu, req.DzienTygodnia, req.CzyOdpracowanie);
        }

        [HttpGet("daj-plan/{data}/{grupa}")]
        public LekcjaWidokDTO[] DajPlanNaDzien(DateTime data, string grupa)
        {
            var wynik = _lekcjaService.DajPlanGrupyNaDzien(data, grupa);
            return wynik;
        }

        [HttpGet("daj-plan-na-tydzien/{grupa}")]
        public PlanDnia[] DajPlanNaTydzien(string grupa)
        {
            var wynik = _lekcjaService.DajPlanGrupyNaTydzien(grupa);
            return wynik;
        }

        [HttpGet("daj-plan-odpracowania/{grupa}/{nrZjazdu}")]
        public PlanDnia[] DajPlanOdpracowania(string grupa, int nrZjazdu)
        {
            var wynik = _lekcjaService.DajPlanOdpracowania(grupa, nrZjazdu);
            return wynik;
        }

        [HttpGet("daj-lekcje-dzien-tyg/{tryb}/{semestr}/{dzienTygodnia}")]
        public LekcjaDTO[] DajLekcjeNaDzienTygodnia(TrybStudiow tryb, int semestr, int dzienTygodnia)
        {
            var wynik = _lekcjaService.DajLekcjeNaDzienTygodnia(tryb, semestr, dzienTygodnia);
            return wynik;
        }
    }
}
