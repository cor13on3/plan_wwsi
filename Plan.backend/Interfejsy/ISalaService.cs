using Plan.API.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Plan.Interfejsy
{
    public interface ISalaService
    {
        void Dodaj(string nazwa, RodzajSali rodzajSali);
        IEnumerable<SalaWidokDTO> Przegladaj();
        void Usun(int id);
    }
}
