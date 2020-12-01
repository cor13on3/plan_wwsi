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
        Task Dodaj(string imie, string nazwisko, string email, string haslo);
        Task<DaneUzytkownikaDTO> Zaloguj(string email, string haslo);
        void Wyloguj();
    }
}
