using Plan.Core.DTO;
using System;

namespace Plan.Core.IServices
{
    public interface IWykladowcaService
    {
        void Dodaj(string tytul, string imie, string nazwisko, string email, int[] idSpecjalizacji);
        void Zmien(int id, string tytul, string imie, string nazwisko, string email, int[] idSpecjalizacji);
        void Usun(int id);
        WykladowcaWidokDTO[] Przegladaj(string fraza);
        WykladowcaDTO Daj(int id);
        LekcjaWidokDTO[] DajPlan(int id, DateTime data);
    }
}
