using Plan.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Plan.Core.IServices
{
    public interface IWykladowcaService
    {
        void Dodaj(string tytul, string imie, string nazwisko, string email, int[] idSpecjalnosci);
        void Zmien(int id, string tytul, string imie, string nazwisko, string email, int[] idSpecjalnosci);
        void Usun(int id);
        WykladowcaWidokDTO[] Przegladaj(string fraza = null);
        WykladowcaDTO Daj(int id);
        LekcjaWidokDTO[] DajPlan(int id, DateTime data);
    }
}
