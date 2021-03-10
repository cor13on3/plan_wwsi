namespace PlanWWSI.Models
{
    public enum FormaLekcji
    {
        Wyklad,
        Cwiczenia
    }

    public class LekcjaWidok
    {
        public int IdLekcji { get; set; }
        public string Nazwa { get; set; }
        public string Wykladowca { get; set; }
        public FormaLekcji Forma { get; set; }
        public string Godziny { get; set; }
        public string Sala { get; set; }
        public bool JestSala { get; set; }
    }
}