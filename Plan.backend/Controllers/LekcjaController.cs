using System;
using System.Linq;
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
        public void DodajLekcje([FromBody] KomendaDodajLekcje req)
        {
            _lekcjaService.Dodaj(req.IdPrzedmiotu, req.IdWykladowcy, req.IdSali, req.GodzinaOd, req.GodzinaDo, (FormaLekcji)req.Forma);
        }

        [HttpPost("grupy/przypisz")]
        public void PrzypiszGrupy([FromBody] KomendaPrzypiszGrupyLekcji req)
        {
            foreach (string nr in req.NrGrup)
                _lekcjaService.PrzypiszGrupe(req.IdLekcji, nr, req.NrZjazdu, req.DzienTygodnia, req.CzyOdpracowanie);
        }

        [HttpGet("plan/daj")]
        public LekcjaWidokDTO[] DajPlan(DateTime data, string grupa)
        {
            var result = _lekcjaService.DajPlan(data, grupa);
            return result.ToArray();
        }
    }
}
