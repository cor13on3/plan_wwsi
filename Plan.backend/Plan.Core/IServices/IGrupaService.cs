using Plan.Core.DTO;
using Plan.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Plan.Core.IServices
{
    public interface IGrupaService
    {
        void Dodaj(string numer, int semestr, TrybStudiow trybStudiow, StopienStudiow stopienStudiow);
        GrupaDTO[] Przegladaj(string fraza);
        GrupaDTO[] Filtruj(TrybStudiow? tryb, StopienStudiow? stopien, int? semestr);
        void Usun(string numer);
        GrupaDTO Daj(string numer);
    }
}
