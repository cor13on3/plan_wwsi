using Plan.Core.DTO;
using Plan.Core.Entities;
using System;

namespace Plan.Core.IServices
{
    public class ZjazdKolejny
    {
        public int NrZjazdu { get; set; }
        public int IdZjazdu { get; set; }
        public bool CzyOdpracowanie { get; set; }
    }

    public interface IKalendariumService
    {
        void DodajZjazd(DateTime dataOd, DateTime dataDo, RodzajSemestru rodzajSemestru);
        void PrzyporzadkujGrupyDoZjazdu(ZjazdKolejny zjazd, string[] grupy);
        void UsunGrupyZZjazdu(string[] grupy, int nrKolejny);

        ZjazdWidokDTO[] PrzegladajZjazdyGrupy(string nrGrupy);
        ZjazdWidokDTO[] PrzegladajZjazdy();
    }
}
