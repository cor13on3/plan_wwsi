using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Test2.Models;
using Test2.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Test2.Controllers
{
    public class WykladowcaRequest
    {
        public string Tytul { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public string Email { get; set; }
        public int[] Specjalnosci { get; set; }
    }

    [Route("api/wykladowca")]
    [ApiController]
    public class WykladowcaController : ControllerBase
    {
        private IWykladowcaService _wykladowcaService;

        public WykladowcaController(IWykladowcaService wykladowcaService)
        {
            _wykladowcaService = wykladowcaService;
        }

        // GET: api/<WykladowcaController>
        [HttpGet]
        public IEnumerable<WykladowcaWidokDTO> Get()
        {
            var res = _wykladowcaService.DajWykladowcow();
            return res;
        }

        // GET api/<WykladowcaController>/5
        [HttpGet("{id}")]
        public WykladowcaDTO Get(int id)
        {
            return _wykladowcaService.Daj(id);
        }

        // POST api/<WykladowcaController>
        [HttpPost]
        public void Post([FromBody] WykladowcaRequest dto)
        {
            _wykladowcaService.DodajWykladowce(dto.Tytul, dto.Imie, dto.Nazwisko, dto.Email, dto.Specjalnosci);
        }

        // PUT api/<WykladowcaController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] WykladowcaRequest dto)
        {
            _wykladowcaService.ZmienWykladowce(id, dto.Tytul, dto.Imie, dto.Nazwisko, dto.Email, dto.Specjalnosci);
        }

        // DELETE api/<WykladowcaController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _wykladowcaService.UsunWykladowce(id);
        }
    }
}
