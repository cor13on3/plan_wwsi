using Plan.Core.DTO;
using Plan.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Plan.Core.IServices
{
    public interface ILekcjaService
    {
        int Dodaj(int przedmiotId, int wykladowcaId, int salaId, string godzinaOd, string godzinaDo, FormaLekcji forma);
        void Zmien(int lekcjaId, int przedmiotId, int wykladowcaId, int salaId, string godzinaOd, string godzinaDo, FormaLekcji forma);
        void PrzypiszGrupe(int lekcjaId, string nrGrupy, int nrZjazdu, int dzienTygodnia, bool czyOdpracowanie);
        void Usun(int lekcjaId);
        LekcjaWidokDTO[] DajPlanNaDzien(DateTime data, string nrGrupy);
        PlanDnia[] DajPlanNaTydzien(string nrGrupy);
        PlanDnia[] DajPlanOdpracowania(string nrGrupy, int nrZjazdu);
    }
}
