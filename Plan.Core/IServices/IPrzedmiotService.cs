using Plan.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Plan.Core.IServices
{
    public interface IPrzedmiotService
    {
        PrzedmiotWidokDTO[] Przegladaj();
        void Dodaj(string nazwa);
        void Usun(int id);
    }
}
