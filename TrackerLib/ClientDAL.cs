using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using TrackerCommon;

using TrackerLib.Entities;
using TrackerLib.Interfaces;

namespace TrackerLib
{
    public class ClientDAL : DAL<ClientEntity, Context>, IClientDAL
    {
        public ClientDAL(Context context) : base(context) { }

        public void Delete(int id)
        {
            var entity = (from c in _dbset where c.Id == id select c).SingleOrDefault();
            if (entity != null)
            {
                _dbset.Remove(entity);
                _context.SaveChanges();
            }
        }

        public override IEnumerable<ClientEntity> Get(
            Expression<Func<ClientEntity, bool>> pred = null,
            string sort = null,
            char direction = 'a')
        {
            return pred switch
            {
                null => _dbset
                    .Include(x => x.ClientType)
                    .Include(x => x.Phones)
                    .Include(x => x.Notes)
                    .OrderBy(x => x.Name)
                    .AsNoTracking()
                    .ToList(),
                _ => _dbset
                    .Include(x => x.ClientType)
                    .Include(x => x.Phones)
                    .Include(x => x.Notes)
                    .Where(pred)
                    .OrderBy(x => x.Name)
                    .AsNoTracking()
                    .ToList()
            };
        }

        public ClientEntity Read(int id) => Get(x => x.Id == id).SingleOrDefault();

        public bool ClientTypeHasClients(int ctid) => Get(x => x.ClientTypeId == ctid).Any();
    }
}
