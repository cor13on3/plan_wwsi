using Plan.Core.DTO;
using Plan.Core.Entities;
using Plan.Core.IDatabase;
using Plan.Core.IServices;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Plan.Core.Services
{
    public class SalaService : ISalaService
    {
        private IBazaDanych _baza;

        public SalaService(IBazaDanych baza)
        {
            _baza = baza;
        }

        public void Dodaj(string nazwa, RodzajSali rodzajSali)
        {
            _baza.Daj<Sala>().Dodaj(new Sala
            {
                Nazwa = nazwa,
                Rodzaj = rodzajSali
            });
            _baza.Zapisz();
        }

        public SalaWidokDTO[] Przegladaj()
        {
            return _baza.Daj<Sala>().Przegladaj()
                .Select(x => new SalaWidokDTO
                {
                    Id = x.IdSali,
                    Nazwa = x.Nazwa,
                    Rodzaj = (int)x.Rodzaj
                })
                .ToArray();
        }

        public void Usun(int id)
        {
            if (_baza.Daj<Sala>().Daj(id) == null)
                throw new Exception($"Sala o id {id} nie istnieje");
            _baza.Daj<Sala>().Usun(id);
            _baza.Zapisz();
        }
    }
}
