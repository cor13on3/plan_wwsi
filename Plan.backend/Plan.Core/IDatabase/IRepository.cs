using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Plan.Core.IDatabase
{
    public interface IRepozytorium<T> where T : class
    {
        void Dodaj(T encja);
        T Znajdz(object id);
        T WybierzPierwszy(Expression<Func<T, bool>> kryteria);
        void Edytuj(T encja);
        void Usun(object id);
        void Usun(T encja);
        void Usun(Expression<Func<T, bool>> kryteria);
        void UsunWiele(IEnumerable<T> encje);
        IEnumerable<TDTO> Wybierz<TDTO>(IZapytanie<T, TDTO> zapytanie);
    }
}