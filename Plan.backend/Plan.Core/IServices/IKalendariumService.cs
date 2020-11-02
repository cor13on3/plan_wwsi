using Plan.Core.DTO;
using Plan.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Plan.Core.IServices
{
    public interface IKalendariumService
    {
        PropozycjaZjazduWidokDTO[] PrzygotujZjazdy(DateTime poczatekSemestru, DateTime koniecSemestru, TrybStudiow trybStudiow);
        void DodajZjazd(ZjazdDTO zjazdy);
        void PrzyporzadkujZjazdyGrupie(string nrGrupy, ZjazdKolejnyDTO[] zjazdy);
        void PrzyporzadkujGrupyDoZjazdu(ZjazdKolejnyDTO zjazd, string[] grupy);
        void UsunGrupyZZjazdu(string[] grupy, int nrKolejny);

        ZjazdWidokDTO[] PrzegladajZjazdyGrupy(string nrGrupy);
        ZjazdWidokDTO[] PrzegladajZjazdy();
    }
}
