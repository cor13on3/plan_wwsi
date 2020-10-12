using Plan.API.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Plan.Interfejsy
{
    public interface IGrupaService
    {
        void Dodaj(string numer, int semestr, TrybStudiow trybStudiow, StopienStudiow stopienStudiow);
        IEnumerable<GrupaWidokDTO> Przegladaj();
        void Usun(string numer);
    }
}
