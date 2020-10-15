using Plan.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Plan.Core.IServices
{
    public interface IWykladowcaService
    {
        void DodajWykladowce(string tytul, string imie, string nazwisko, string email, int[] idSpecjalnosci);
        void ZmienWykladowce(int id, string tytul, string imie, string nazwisko, string email, int[] idSpecjalnosci);
        void UsunWykladowce(int id);
        WykladowcaWidokDTO[] DajWykladowcow(string fraza = null);
        WykladowcaDTO Daj(int id);
    }
}
