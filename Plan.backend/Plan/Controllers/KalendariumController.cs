using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Plan.API.Komendy;
using Plan.Core.DTO;
using Plan.Core.Entities;
using Plan.Core.IServices;

namespace Plan.API.Controllers
{
    [Route("api/kalendarium")]
    [ApiController]
    public class KalendariumController : ControllerBase
    {
        private IKalendariumService _service;

        public KalendariumController(IKalendariumService service)
        {
            _service = service;
        }

        [HttpGet("daj-proponowane-zjazdy")]
        public PropozycjaZjazduWidokDTO[] DajProponowaneZjazdy(DateTime dataOd, DateTime dataDo, TrybStudiow tryb)
        {
            return _service.PrzygotujZjazdy(dataOd, dataDo, tryb);
        }

        [Authorize]
        [HttpPost("dodaj-zjazd")]
        public void DodajZjazdy([FromBody] ZjazdDTO[] zjazdy)
        {
            _service.DodajZjazdy(zjazdy);
        }

        [Authorize]
        [HttpPost("przyporzadkuj-zjazdy-grupie")]
        public void PrzyporzadkujZjazdyGrupie([FromBody] KomendaPrzypiszZjazdyGrupie req)
        {
            _service.PrzyporzadkujZjazdyGrupie(req.NrGrupy, req.Zjazdy);
        }

        [HttpGet("{nrGrupy}")]
        public ZjazdWidokDTO[] DajZjazdy(string nrGrupy)
        {
            return _service.PrzegladajZjazdy(nrGrupy);
        }
    }
}
