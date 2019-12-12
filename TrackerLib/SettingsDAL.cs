using Microsoft.EntityFrameworkCore;

using TrackerLib.Entities;
using TrackerLib.Interfaces;

namespace TrackerLib
{
    public class SettingsDAL : ISettingsDAL
    {
        private readonly Context _context;

        public SettingsDAL(Context context) => _context = context;

        public void Update(SettingsEntity entity)
        {
            _context.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
