using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test2.Data;
using Test2.Entities;

namespace Test2.Services
{
    public interface IRepozytorium<E> 
        where E : Entity
    {
        void Dodaj(E entity);
        IEnumerable<E> Przegladaj();
        void Zmien(int id, E entity);
        void Usun(int id);
    }

    public class Repozytorium<E> : IRepozytorium<E>
        where E : Entity
    {
        private PlanContext _planContext;
        private DbSet<E> _table;

        public Repozytorium(PlanContext planContext)
        {
            _planContext = planContext;
            _table = _planContext.Set<E>();
        }

        public void Dodaj(E entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<E> Przegladaj()
        {
            return _table.ToArray();
        }

        public void Usun(int id)
        {
            throw new NotImplementedException();
        }

        public void Zmien(int id, E entity)
        {
            throw new NotImplementedException();
        }
    }
}
