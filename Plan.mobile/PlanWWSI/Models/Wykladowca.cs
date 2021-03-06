using PlanWWSI.API;
using System.Linq;

namespace PlanWWSI.Models
{
    public class WykladowcaWidok
    {
        public int Id { get; set; }
        public string Nazwa { get; set; }
        public string Email { get; set; }
    }

    public class WykladowcaModel
    {
        public string Nazwa { get; set; }
        public string Email { get; set; }
        public string Specjalizacje { get; set; }

        public WykladowcaModel(WykladowcaDTO dto)
        {
            Nazwa = $"{dto.Tytul} {dto.Imie} {dto.Nazwisko}";
            Email = dto.Email;
            Specjalizacje = string.Join(", ", dto.Specjalizacje.Select(x => x.Nazwa).ToArray());
        }
    }
}