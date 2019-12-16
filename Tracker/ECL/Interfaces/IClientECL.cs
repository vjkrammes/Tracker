using Tracker.ECL.DTO;

using TrackerLib.Entities;

namespace Tracker.ECL.Interfaces
{
    public interface IClientECL : IECL<ClientEntity, Client>
    {
        void Delete(int id);
        Client Read(int id);
        Client Read(string name);
        bool ClientTypeHasClients(int ctid);
    }
}
