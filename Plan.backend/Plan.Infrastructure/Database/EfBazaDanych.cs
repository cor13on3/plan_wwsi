using Microsoft.Extensions.Configuration;
using Plan.Core.IDatabase;
using Plan.Serwis.BazaDanych;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Plan.Infrastructure.DB
{
    public class EfBazaDanych : IBazaDanych, IDisposable
    {
        private PlanContext _db = null;
        private Dictionary<Type, object> _repositories = new Dictionary<Type, object>();

        public EfBazaDanych(IConfiguration configuration)
        {
            _db = new PlanContext(configuration);
        }

        public IRepozytorium<T> DajTabele<T>() where T : class
        {
            if (_repositories.Keys.Contains(typeof(T)) == true)
                return _repositories[typeof(T)] as IRepozytorium<T>;
            IRepozytorium<T> repo = new EfRepozytorium<T>(_db);
            _repositories.Add(typeof(T), repo);
            return repo;
        }

        public void Zapisz()
        {
            _db.SaveChanges();
        }


        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                    _db.Dispose();
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
