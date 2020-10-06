using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test2.Entities;
using Test2.Models;
using Test2.Services;

namespace Test2.Controllers
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

        [HttpGet]
        public IEnumerable<ProponowanyZjazdDTO> Get(DateTime dataOd, DateTime dataDo, TrybStudiow tryb)
        {
            return _service.PrzygotujZjazdy(dataOd, dataDo, tryb);
        }

        [HttpPost]
        public void DodajZjazdy([FromBody] ZjazdDTO[] zjazdy)
        {
            _service.DodajZjazdy(zjazdy);
        }

        [HttpPost]
        public void UstalZjazdy([FromBody] string nrGrupy, [FromBody] KolejnyZjazdDTO[] zjazdy)
        {
            _service.PrzyporzadkujZjazdyGrupie(nrGrupy, zjazdy);
        }
    }
}
