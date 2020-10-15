using System;
using System.Collections.Generic;
using System.Text;

namespace Plan.Core.IDatabase
{
    public interface IBazaDanych
    {
        IRepozytorium<T> Daj<T>() where T : class;
        void Zapisz();
    }
}
