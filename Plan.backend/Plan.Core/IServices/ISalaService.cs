using Plan.Core.DTO;
using Plan.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Plan.Core.IServices
{
    public interface ISalaService
    {
        void Dodaj(string nazwa, RodzajSali rodzajSali);
        SalaWidokDTO[] Przegladaj();
        void Usun(int id);
    }
}
