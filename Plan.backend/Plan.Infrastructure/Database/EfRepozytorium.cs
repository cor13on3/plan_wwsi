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

        public T WybierzPierwszy(Expression<Func<T, bool>> kryteria)
        {
            return dbSet.FirstOrDefault(kryteria);
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
            T rekord = dbSet.Find(id);
            Usun(rekord);
        }

        public void Usun(T encja)
        {
            if (context.Entry(encja).State == EntityState.Detached)
                dbSet.Attach(encja);
            dbSet.Remove(encja);
        }

        public void Usun(Expression<Func<T, bool>> kryteria)
        {
            var rekord = dbSet.FirstOrDefault(kryteria);
            if (rekord == null)
                return;
            if (context.Entry(rekord).State == EntityState.Detached)
                dbSet.Attach(rekord);
            dbSet.Remove(rekord);
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
