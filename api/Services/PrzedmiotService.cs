using System;
using System.Collections.Generic;
using System.Linq;
using Test2.Data;
using Test2.Entities;
using Test2.Models;

namespace Test2.Services
{
    public class PrzedmiotService : IPrzedmiotService
    {
        private PlanContext _planContext;

        public PrzedmiotService(PlanContext planContext)
        {
            _planContext = planContext;
        }

        public void Dodaj(string nazwa)
        {
            _planContext.Przedmiot.Add(new Przedmiot
            {
                Nazwa = nazwa,
            });
            _planContext.SaveChanges();
        }

        public IEnumerable<PrzedmiotDTO> Przegladaj()
        {
            return _planContext.Przedmiot.Select(x => new PrzedmiotDTO
            {
                Id = x.IdPrzedmiotu,
                Nazwa = x.Nazwa,
            });
        }

        public void Usun(int id)
        {
            var przedmiot = _planContext.Przedmiot.Find(id);
            if (przedmiot == null)
                throw new Exception($"Przedmiot o id {id} nie istnieje");
            _planContext.Przedmiot.Remove(przedmiot);
            _planContext.SaveChanges();
        }
    }
}
