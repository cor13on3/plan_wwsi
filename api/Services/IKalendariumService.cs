using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test2.Entities;
using Test2.Models;

namespace Test2.Services
{
    public interface IKalendariumService
    {
        IEnumerable<ProponowanyZjazdDTO> PrzygotujZjazdy(DateTime poczatekSemestru, DateTime koniecSemestru, TrybStudiow trybStudiow);
        void DodajZjazdy(ZjazdDTO[] zjazdy);
        void PrzyporzadkujZjazdyGrupie(string nrGrupy, KolejnyZjazdDTO[] zjazdy);

        IEnumerable<ZjazdWidokDTO> PrzegladajZjazdy(string nrGrupy);
    }
}
