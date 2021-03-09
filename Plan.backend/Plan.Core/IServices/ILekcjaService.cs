using Plan.Core.DTO;
using Plan.Core.Entities;
using System;

namespace Plan.Core.IServices
{
    public interface ILekcjaService
    {
        int Dodaj(int przedmiotId, int wykladowcaId, int salaId, int dzienTygodnia, string godzinaOd, string godzinaDo, FormaLekcji forma);
        void PrzypiszGrupe(int lekcjaId, string nrGrupy, int nrZjazdu, bool czyOdpracowanie);
        void WypiszGrupe(int idLekcji, string nrGrupy, int nrZjazdu, bool czyOdpracowanie);
        void Usun(int lekcjaId);
        LekcjaWidokDTO[] DajPlanGrupyNaDzien(DateTime data, string nrGrupy);
        PlanDnia[] DajPlanGrupyNaTydzien(string nrGrupy);
        PlanDnia[] DajPlanOdpracowania(string nrGrupy, int nrZjazdu);
        LekcjaDTO[] DajLekcjeNaDzienTygodnia(TrybStudiow trybStudiow, int semestr, int dzienTygodnia);
    }
}
