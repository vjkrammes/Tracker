using System.Collections.Generic;
using System.Linq;

using TrackerLib.Entities;
using TrackerLib.Interfaces;

namespace TrackerLib
{
    public class MileageDAL : DAL<MileageEntity, Context>, IMileageDAL
    {
        public MileageDAL(Context context) : base(context) { }

        public void Delete(int id)
        {
            var entity = (from m in _dbset where m.Id == id select m).SingleOrDefault();
            if (entity != null)
            {
                _dbset.Remove(entity);
                _context.SaveChanges();
            }
        }

        public IEnumerable<MileageEntity> GetForClient(int cid) => Get(x => x.ClientId == cid, "Date", 'd');

        public MileageEntity Read(int id) => Get(x => x.Id == id).SingleOrDefault();

        public bool ClientHasMileage(int cid) => GetForClient(cid).Any();

        public decimal TotalMiles() => _dbset.Sum(x => x.Miles);

        public decimal TotalMiles(int cid) => _dbset.Where(x => x.ClientId == cid).Sum(x => x.Miles);
    }
}
