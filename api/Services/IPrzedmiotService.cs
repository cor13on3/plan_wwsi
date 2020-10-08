using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test2.Models;

namespace Test2.Services
{
    public interface IPrzedmiotService
    {
        IEnumerable<PrzedmiotDTO> Przegladaj();
        void Dodaj(string nazwa);
        void Usun(int id);
    }
}
