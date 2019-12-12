using System.Collections.Generic;
using System.Linq;

using TrackerLib.Entities;
using TrackerLib.Interfaces;

namespace TrackerLib
{
    public class NoteDAL : DAL<NoteEntity, Context>, INoteDAL
    {
        public NoteDAL(Context context) : base(context) { }

        public void Delete(int id)
        {
            var entity = (from n in _dbset where n.Id == id select n).SingleOrDefault();
            if (entity != null)
            {
                _dbset.Remove(entity);
                _context.SaveChanges();
            }
        }

        public IEnumerable<NoteEntity> GetForClient(int cid) => Get(x => x.ClientId == cid, "Date", 'd');

        public NoteEntity Read(int id) => Get(x => x.Id == id).SingleOrDefault();

        public bool ClientHasNotes(int cid) => GetForClient(cid).Any();
    }
}
