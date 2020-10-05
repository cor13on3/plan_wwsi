using System.Collections.Generic;

namespace Test2.Entities
{
    public class Specjalnosc
    {
        public int IdSpecjalnosci { get; set; }
        public string Nazwa { get; set; }

        public List<WyklSpec> WyklSpecList { get; set; }
    }
}
