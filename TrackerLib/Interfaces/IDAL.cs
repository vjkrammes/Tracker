﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace TrackerLib.Interfaces
{
    public interface IDAL<T> where T : class, new()
    {
        int Count { get; }
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
        IEnumerable<T> Get(Expression<Func<T, bool>> pred = null, string sortby = null, char direction = 'a');
    }
}
