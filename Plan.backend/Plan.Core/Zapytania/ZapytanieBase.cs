using Plan.Core.IDatabase;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Plan.Core.Zapytania
{
    public abstract class ZapytanieBase<T, TMAP> : ISpecification<T, TMAP>
    {
        public ZapytanieBase(Expression<Func<T, bool>> kryteria)
        {
            Kryteria = kryteria;
        }
        public ZapytanieBase()
        {
        }

        public Expression<Func<T, bool>> Kryteria { get; private set; }
        public List<Expression<Func<T, object>>> Skladowe { get; private set; } = new List<Expression<Func<T, object>>>();
        public List<string> SkladoweString { get; private set; } = new List<string>();
        public Expression<Func<T, TMAP>> Mapowanie { get; private set; }

        protected virtual void UstawKryteria(Expression<Func<T, bool>> kryteria)
        {
            Kryteria = kryteria;
        }

        protected virtual void DodajSkladowa(Expression<Func<T, object>> expr)
        {
            Skladowe.Add(expr);
        }

        protected virtual void DodajSkladowa(string includeString)
        {
            SkladoweString.Add(includeString);
        }

        protected virtual void DodajMapowanie(Expression<Func<T, TMAP>> mapping)
        {
            Mapowanie = mapping;
        }
    }
}
