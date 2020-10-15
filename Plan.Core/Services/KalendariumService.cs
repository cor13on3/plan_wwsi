using Plan.Core.DTO;
using Plan.Core.Entities;
using Plan.Core.IDatabase;
using Plan.Core.IServices;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Plan.Core.Services
{
    public class KalendariumService : IKalendariumService
    {
        private IBazaDanych _baza;

        public KalendariumService(IBazaDanych baza)
        {
            _baza = baza;
        }

        public void DodajZjazdy(ZjazdDTO[] zjazdy)
        {
            foreach (var z in zjazdy)
            {
                var zajetaData = _baza.Daj<Zjazd>().Przegladaj(x => x.DataOd == z.DataOd && x.DataDo == z.DataDo, "");
                if (zajetaData.Count() > 0)
                    throw new Exception($"Istnieje już zjazd w terminie {z.DataOd:DD-MM-YYYY} - {z.DataDo:DD-MM-YYYY}");
                _baza.Daj<Zjazd>().Dodaj(new Zjazd
                {
                    DataOd = z.DataOd,
                    DataDo = z.DataDo,
                    RodzajSemestru = (RodzajSemestru)z.RodzajSemestru
                });
            }

            _baza.Zapisz();
        }

        public IEnumerable<ZjazdWidokDTO> PrzegladajZjazdy(string nrGrupy)
        {
            var query = _baza.Daj<GrupaZjazd>().Przegladaj(x => x.NrGrupy == nrGrupy, "Zjazd");
            return query.Select(x => new ZjazdWidokDTO
            {
                Nr = x.NrZjazdu,
                DataOd = x.Zjazd.DataOd,
                DataDo = x.Zjazd.DataDo,
                CzyOdpracowanie = x.CzyOdpracowanie
            }).ToArray();
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
            var grupa = _baza.Daj<Grupa>().Przegladaj(x => x.NrGrupy == nrGrupy, "").FirstOrDefault();
            if (grupa == null)
                throw new Exception("Grupa nie istnieje");
            foreach (var z in zjazdy)
            {
                var istniejacyZjazd = _baza.Daj<GrupaZjazd>().Przegladaj(x => x.NrGrupy == nrGrupy && x.IdZjazdu == z.IdZjazdu, "");
                if (istniejacyZjazd.Count() > 0)
                    throw new Exception($"Grupa {nrGrupy} ma już przypisany zjazd o id: {z.IdZjazdu}");
                _baza.Daj<GrupaZjazd>().Dodaj(new GrupaZjazd
                {
                    NrZjazdu = z.NrZjazdu,
                    NrGrupy = nrGrupy,
                    IdZjazdu = z.IdZjazdu,
                    CzyOdpracowanie = z.CzyOdpracowanie
                });
            }
            _baza.Zapisz();
        }
    }
}
