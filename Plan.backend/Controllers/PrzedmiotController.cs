﻿using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Plan.Core.DTO;
using Plan.Core.IServices;

namespace Plan.API.Controllers
{
    [Route("api/przedmiot")]
    [ApiController]
    public class PrzedmiotController : ControllerBase
    {
        private IPrzedmiotService _przedmiotService;

        public PrzedmiotController(IPrzedmiotService przedmiotService)
        {
            _przedmiotService = przedmiotService;
        }

        [HttpGet]
        public IEnumerable<PrzedmiotWidokDTO> Przegladaj()
        {
            return _przedmiotService.Przegladaj();
        }

        // TODO: Zabezpieczyć endpointy administracyjne atrybutem [Authorize].
        [HttpPost]
        public void Dodaj([FromBody] string nazwa)
        {
            _przedmiotService.Dodaj(nazwa);
        }

        [HttpDelete("{id}")]
        public void Usun(int id)
        {
            _przedmiotService.Usun(id);
        }
    }
}