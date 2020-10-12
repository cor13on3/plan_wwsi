using Plan.API.DTO;
using Plan.API.Komendy;
using Plan.Interfejsy;
using Plan.Serwis.BazaDanych;
using Plan.Serwis.BazaDanych.Encje;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Plan.Serwis.Implementacja
{
    public class KalendariumService : IKalendariumService
    {
        private PlanContext _planContext;

        public KalendariumService(PlanContext planContext)
        {
            _planContext = planContext;
        }

        public void DodajZjazdy(ZjazdDTO[] zjazdy)
        {
            foreach (var z in zjazdy)
            {
                var zajetaData = _planContext.Zjazd.Where(x => x.DataOd == z.DataOd && x.DataDo == z.DataDo);
                if (zajetaData.Count() > 0)
                    throw new Exception($"Istnieje już zjazd w terminie {z.DataOd:DD-MM-YYYY} - {z.DataDo:DD-MM-YYYY}");
                _planContext.Zjazd.Add(new Zjazd
                {
                    DataOd = z.DataOd,
                    DataDo = z.DataDo,
                    RodzajSemestru = (RodzajSemestru)z.RodzajSemestru
                });
            }
            _planContext.SaveChanges();
        }

        public IEnumerable<ZjazdWidokDTO> PrzegladajZjazdy(string nrGrupy)
        {
            var query = from gz in _planContext.GrupaZjazd
                        join z in _planContext.Zjazd
                        on gz.IdZjazdu equals z.IdZjazdu
                        where gz.NrGrupy == nrGrupy
                        select new ZjazdWidokDTO
                        {
                            Nr = gz.NrZjazdu,
                            DataOd = z.DataOd,
                            DataDo = z.DataDo,
                            CzyOdpracowanie = gz.CzyOdpracowanie
                        };
            return query.ToArray();
        }

        public IEnumerable<PropozycjaZjazduWidokDTO> PrzygotujZjazdy(DateTime poczatekSemestru, DateTime koniecSemestru, TrybStudiow trybStudiow)
        {
            var result = new List<PropozycjaZjazduWidokDTO>();
            var offset = trybStudiow == TrybStudiow.Stacjonarne ? 7 : 14;
            var dlugosc = trybStudiow == TrybStudiow.Stacjonarne ? 5 : 3;
            for (DateTime d = poczatekSemestru; d <= koniecSemestru; d = d.AddDays(offset))
            {
                result.Add(new PropozycjaZjazduWidokDTO
                {
                    DataOd = d,
                    DataDo = d.AddDays(dlugosc - 1)
                });
            }
            return result;
        }

        public void PrzyporzadkujZjazdyGrupie(string nrGrupy, ZjazdKolejnyDTO[] zjazdy)
        {
            var grupa = _planContext.Grupa.Find(nrGrupy);
            if (grupa == null)
                throw new Exception("Grupa nie istnieje");
            foreach (var z in zjazdy)
            {
                var istniejacyZjazd = _planContext.GrupaZjazd.Where(x => x.NrGrupy == nrGrupy && x.IdZjazdu == z.IdZjazdu);
                if (istniejacyZjazd.Count() > 0)
                    throw new Exception($"Grupa {nrGrupy} ma już przypisany zjazd o id: {z.IdZjazdu}");
                _planContext.GrupaZjazd.Add(new GrupaZjazd
                {
                    NrZjazdu = z.NrZjazdu,
                    NrGrupy = nrGrupy,
                    IdZjazdu = z.IdZjazdu,
                    CzyOdpracowanie = z.CzyOdpracowanie
                });
            }
            _planContext.SaveChanges();
        }
    }
}
