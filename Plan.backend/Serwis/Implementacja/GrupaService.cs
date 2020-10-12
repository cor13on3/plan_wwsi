using Plan.API.DTO;
using Plan.Interfejsy;
using Plan.Serwis.BazaDanych;
using Plan.Serwis.BazaDanych.Encje;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Plan.Serwis.Implementacja
{
    public class GrupaService : IGrupaService
    {
        private PlanContext _planContext;

        public GrupaService(PlanContext planContext)
        {
            _planContext = planContext;
        }

        public void Dodaj(string numer, int semestr, TrybStudiow trybStudiow, StopienStudiow stopienStudiow)
        {
            if (string.IsNullOrEmpty(numer) || semestr < 0)
                throw new Exception("Uzupełnij dane");
            if (_planContext.Grupa.Find(numer) != null)
                throw new Exception($"Istnieje już grupa o numerze {numer}");
            _planContext.Grupa.Add(new Grupa
            {
                NrGrupy = numer,
                Semestr = semestr,
                TrybStudiow = trybStudiow,
                StopienStudiow = stopienStudiow
            });
            _planContext.SaveChanges();
        }

        public IEnumerable<GrupaWidokDTO> Przegladaj()
        {
            var query = _planContext.Grupa.Select(x => new GrupaWidokDTO
            {
                Numer = x.NrGrupy
            });
            return query.ToArray();
        }

        public void Usun(string numer)
        {
            var res = _planContext.Grupa.Find(numer);
            if (res == null)
                throw new Exception($"Grupa o numerze {numer} nie istnieje.");
            _planContext.Grupa.Remove(res);
            _planContext.SaveChanges();
        }
    }
}
