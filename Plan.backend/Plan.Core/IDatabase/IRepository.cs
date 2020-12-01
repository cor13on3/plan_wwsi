using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Plan.Core.IDatabase
{
    public interface IRepozytorium<T> where T : class
    {
        void Dodaj(T entity);
        T Znajdz(object id);
        void Edytuj(T entity);
        void Usun(object id);
        void Usun(T entity);
        void UsunWiele(IEnumerable<T> entities);
        void Usun(Expression<Func<T, bool>> match);
        IEnumerable<TDTO> Wybierz<TDTO>(IZapytanie<T, TDTO> spec);
    }
}