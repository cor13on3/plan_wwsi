using Microsoft.EntityFrameworkCore;
using Plan.Core.IDatabase;
using Plan.Serwis.BazaDanych;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Plan.Infrastructure.DB
{
    public class EfRepozytorium<T> : IRepozytorium<T> where T : class
    {
        internal PlanContext context;
        internal DbSet<T> dbSet;

        public EfRepozytorium(PlanContext context)
        {
            this.context = context;
            this.dbSet = context.Set<T>();
        }

        public T Znajdz(object id)
        {
            return dbSet.Find(id);
        }

        public void Dodaj(T entity)
        {
            dbSet.Add(entity);
        }

        public void Edytuj(T entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public void Usun(object id)
        {
            T entityToDelete = dbSet.Find(id);
            Usun(entityToDelete);
        }

        public void Usun(T entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }

        public void Usun(Expression<Func<T, bool>> match)
        {
            var entityToDelete = dbSet.FirstOrDefault(match);
            if (entityToDelete == null)
                return;
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }

        public void UsunWiele(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
                Usun(entity);
        }

        public IEnumerable<TDTO> Wybierz<TDTO>(IZapytanie<T, TDTO> spec)
        {
            var aggregateQuery = spec.Skladowe.Aggregate(dbSet.AsQueryable(), (current, include) => current.Include(include));
            aggregateQuery = spec.SkladoweString.Aggregate(aggregateQuery, (current, include) => current.Include(include));

            var query = aggregateQuery.Where(spec.Kryteria).Select(spec.Mapowanie);
            return query.ToArray();
        }
    }
}
