using System;
using System.Collections.Generic;
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

        [HttpGet("zjazdy/przygotuj")]
        public IEnumerable<PropozycjaZjazduWidokDTO> PrzygotujZjazdy(DateTime dataOd, DateTime dataDo, int tryb)
        {
            return _service.PrzygotujZjazdy(dataOd, dataDo, (TrybStudiow)tryb);
        }

        [HttpPost("zjazdy/dodaj")]
        public void DodajZjazdy([FromBody] ZjazdDTO[] zjazdy)
        {
            _service.DodajZjazdy(zjazdy);
        }

        [HttpPost("zjazdy/przyporzadkuj")]
        public void PrzyporzadkujZjazdy([FromBody] KomendaPrzypiszZjazdyGrupie req)
        {
            _service.PrzyporzadkujZjazdyGrupie(req.NrGrupy, req.Zjazdy);
        }

        [HttpGet("{nrGrupy}")]
        public IEnumerable<ZjazdWidokDTO> PrzegladajZjazdy(string nrGrupy)
        {
            return _service.PrzegladajZjazdy(nrGrupy);
        }
    }
}
