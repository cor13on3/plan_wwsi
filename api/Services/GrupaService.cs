using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test2.Data;
using Test2.Entities;
using Test2.Models;

namespace Test2.Services
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
        }
    }
}
