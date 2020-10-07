using Test2.Entities;

namespace Test2.Models
{
    public class SalaDTO
    {
        public int Id { get; set; }
        public string Nazwa { get; set; }
        public RodzajSali Rodzaj { get; set; }
    }
}