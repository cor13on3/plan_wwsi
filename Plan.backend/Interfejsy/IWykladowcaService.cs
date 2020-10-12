using Plan.API.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Plan.Interfejsy
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
