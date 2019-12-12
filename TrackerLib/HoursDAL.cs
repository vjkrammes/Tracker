using System.Collections.Generic;
using System.Linq;

using TrackerLib.Entities;
using TrackerLib.Interfaces;

namespace TrackerLib
{
    public class HoursDAL : DAL<HoursEntity, Context>, IHoursDAL
    {
        public HoursDAL(Context context) : base(context) { }

        public void Delete(int id)
        {
            var entity = (from h in _dbset where h.Id == id select h).SingleOrDefault();
            if (entity != null)
            {
                _dbset.Remove(entity);
                _context.SaveChanges();
            }
        }

        public IEnumerable<HoursEntity> GetForClient(int cid) => Get(x => x.ClientId == cid, "Date", 'd');

        public HoursEntity Read(int id) => Get(x => x.Id == id).SingleOrDefault();

        public bool ClientHasHours(int cid) => GetForClient(cid).Any();

        public decimal TotalHours() => _dbset.Sum(x => x.Time);

        public decimal TotalHours(int cid) => _dbset.Where(x => x.ClientId == cid).Sum(x => x.Time);
    }
}
