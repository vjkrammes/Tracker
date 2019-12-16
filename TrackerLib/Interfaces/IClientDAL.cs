using TrackerLib.Entities;

namespace TrackerLib.Interfaces
{
    public interface IClientDAL : IDAL<ClientEntity>, IIdDAL<ClientEntity>
    {
        ClientEntity Read(string name);
        bool ClientTypeHasClients(int ctid);
    }
}
