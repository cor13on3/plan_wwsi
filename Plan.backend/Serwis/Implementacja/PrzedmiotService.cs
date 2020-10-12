using Plan.API.DTO;
using Plan.Interfejsy;
using Plan.Serwis.BazaDanych;
using Plan.Serwis.BazaDanych.Encje;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Plan.Serwis.Implementacja
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

        public IEnumerable<PrzedmiotWidokDTO> Przegladaj()
        {
            return _planContext.Przedmiot.Select(x => new PrzedmiotWidokDTO
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
