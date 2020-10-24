using Microsoft.AspNetCore.Identity;
using Plan.Core.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Plan.Core.IServices
{
    public interface IUzytkownikService
    {
        void Dodaj(string imie, string nazwisko, string email, string haslo);
        DaneUzytkownikaDTO Zaloguj(string email, string haslo);
        void Wyloguj();
    }
}
