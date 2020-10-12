﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test2.Entities;
using Test2.Models;

namespace Test2.Services
{
    public interface ILekcjaService
    {
        void Dodaj(int przedmiotId, int wykladowcaId, int salaId, string godzinaOd, string godzinaDo, FormaLekcji forma);
        void Zmien(int lekcjaId, int przedmiotId, int wykladowcaId, int salaId, string godzinaOd, string godzinaDo, FormaLekcji forma);
        void PrzypiszGrupe(int lekcjaId, string nrGrupy, int nrZjazdu, int dzienTygodnia, bool czyOdpracowanie);
        void Usun(int lekcjaId);
        IEnumerable<LekcjaView> DajPlan(DateTime data, string nrGrupy);
    }
}
