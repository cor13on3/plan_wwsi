using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Plan.API.Komendy;
using Plan.Core.DTO;
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

        [HttpPost("dodaj-zjazd")]
        [Authorize]
        public void DodajZjazd([FromBody] KomendaDodajZjazd req)
        {
            _service.DodajZjazd(req.DataOd, req.DataDo, req.RodzajSemestru);
        }

        [HttpGet]
        public ZjazdWidokDTO[] PrzegladajZjazdy()
        {
            return _service.PrzegladajZjazdy();
        }

        [HttpGet("{nrGrupy}")]
        public ZjazdWidokDTO[] DajZjazdyGrupy(string nrGrupy)
        {
            return _service.PrzegladajZjazdyGrupy(nrGrupy);
        }

        [HttpPost("przyporzadkuj-grupy-do-zjazdu")]
        [Authorize]
        public void PrzyporzadkujGrupyDoZjazdu([FromBody] KomendaPrzypiszGrupyDoZjazdu req)
        {
            _service.PrzyporzadkujGrupyDoZjazdu(req.Zjazd, req.Grupy);
        }

        [HttpPost("usun-grupy-z-zjazdu")]
        [Authorize]
        public void UsunGrupyZZjazdu([FromBody] KomendaUsunGrupyZZjazdu req)
        {
            _service.UsunGrupyZZjazdu(req.Grupy, req.NrKolejny);
        }
    }
}
