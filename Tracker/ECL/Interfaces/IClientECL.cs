using System.Collections.Generic;
using Tracker.ECL.DTO;

using TrackerLib.Entities;

namespace Tracker.ECL.Interfaces
{
    public interface IClientECL : IECL<ClientEntity, Client>
    {
        void Delete(int id);
        IEnumerable<Client> ClientsWithActivity(int year = 0);
        IEnumerable<Client> ClientsWithActivity(int start, int end);
        Client Read(int id);
        Client Read(string name);
        bool ClientTypeHasClients(int ctid);
    }
}
