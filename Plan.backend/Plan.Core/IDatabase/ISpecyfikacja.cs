using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Plan.Core.IDatabase
{
    public interface ISpecification<T,TDTO>
    {
        Expression<Func<T, bool>> Kryteria { get; }
        List<Expression<Func<T, object>>> Skladowe { get; }
        List<string> SkladoweString { get; }
        Expression<Func<T, TDTO>> Mapowanie { get; }
    }
}
