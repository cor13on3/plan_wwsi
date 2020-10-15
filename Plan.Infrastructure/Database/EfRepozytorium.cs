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

        public virtual IQueryable<T> Przegladaj(
            Expression<Func<T, bool>> filtr = null,
            string zawiera = "")
        {
            IQueryable<T> query = dbSet;

            if (filtr != null)
            {
                query = query.Where(filtr);
            }

            foreach (var includeProperty in zawiera.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            return query;
        }

        public virtual T Daj(object id)
        {
            return dbSet.Find(id);
        }

        public virtual void Dodaj(T entity)
        {
            dbSet.Add(entity);
        }

        public virtual void Usun(object id)
        {
            T entityToDelete = dbSet.Find(id);
            Usun(entityToDelete);
        }

        public virtual void Usun(T entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }

        public virtual void Edytuj(T entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public void UsunWiele(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
                Usun(entity);
        }
    }
}
