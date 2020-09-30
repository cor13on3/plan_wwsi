using System;
using System.Collections.Generic;
using Test2.Models;

namespace Test2.Services
{
    public interface IPlanService
    {
        void DodajPrzedmiot(string nazwa, FormaPrzedmiotu forma);
        void ZmienPrzedmiot(int id, string nazwa, FormaPrzedmiotu forma);
        void UsunPrzedmiot(int id);
        IEnumerable<Przedmiot> DajPrzedmioty();

        void DodajWykladowce(string tytul, string imie, string nazwisko, string email, int idSpecjalnosci);
        void ZmienWykladowce(int id, string tytul, string imie, string nazwisko, string email, int idSpecjalnosci);
        void UsunWykladowce(int id);
        IEnumerable<Wykladowca> DajWykladowcow(string fraza = null);

        void DodajSpecjalnosc(string nazwa);
        void ZmienSpecjalnosc(int id, string nazwa);
        void UsunSpecjalnosc(int id);
        IEnumerable<Specjalnosc> DajSpecjalnosci(string fraza = null);

        void DodajSale(string nazwa, RodzajSali rodzaj);
        void ZmienSale(int id, string nazwa, RodzajSali rodzaj);
        void UsunSale(int id);
        IEnumerable<Sala> DajSale();

        void DodajZjazd(DateTime dataOd, DateTime dataDo, RodzajSemestru semestr);
        void ZmienZjazd(int id, DateTime dataOd, DateTime dataDo, RodzajSemestru semestr);
        void UsunZjazd(int id);
        IEnumerable<Zjazd> DajZjazdy();

        void DodajGrupe(string numer, short semestr, TrybStudiow tryb, StopienStudiow stopien);
        void ZmienGrupe(string numer, short semestr, TrybStudiow tryb, StopienStudiow stopien);
        void UsunGrupe(string numer);
        IEnumerable<Grupa> DajGrupy();

        void PrzyporzadkujZjazd(short nrZjazdu, string nrGrupy, int idZjazdu);

        void DodajLekcje(int idPrzedmiotu, int idWykladowcy, int idSali, TimeSpan godzinaOd, TimeSpan godzinaDo, bool czyOdpracowanie);
        void ZmienLekcje(int idLekcji, int idPrzedmiotu, int idWykladowcy, int idSali, TimeSpan godzinaOd, TimeSpan godzinaDo, bool czyOdpracowanie);
        void UsunLekcje(int idLekcji);
        IEnumerable<Lekcja> DajLekcje();

        void PrzyporzadkujLekcje(string nrGrupy, int idLekcji);
    }
}
