using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test2.Entities;
using Test2.Models;

namespace Test2.Services
{
    public interface IGrupaService
    {
        void Dodaj(string numer, int semestr, TrybStudiow trybStudiow, StopienStudiow stopienStudiow);
        IEnumerable<GrupaWidokDTO> Przegladaj();
        void Usun(string numer);
    }
}
