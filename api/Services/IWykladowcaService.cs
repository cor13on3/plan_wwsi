using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test2.Models;

namespace Test2.Services
{
    public interface IWykladowcaService
    {
        void DodajWykladowce(string tytul, string imie, string nazwisko, string email, int[] idSpecjalnosci);
        void ZmienWykladowce(int id, string tytul, string imie, string nazwisko, string email, int[] idSpecjalnosci);
        void UsunWykladowce(int id);
        IEnumerable<WykladowcaWidokDTO> DajWykladowcow(string fraza = null);
        WykladowcaDTO Daj(int id);
    }
}
