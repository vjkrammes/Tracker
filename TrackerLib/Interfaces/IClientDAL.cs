using System.Collections.Generic;
using TrackerLib.Entities;

namespace TrackerLib.Interfaces
{
    public interface IClientDAL : IDAL<ClientEntity>, IIdDAL<ClientEntity>
    {
        IEnumerable<ClientEntity> ClientsWithActivity(int year = 0);
        IEnumerable<ClientEntity> ClientsWithActivity(int start, int end);
        ClientEntity Read(string name);
        bool ClientTypeHasClients(int ctid);
    }
}
