using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test2.Entities;
using Test2.Models;

namespace Test2.Services
{
    public interface ISalaService
    {
        void Dodaj(string nazwa, RodzajSali rodzajSali);
        IEnumerable<SalaDTO> Przegladaj();
        void Usun(int id);
    }
}
