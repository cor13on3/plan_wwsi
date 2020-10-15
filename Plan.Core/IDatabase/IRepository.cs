using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Plan.Core.IDatabase
{
    public interface IRepozytorium<T> where T : class
    {
        void Dodaj(T entity);
        T Daj(object id);
        void Edytuj(T entity);
        void Usun(object id);
        void Usun(T entity);
        void UsunWiele(IEnumerable<T> entities);
        IQueryable<T> Przegladaj(Expression<Func<T, bool>> filtr = null, string zawiera = "");
    }
}