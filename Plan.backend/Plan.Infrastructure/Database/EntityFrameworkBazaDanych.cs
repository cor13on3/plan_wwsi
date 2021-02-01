using Microsoft.Extensions.Configuration;
using Plan.Core.IDatabase;
using Plan.Serwis.BazaDanych;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Plan.Infrastructure.DB
{
    public class EntityFrameworkBazaDanych : IBazaDanych, IDisposable
    {
        private PlanContext _db = null;
        private Dictionary<Type, object> repozytoria = new Dictionary<Type, object>();

        public EntityFrameworkBazaDanych(IConfiguration configuration)
        {
            _db = new PlanContext(configuration);
        }

        public IRepozytorium<T> DajRepozytorium<T>() where T : class
        {
            Type typ = typeof(T);
            if (repozytoria.Keys.Contains(typ))
                return repozytoria[typ] as IRepozytorium<T>;
            IRepozytorium<T> repo = new EntityFrameworkAdapter<T>(_db);
            repozytoria.Add(typ, repo);
            return repo;
        }

        public void Zapisz()
        {
            _db.SaveChanges();
        }


        private bool _disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                    _db.Dispose();
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
