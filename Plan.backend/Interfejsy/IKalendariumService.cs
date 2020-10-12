using Plan.API.DTO;
using Plan.API.Komendy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Plan.Interfejsy
{
    public interface IKalendariumService
    {
        IEnumerable<PropozycjaZjazduWidokDTO> PrzygotujZjazdy(DateTime poczatekSemestru, DateTime koniecSemestru, TrybStudiow trybStudiow);
        void DodajZjazdy(ZjazdDTO[] zjazdy);
        void PrzyporzadkujZjazdyGrupie(string nrGrupy, ZjazdKolejnyDTO[] zjazdy);

        IEnumerable<ZjazdWidokDTO> PrzegladajZjazdy(string nrGrupy);
    }
}
