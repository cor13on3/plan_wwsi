using Plan.API.DTO;
using Plan.Interfejsy;
using Plan.Serwis.BazaDanych;
using Plan.Serwis.BazaDanych.Encje;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Plan.Serwis.Implementacja
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

        public IEnumerable<SalaWidokDTO> Przegladaj()
        {
            return _planContext.Sala.Select(x => new SalaWidokDTO
            {
                Id = x.IdSali,
                Nazwa = x.Nazwa,
                Rodzaj = (int)x.Rodzaj
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
