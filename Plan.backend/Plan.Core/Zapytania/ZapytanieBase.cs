using Plan.Core.IDatabase;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Plan.Core.Zapytania
{
    public abstract class ZapytanieBase<Entity, Result> : IZapytanie<Entity, Result>
    {
        public Expression<Func<Entity, bool>> Kryteria { get; private set; }
        public List<Expression<Func<Entity, object>>> Skladowe { get; private set; } = new List<Expression<Func<Entity, object>>>();
        public List<string> SkladoweString { get; private set; } = new List<string>();
        public Expression<Func<Entity, Result>> Mapowanie { get; private set; }

        public ZapytanieBase()
        {
            Kryteria = x => true;
        }

        protected virtual void UstawKryteria(Expression<Func<Entity, bool>> kryteria)
        {
            Kryteria = kryteria;
        }

        protected virtual void DodajSkladowa(Expression<Func<Entity, object>> expr)
        {
            Skladowe.Add(expr);
        }

        protected virtual void DolaczEncje(string includeString)
        {
            SkladoweString.Add(includeString);
        }

        protected virtual void DodajMapowanie(Expression<Func<Entity, Result>> mapping)
        {
            Mapowanie = mapping;
        }
    }
}
