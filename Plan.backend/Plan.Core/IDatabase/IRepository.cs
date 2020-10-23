using System.Collections.Generic;

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
        IEnumerable<TDTO> Wybierz<TDTO>(ISpecification<T, TDTO> spec);
        bool Istnieje<TDTO>(ISpecification<T, TDTO> spec);
    }
}