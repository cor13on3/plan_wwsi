using PlanWWSI.Models;
using System;
using System.Linq;

namespace PlanWWSI.ViewModels
{
    public class ZajeciaSzczegolyViewModel : BaseViewModel
    {
        public ZajeciaSzczegoly Szczegoly { get; set; }

        public Zajecia[] ListaZajec = new Zajecia[0];

        public ZajeciaSzczegolyViewModel(Zajecia item = null)
        {
            Title = item?.Nazwa;

            Szczegoly = new ZajeciaSzczegoly
            {
                Id = item.Id,
                Nazwa = item.Nazwa,
                Godzina = item.Godzina,
                Wykladowca = "Jan Kowalski",
                Sala = "123",
                Alternatywy = new Zajecia[]
                {
                    new Zajecia{ Nazwa = "Sztuczna Inteligencja", Godzina = item.Godzina},
                    new Zajecia{ Nazwa = "Grafika", Godzina = item.Godzina},
                }
            };
        }

        public Zajecia[] DajProponowaneZajecia(string godzina)
        {
            if (string.IsNullOrEmpty(godzina))
                throw new InvalidOperationException("Nie podano godziny.");
            var res = ListaZajec.Where(x => x.Godzina == godzina).ToArray();
            return res;
        }
    }
}
