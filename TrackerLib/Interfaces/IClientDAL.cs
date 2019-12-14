using TrackerLib.Entities;

namespace TrackerLib.Interfaces
{
    public interface IClientDAL : IDAL<ClientEntity>, IIdDAL<ClientEntity>
    {
        bool ClientTypeHasClients(int ctid);
    }
}
