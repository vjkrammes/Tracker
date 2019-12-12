using System.Collections.Generic;
using System.Linq;

using TrackerLib.Entities;
using TrackerLib.Interfaces;

namespace TrackerLib
{
    public class PhoneDAL : DAL<PhoneEntity, Context>, IPhoneDAL
    {
        public PhoneDAL(Context context) : base(context) { }

        public void Delete(int id)
        {
            var entity = (from p in _dbset where p.Id == id select p).SingleOrDefault();
            if (entity != null)
            {
                _dbset.Remove(entity);
                _context.SaveChanges();
            }
        }

        public IEnumerable<PhoneEntity> GetForClient(int cid) => Get(x => x.ClientId == cid, "Number", 'a');

        public IEnumerable<PhoneEntity> GetForPhoneType(int ptid) => Get(x => x.PhoneTypeId == ptid, "Number", 'a');

        public PhoneEntity Read(int id) => Get(x => x.Id == id).SingleOrDefault();

        public bool ClientHasPhones(int cid) => GetForClient(cid).Any();

        public bool PhoneTypeHasPhones(int ptid) => GetForPhoneType(ptid).Any();
    }
}
