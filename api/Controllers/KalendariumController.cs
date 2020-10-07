using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Test2.Entities;
using Test2.Models;
using Test2.Services;

namespace Test2.Controllers
{
    public class UstalZjazdyRequest
    {
        public string NrGrupy { get; set; }
        public KolejnyZjazdDTO[] Zjazdy { get; set; }
    }

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
        public IEnumerable<ProponowanyZjazdDTO> PrzygotujZjazdy(DateTime dataOd, DateTime dataDo, TrybStudiow tryb)
        {
            return _service.PrzygotujZjazdy(dataOd, dataDo, tryb);
        }

        [HttpPost("zjazdy/dodaj")]
        public void DodajZjazdy([FromBody] ZjazdDTO[] zjazdy)
        {
            _service.DodajZjazdy(zjazdy);
        }

        [HttpPost("zjazdy/przyporzadkuj")]
        public void PrzyporzadkujZjazdy([FromBody] UstalZjazdyRequest req)
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
