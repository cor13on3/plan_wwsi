using Microsoft.EntityFrameworkCore;
using Plan.Core.IDatabase;
using Plan.Serwis.BazaDanych;
using System.Collections.Generic;
using System.Linq;

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

        public void Edytuj(T entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public void UsunWiele(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
                Usun(entity);
        }

        public bool Istnieje<TDTO>(ISpecification<T, TDTO> spec)
        {
            var queryableResultWithIncludes = spec.Skladowe
                                                    .Aggregate(dbSet.AsQueryable(),
            (current, include) => current.Include(include));

            var secondaryResult = spec.SkladoweString
                .Aggregate(queryableResultWithIncludes,
                    (current, include) => current.Include(include));

            return secondaryResult.Any(spec.Kryteria);
        }

        public IEnumerable<TDTO> Wybierz<TDTO>(ISpecification<T, TDTO> spec)
        {
            var queryableResultWithIncludes = spec.Skladowe
                                                    .Aggregate(dbSet.AsQueryable(),
            (current, include) => current.Include(include));

            // modify the IQueryable to include any string-based include statements
            var secondaryResult = spec.SkladoweString
                .Aggregate(queryableResultWithIncludes,
                    (current, include) => current.Include(include));

            // return the result of the query using the specification's criteria expression
            return secondaryResult
                            .Where(spec.Kryteria)
                            .Select(spec.Mapowanie)
                            .ToArray();
        }
    }
}
