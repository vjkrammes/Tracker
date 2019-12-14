using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

using TrackerLib.Entities;
using TrackerLib.Interfaces;

namespace TrackerLib
{
    public class ClientTypeDAL : DAL<ClientTypeEntity, Context>, IClientTypeDAL
    {
        public ClientTypeDAL(Context context) : base(context) { }

        public void Delete(int id)
        {
            var entity = (from c in _dbset where c.Id == id select c).SingleOrDefault();
            if (entity != null)
            {
                _dbset.Remove(entity);
                _context.SaveChanges();
            }
        }

        public override IEnumerable<ClientTypeEntity> Get(
            Expression<Func<ClientTypeEntity, bool>> pred = null,
            string sort = null,
            char direction = 'a') => base.Get(pred, "Name", 'a');

        public ClientTypeEntity Read(int id) => Get(x => x.Id == id).SingleOrDefault();

        public ClientTypeEntity Read(string name) => Get(x => x.Name == name).SingleOrDefault();
    }
}
