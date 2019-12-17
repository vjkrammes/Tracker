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
                    .OrderBy(x => x.Name)
                    .AsNoTracking()
                    .ToList(),
                _ => _dbset
                    .Include(x => x.ClientType)
                    .Where(pred)
                    .OrderBy(x => x.Name)
                    .AsNoTracking()
                    .ToList()
            };
        }

        public IEnumerable<ClientEntity> ClientsWithActivity(int year = 0)
        {
            HashSet<int> mclients;
            HashSet<int> hclients;
            switch (year)
            {
                case 0:
                    hclients = new HashSet<int>(_context.Hours.Select(x => x.ClientId).Distinct());
                    mclients = new HashSet<int>(_context.Mileage.Select(x => x.ClientId).Distinct());
                    break;
                default:
                    hclients = new HashSet<int>(_context.Hours.Where(x => x.Date.Year == year).Select(x => x.ClientId).Distinct());
                    mclients = new HashSet<int>(_context.Mileage.Where(x => x.Date.Year == year).Select(x => x.ClientId).Distinct());
                    break;
            }
            mclients.UnionWith(hclients);
            List<ClientEntity> ret = new List<ClientEntity>();
            foreach (var id in mclients)
            {
                ret.Add(Read(id));
            }
            return ret;
        }

        public IEnumerable<ClientEntity> ClientsWithActivity(int start, int end)
        {
            HashSet<int> mclients = new HashSet<int>(_context.Mileage
                .Where(x => x.Date.Year >= start && x.Date.Year <= end)
                .Select(x => x.ClientId)
                .Distinct());
            HashSet<int> hclients = new HashSet<int>(_context.Hours
                .Where(x => x.Date.Year >= start && x.Date.Year <= end)
                .Select(x => x.ClientId)
                .Distinct());
            mclients.UnionWith(hclients);
            List<ClientEntity> ret = new List<ClientEntity>();
            foreach (var id in mclients)
            {
                ret.Add(Read(id));
            }
            return ret;
        }

        public ClientEntity Read(int id) => Get(x => x.Id == id).SingleOrDefault();

        public ClientEntity Read(string name) => Get(x => x.Name == name).SingleOrDefault();

        public bool ClientTypeHasClients(int ctid) => Get(x => x.ClientTypeId == ctid).Any();
    }
}
