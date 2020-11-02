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
        public void DodajZjazd([FromBody] ZjazdDTO zjazd)
        {
            _service.DodajZjazd(zjazd);
        }

        [Authorize]
        [HttpPost("przyporzadkuj-zjazdy-grupie")]
        public void PrzyporzadkujZjazdyGrupie([FromBody] KomendaPrzypiszZjazdyGrupie req)
        {
            _service.PrzyporzadkujZjazdyGrupie(req.NrGrupy, req.Zjazdy);
        }

        [Authorize]
        [HttpPost("przyporzadkuj-grupy-do-zjazdu")]
        public void PrzyporzadkujGrupyDoZjazdu([FromBody] KomendaPrzypiszGrupyDoZjazdu req)
        {
            _service.PrzyporzadkujGrupyDoZjazdu(req.Zjazd, req.Grupy);
        }

        [Authorize]
        [HttpPost("usun-grupy-z-zjazdu")]
        public void UsunGrupyZZjazdu([FromBody] KomendaUsunGrupyZZjazdu req)
        {
            _service.UsunGrupyZZjazdu(req.Grupy, req.NrKolejny);
        }

        [HttpGet("{nrGrupy}")]
        public ZjazdWidokDTO[] DajZjazdyGrupy(string nrGrupy)
        {
            return _service.PrzegladajZjazdyGrupy(nrGrupy);
        }

        [HttpGet("zjazdy")]
        public ZjazdWidokDTO[] DajZjazdy()
        {
            return _service.PrzegladajZjazdy();
        }
    }
}
