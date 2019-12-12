using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Tracker.ECL.Interfaces
{
    public interface IECL<TEntity, TDTO>
    {
        int Count { get; }
        void Insert(TDTO dto);
        void Update(TDTO dto);
        void Delete(TDTO dto);
        IEnumerable<TDTO> Get(Expression<Func<TEntity, bool>> pred = null, string sort = null, char direction = 'a');
    }
}
