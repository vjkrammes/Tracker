using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

using TrackerLib.Entities;
using TrackerLib.Interfaces;

namespace TrackerLib
{
    public class PhoneTypeDAL : DAL<PhoneTypeEntity, Context>, IPhoneTypeDAL
    {
        public PhoneTypeDAL(Context context) : base(context) { }

        public void Delete(int id)
        {
            var entity = (from p in _dbset where p.Id == id select p).SingleOrDefault();
            if (entity != null)
            {
                _dbset.Remove(entity);
                _context.SaveChanges();
            }
        }

        public override IEnumerable<PhoneTypeEntity> Get(Expression<Func<PhoneTypeEntity, bool>> pred = null,
            string sort = null,
            char direction = 'a') => base.Get(pred, "Name", 'a');

        public PhoneTypeEntity Read(int id) => Get(x => x.Id == id).SingleOrDefault();

        public PhoneTypeEntity Read(string name) => Get(x => x.Name == name).SingleOrDefault();
    }
}
