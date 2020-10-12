using Plan.API.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Plan.Interfejsy
{
    public interface IPrzedmiotService
    {
        IEnumerable<PrzedmiotWidokDTO> Przegladaj();
        void Dodaj(string nazwa);
        void Usun(int id);
    }
}
