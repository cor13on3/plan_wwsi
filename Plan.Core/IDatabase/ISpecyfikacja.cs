using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Plan.Core.IDatabase
{
    public interface ISpecification<T,TDTO>
    {
        Expression<Func<T, TDTO>> Mapowanie { get; }
        Expression<Func<T, bool>> Kryteria { get; }
        List<Expression<Func<T, object>>> Skladowe { get; }
        List<string> SkladoweString { get; }
    }
}
