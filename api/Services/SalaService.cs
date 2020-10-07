using System;
using System.Collections.Generic;
using System.Linq;
using Test2.Data;
using Test2.Entities;
using Test2.Models;

namespace Test2.Services
{
    public class SalaService : ISalaService
    {
        private PlanContext _planContext;

        public SalaService(PlanContext planContext)
        {
            _planContext = planContext;
        }

        public void Dodaj(string nazwa, RodzajSali rodzajSali)
        {
            _planContext.Sala.Add(new Sala
            {
                Nazwa = nazwa,
                Rodzaj = rodzajSali
            });
            _planContext.SaveChanges();
        }

        public IEnumerable<SalaDTO> Przegladaj()
        {
            return _planContext.Sala.Select(x => new SalaDTO
            {
                Id = x.IdSali,
                Nazwa = x.Nazwa,
                Rodzaj = x.Rodzaj
            });
        }

        public void Usun(int id)
        {
            var sala = _planContext.Sala.Find(id);
            if (sala == null)
                throw new Exception($"Sala o id {id} nie istnieje");
            _planContext.Sala.Remove(sala);
            _planContext.SaveChanges();
        }
    }
}
